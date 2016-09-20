using UnityEngine;
using System.Collections.Generic;
using System;

public class Feint : Passive, AttackBuff {

	Unit c;
	Stats _stats;
	int intelligence;

	public override string Name {
		get {
			return "Feint";
		}
	}

	// Use this for initialization
	void Start () {
		c = GetComponent<Unit>();
		RefreshBonus();
		c.RegisterAttackBuff(this);
	}

	private void RefreshBonus()
	{
		intelligence = c.ModifiedStats.intelligence;
	}

	void LevelUp(){
		RefreshBonus();
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		int targetInt = target.ModifiedStats.intelligence;
		if(intelligence > targetInt)
		{
			float bonus = (float)(intelligence - targetInt)/20;
			_stats.dodgeBonus = bonus;
			_stats.crit = bonus;
			_stats.hitBonus = bonus;
			_stats.critDodge = bonus;
			return true;
		}
		return false;
	}

	public Stats Stats {
		get{
			return _stats;
		}
	}
}
