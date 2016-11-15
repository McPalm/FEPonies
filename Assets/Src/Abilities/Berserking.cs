using UnityEngine;
using System.Collections.Generic;
using System;

public class Berserking : Passive, IAttackModifier, HealthObserver, Buff {

	bool active = false;
	Stats _stats;

	public override string Name
	{
		get
		{
			return "Berserking";
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public bool Active
	{
		get
		{
			return active;
		}
	}

	public Stats Stats
	{
		get
		{
			return _stats;
		}
	}

	public void Test(DamageData dd)
	{
		if(Active)
		{ 
			dd.damageMultipler *= 1.25f;
		}
	}

	void Activate()
	{
		active = true;
		GetComponent<SpriteRenderer>().color = new Color(1f, 0.3f, 0.3f);
		_stats.dodgeBonus = -0.25f;
		_stats.strength = 1;
	}

	void Deactivate()
	{
		active = false;
		GetComponent<SpriteRenderer>().color = Color.white;
		_stats.dodgeBonus = -0f;
		_stats.strength = 0;
	}

	public void NotifyHealth(Unit unit, int change)
	{
		if(Active &! (unit.damageTaken >= unit.CurrentHP))
		{
			Deactivate();
		}
		else if (unit.damageTaken >= unit.CurrentHP & !Active)
		{
			Activate();
		}
	}
	void Awake()
	{
		GetComponent<Unit>().RegisterHealthObserver(this);
		_stats = new Stats();
		GetComponent<Unit>().Character.AddBuff(this);
	}
}