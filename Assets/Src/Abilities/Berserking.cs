using UnityEngine;
using System.Collections.Generic;
using System;

public class Berserking : Passive, IAttackModifier {

	bool active = false;

	public override string Name
	{
		get
		{
			return "Berserking";
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
		if(active)
		{
			dd.damageMultipler *= 1.2f;
		}
	}

	void FinishedAttackSequence(Unit u)
	{
		Unit user = GetComponent<Unit>();
		if (user.damageTaken >= user.CurrentHP)
		{
			Activate();
		}
	}

	void Activate()
	{
		active = true;
		GetComponent<SpriteRenderer>().color = Color.red;
	}
}