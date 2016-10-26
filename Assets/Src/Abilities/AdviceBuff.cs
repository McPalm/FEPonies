using UnityEngine;
using System.Collections.Generic;
using System;

public class AdviceBuff : MonoBehaviour, IAttackModifier
{
	public int bonus = 0;

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public void Test(DamageData dd)
	{
		dd.flatBonus += bonus;
		if (!dd.testAttack) dd.RegisterCallback(Expend);
	}

	public void Initialize(int bonus)
	{
		this.bonus = bonus;
	}

	void Expend(DamageData dd)
	{
		Destroy(this);
	}
}