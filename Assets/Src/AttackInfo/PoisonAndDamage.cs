using UnityEngine;
using System.Collections;
using System;

public class PoisonAndDamage : MonoBehaviour, IEffect {

	public int Apply(DamageData attackData)
	{
		return StaticApply(attackData);
	}


    public int StaticApply(DamageData attackData)
	{
		return Damage.StaticApply(attackData) + Poison.StaticApply(attackData);
	}	
}
