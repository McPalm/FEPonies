using UnityEngine;
using System.Collections;
using System;

public class Execute : Passive, IAttackModifier {

	private Stats _stats;

	public override string Name
	{
		get
		{
			return "Execute";
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public void Test(DamageData dd)
	{
		dd.flatBonus += dd.target.damageTaken / 4;
	}
}
