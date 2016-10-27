using UnityEngine;
using System.Collections;
using System;

public class Block : Passive, IDefenceModifiers {

	private Unit user;

	public override string Name
	{
		get
		{
			return "Block";
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public void DefenceTest(DamageData dd)
	{
		if (dd.testAttack)
		{
			dd.damageMultipler *= 0.75f;
		}
		else if(UnityEngine.Random.Range(0, 2) == 0)
		{
			dd.damageMultipler *= 0.5f;
			Particle.Block(transform.position);
		}
	}
}
