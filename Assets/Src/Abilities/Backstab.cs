﻿using UnityEngine;
using System.Collections;

public class Backstab : Skill, AttackBuff
{

	private Unit u;
	private Stats damageBonus;

	public override string Name
	{
		get
		{
			return "Backstab";
		}
	}

	public Stats Stats
	{
		
		get{return damageBonus; }
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		return target.RetaliationsLeft == 0; // Might wanna make it work with mutual attack ranges.
	}

	// Use this for initialization
	public void Start()
	{
		u = GetComponent<Unit>();
		damageBonus = new Stats();
		damageBonus.might = 4 + u.level / 2;
		damageBonus.hitBonus = 0.2f;
		u.RegisterAttackBuff(this);
	}
}
