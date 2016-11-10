using UnityEngine;
using System.Collections.Generic;
using System;

public class BloodThirst : Passive, IAttackModifier {

	Berserking berserk;

	public override string Name
	{
		get
		{
			return "Blood Thirst";
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
		if (berserk == null)
			berserk = GetComponent<Berserking>();
		if (berserk && berserk.Active)
		{
			dd.RegisterCallback(Healme);
		}
	}

	void Healme(DamageData d)
	{
		
		if (UnityEngine.Random.Range(0, 2) == 0)
		{
			int heal = (d.FinalDamage / 2 + d.source.Character.Level) / 2 + 3;
			d.source.Heal(heal);
		}
	}
}