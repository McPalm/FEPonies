using UnityEngine;
using System.Collections.Generic;
using System;

public class WindStance : Stance, Buff {

	bool _active = false;
	Stats _stats;
	Stats _nothing;

	public override string Name
	{
		get
		{
			return "Wind Stance";
		}
	}

	public Stats Stats
	{
		get
		{
			return (_active) ? _stats : _nothing;
		}
	}

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

	// Use this for initialization
	void Start () {
		GetComponent<Unit>().Character.AddBuff(this);
		_stats = new Stats();
		_stats.dodgeBonus = 0.3f;
		_stats.hitBonus = -0.25f;
		_nothing = new Stats();
	}
}