using UnityEngine;
using System.Collections;
using System;

public class PinAndDamage : MonoBehaviour, IEffect {

	public int duration = 1;

    public int StaticApply(DamageData dd, int duration = 1)
	{
		PinEffect.StaticApply(dd, duration);
		return Damage.StaticApply(dd); // may kill target, do last. Just to keep the sequence more safe.
	}

	public int Apply(DamageData attackData)
	{
		return StaticApply(attackData, duration);
	}
}
