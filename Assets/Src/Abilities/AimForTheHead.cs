using UnityEngine;
using System.Collections.Generic;
using System;

public class AimForTheHead : Stance, IAttackModifier, Buff {

	bool _active = false;
	Stats _bonus;
	Stats _nothing;

	public override bool Active
	{
		get
		{
			return _active;
		}

		protected set
		{
			_active = value;
		}
	}

	public override string Name
	{
		get
		{
			return "Aim for the Head";
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public Stats Stats
	{
		get
		{
			return (_active) ? _bonus : _nothing;
		}
	}

	public void Test(DamageData dd)
	{
		if (_active && dd.crit) dd.damageMultipler *= 1.2f;
	}

	// Use this for initialization
	void Start () {
		_bonus = new Stats();
		_bonus.critBonus = 0.15f;
		_bonus.hitBonus = -0.15f;
		_nothing = new Stats();
		GetComponent<Unit>().Character.AddBuff(this);
	}
}