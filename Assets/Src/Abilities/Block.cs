using UnityEngine;
using System.Collections;
using System;

public class Block : Passive, IDefenceModifiers {

	private bool active = false;
	private Unit user;
#pragma warning disable
	private Stats stats;

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

	public void Test(DamageData dd)
	{
		Debug.Log("Block?");
		if (dd.testAttack)
		{
			Debug.Log("Just a test..");
			dd.damageMultipler *= 0.75f;
		}
		else if(UnityEngine.Random.Range(0, 2) == 0)
		{
			Debug.Log("Block!!");
			dd.damageMultipler *= 0.5f;
			Particle.Block(transform.position);
		}
	}
}
