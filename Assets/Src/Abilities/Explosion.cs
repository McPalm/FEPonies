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
			_fba = new FireballAnimation();
		}
		_tgts = TargetedAbilityInputManager.Burst(target);
		
		Unit user = GetComponent<Unit>();
		Action<Tile> cb = new Action<Tile>(ApplyAndShit);
		_fba.Animate(user, target, cb);
		
		FinishUse();
	}

	private IEnumerable<Tile> _tgts;
	
	void ApplyAndShit(Tile ti){
		Character user = GetComponent<Unit>().Character;
		int dmg = user.Level + 4 + user.ModifiedStats.intelligence;

		foreach(Tile t in _tgts){
            if (t.isOccuppied)
            {
				DamageData attackData = new DamageData();
				attackData.baseDamage = dmg;
				attackData.resistanceMultiplier = 1f;
				attackData.defenceMultiplier = 0f;
				t.Unit.Damage(attackData);
            }
		}
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		return new HashSet<Tile>(IncreasedRange.StaticGetTiles(tile));
	}

	public float judgeAbility (Unit user, Tile move, out Tile target)
	{
		HashSet<Tile> possibleTargets=GetAvailableTargets(move);
		target=null;
		if(possibleTargets.Count==0)
		{
			return 0;
		}
		int dmg = user.Character.Level + 4 + user.Character.ModifiedStats.intelligence;
		float value = -1;
		float maxValue =-1;
		foreach(Tile q in possibleTargets)
		{
			value=-1;
			foreach(Tile t in TargetedAbilityInputManager.Burst(q)){
				if(t && t.isOccuppied){
					DamageData dd = new DamageData();
					dd.defenceMultiplier = 0f;
					dd.resistanceMultiplier = 1f;
					dd.baseDamage = dmg;
					dd.testAttack = true;
                    dd.target = t.Unit;
                    dd.source = user;
                    dd.SourceTile = move;
					if(t.Unit.isHostile(user)) value += t.Unit.Damage(dd);
					else value -= t.Unit.Damage(dd);
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


