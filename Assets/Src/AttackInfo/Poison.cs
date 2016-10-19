using UnityEngine;
using System.Collections.Generic;
using System;

public class Poison : MonoBehaviour, IEffect {

    static public int StaticApply(DamageData attackData)
	{
		if (attackData.testAttack) return 5;
		PoisonDebuff pd = attackData.target.GetComponent<PoisonDebuff>();
		if(pd == null){
			attackData.target.gameObject.AddComponent<PoisonDebuff>();
		}
		return 5;
	}

	public int Apply(DamageData attackData)
	{
		if (attackData.testAttack) return 5;
		return StaticApply(attackData);
	}
}
