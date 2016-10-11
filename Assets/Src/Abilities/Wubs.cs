using UnityEngine;
using System.Collections.Generic;

public class Wubs : AbilityWithManacost, TargetedAbility {

	public override string Name {
		get {
			return "Wubs";
		}
	}

	public override void Use ()
	{
		if (GetComponent<Mana>().CanCast(ManaCost)){
			TargetedAbilityInputManager.Instance.Listen(this, TargetedAbilityInputManager.AreaOfEffect.Line, GetComponent<Unit>().Tile);
		}
		else{
			GUInterface.Instance.PrintMessage("Not Enough Mana!");
		}
	}

	public override int ManaCost {
		get {
			return 2;
		}
	}

	public void Notify (Tile target)
	{
		Unit user = GetComponent<Unit>();
		int damageDealth = user.Character.level + 4 + user.ModifiedStats.intelligence;
		foreach(Tile t in TargetedAbilityInputManager.Line3(user.Tile, target)){
			if(t.isOccuppied) t.Unit.Damage(damageDealth, new DamageType(DamageType.MAGIC), false);
		}

		FinishUse();
	}

	public HashSet<Tile> GetAvailableTargets ()
	{
		return GetAvailableTargets(GetComponent<Unit>().Tile);
	}

	public HashSet<Tile> GetAvailableTargets (Tile tile)
	{
		HashSet<Tile> h = new HashSet<Tile>(Melee.StaticGetTiles(tile));
		h.UnionWith(IncreasedRange.StaticGetTiles(tile));
		return h;
	}


	public int judgeAbility (Unit user, Tile move, out Tile target)
	{
		HashSet<Tile> possibleTargets=GetAvailableTargets(move);
		target=null;
		if(possibleTargets.Count==0)
		{
			return 0;
		}

		int damageDealth = user.Character.level + 4 + user.ModifiedStats.intelligence;
		int value = -1;
		int maxValue =-1;
		foreach(Tile q in possibleTargets)
		{
			value=-1;
			foreach(Tile t in TargetedAbilityInputManager.Line3(move,q)){
				if(t.isOccuppied){
					if(t.Unit.isHostile(user)) value += t.Unit.Damage(damageDealth, new DamageType(DamageType.MAGIC), true);
					else if(t.Unit != user) value -= t.Unit.Damage(damageDealth, new DamageType(DamageType.MAGIC), true);
				}
			}
			if(value > 5) value += 20;
			if (value>maxValue)
			{
				target=q;
				maxValue=value;
			}
		}
		return maxValue;
	}
	
	public bool willCast()
	{
		if (!GetComponent<Mana>().CanCast(ManaCost)) return false;
		else return true;
	}
	
	public Ability getAbility {
		get {
			return this;
		}
	}

}
