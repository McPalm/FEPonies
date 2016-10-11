using UnityEngine;
using System.Collections;
using System;

public class Execute : Passive, AttackBuff {

	private Stats _stats;

	public override string Name
	{
		get
		{
			return "Execute";
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
		int lost = target.damageTaken;
		int max = target.Character.ModifiedStats.maxHP;

		_stats.might = lost / 5;
		if (lost * 2 > max) _stats.hitBonus = 0.2f;
		else _stats.hitBonus = 0f;
		return true;
	}
}
