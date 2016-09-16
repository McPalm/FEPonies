using UnityEngine;
using System.Collections.Generic;
using System;

public class MageSlayer : Skill, AttackBuff {

	Stats _stats;

	public override string Name {
		get {
			return "Mageslayer";
		}
	}

	public Stats Stats
	{
		get
		{
			return _stats;
		}
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		int targetMana = target.GetComponent<Mana>().MaxMana;
		if(targetMana > 0)
		{
			_stats.might = targetMana * 2;
			return true;
		}
		return false;
	}

	// Use this for initialization
	void Start () {
		Unit u = GetComponent<Unit>();
		u.RegisterAttackBuff(this);
		_stats.hitBonus = 0.1f;
	}
}
