using UnityEngine;
using System.Collections.Generic;
using System;

public class TwistTheBlades : Passive, IAttackModifier {
	public override string Name
	{
		get
		{
			return "Twist the Blades";
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
		if(dd.crit || dd.backstab)
		{
			dd.damageMultipler *= 1.15f;
		}
	}
}