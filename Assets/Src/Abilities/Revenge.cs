using UnityEngine;
using System.Collections.Generic;
using System;

public class Revenge : Passive, Buff {

	EyeForAnEye e4e;
	Stats s;

	public override string Name
	{
		get
		{
			return "Revenge";
		}
	}

	public Stats Stats
	{
		get
		{
			s.hitBonus = 0.03f * e4e.Stacks;
			s.critBonus = 0.03f * e4e.Stacks;
			return s;
		}
	}

	// Use this for initialization
	void Start () {
		e4e = GetComponent<EyeForAnEye>();
		e4e.Distance++;
		GetComponent<Unit>().Character.AddBuff(this);
		s = new Stats();
	}
}