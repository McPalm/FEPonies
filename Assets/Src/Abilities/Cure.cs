using UnityEngine;
using System.Collections.Generic;

public class Cure: AbilityWithManacost, TargetedAbility, AIAbility
{
	public int judgeAbility(Unit user, Tile move, out Tile target)
	{
		HashSet<Tile> possibleTargets=GetAvailableTargets(move);
		target=null;
		if(possibleTargets.Count==0)
		{
			return 0;
		}
		
		int value = 0;
		int maxValue =0;
		foreach(Tile q in possibleTargets)
		{
			value=0;
			if(q.Unit.damageTaken< user.Character.level+10)
			{
				value=0;
			}
			else
			{
				value=q.Unit.damageTaken;
				value+=5;
				HashSet<Unit> enemyUnits=UnitManager.Instance.GetUnitsByHostility(user);
				foreach(Unit u in enemyUnits)
				{
					if(Melee.StaticGetTiles(u.Tile).Contains(move))
					{
						value-=1;
					}
					if(Ranged.staticGetTiles(u.Tile).Contains(move))
					{
						value-=1;
					}
				}
			}
			
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
	
	public Ability getAbility
	{
		get
		{
			return this;
		}
	}

	public override int ManaCost {
		get {
			return 1;
		}
	}
	
	public override void Use(){
		if (GetComponent<Mana>().CanCast(ManaCost)){
			TargetedAbilityInputManager.Instance.Listen(this);
		}
		else{
			OOM();
		}
	}
	
	public override string Name{
		get{return "Cure";}
	}
	
	public void Notify(Tile target)
	{
		Character c = GetComponent<Character>();
		int power = c.ModifiedStats.intelligence +5;
		target.Unit.Heal(power);
		foreach(Debuff d in target.Unit.GetComponents<Debuff>()){
			Destroy(d);
		}
		FinishUse();

	}
	
	public HashSet<Tile> GetAvailableTargets()
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		if(host == null){
			Debug.LogError("host not found for Cure");
		}else{
			retVal.Add(host.Tile);
			foreach(Tile t in Melee.StaticGetTiles(host.Tile)){
				if(t.isOccuppied && !host.isHostile(t.Unit)){
					retVal.Add(t);
				}
			}
		}
		return retVal;
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		if(host == null){
			Debug.LogError("host not found for Cure");
		}else{
			retVal.Add(host.Tile);
			foreach(Tile t in Melee.StaticGetTiles(tile)){
				if(t.isOccuppied && !host.isHostile(t.Unit)){
					retVal.Add(t);
				}
			}
		}
		return retVal;
	}
}