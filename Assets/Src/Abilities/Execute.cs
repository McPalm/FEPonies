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
		float targetp = 1f - ((float)dd.target.CurrentHP / (float)dd.target.Character.ModifiedStats.maxHP);
		Debug.Log("Execute: " + targetp);
		dd.damageMultipler *= 1f + 0.3f * targetp;
	}
}
