using System;
using UnityEngine;
using System.Collections.Generic;

public class Explosion : AbilityWithManacost, TargetedAbility, AIAbility {

	private FireballAnimation _fba;
	private DamageType dt = new DamageType();
	 
	public override string Name {
		get {
			return "Explosion";
		}
	}

	public override void Use ()
	{
		if (GetComponent<Mana>().CanCast(ManaCost)){
			TargetedAbilityInputManager.Instance.Listen(this, TargetedAbilityInputManager.AreaOfEffect.Burst);
		}
		else{
			OOM();
		}
	}

	public override int ManaCost {
		get {
			return 2;
		}
	}

	void Start(){
		dt.Magic = true;
	}

	public HashSet<Tile> GetAvailableTargets ()
	{
		Tile t = GetComponent<Unit>().Tile;
		return new HashSet<Tile>(IncreasedRange.StaticGetTiles(t));
	}

	public void Notify (Tile target)
	{
		if(_fba == null){
			_fba = gameObject.AddComponent<FireballAnimation>();
		}
		_tgts = TargetedAbilityInputManager.Burst(target);
		
		Unit user = GetComponent<Unit>();
		Action<Tile> cb = new Action<Tile>(ApplyAndShit);
		_fba.Animate(user, target, cb);
		
		FinishUse();
	}

	private IEnumerable<Tile> _tgts;
	
	void ApplyAndShit(Tile ti){
		Unit user = GetComponent<Unit>();
		int damageDealth = user.level + 4 + user.ModifiedStats.intelligence;
		foreach(Tile t in _tgts){


			if(t.isOccuppied) t.Unit.Damage(damageDealth, dt);
		}
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		return new HashSet<Tile>(IncreasedRange.StaticGetTiles(tile));
	}

	public int judgeAbility (Unit user, Tile move, out Tile target)
	{
		HashSet<Tile> possibleTargets=GetAvailableTargets(move);
		target=null;
		if(possibleTargets.Count==0)
		{
			return 0;
		}
		int damageDealth = user.level + 4 + user.ModifiedStats.intelligence;
		int value = -1;
		int maxValue =-1;
		foreach(Tile q in possibleTargets)
		{
			value=-1;
			foreach(Tile t in TargetedAbilityInputManager.Burst(q)){
				if(t && t.isOccuppied){
					if(t.Unit.isHostile(user)) value += t.Unit.Damage(damageDealth, dt, true);
					else value -= t.Unit.Damage(damageDealth, dt, true);
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


