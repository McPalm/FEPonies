using UnityEngine;
using System.Collections;
using System;

public class Spearpoint : Passive, AttackBuff {

	Stats stats;


	public override string Name
	{
		get
		{
			return "Spearpoint!";
		}
	}

	public Stats Stats
	{
		get
		{
			return stats;
		}
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		return target.damageTaken == 0;
	}

	// Use this for initialization
	void Start () {
		GetComponent<Unit>().RegisterAttackBuff(this);
		stats = new Stats();
		stats.critBonus = 0.1f;
	}
}
