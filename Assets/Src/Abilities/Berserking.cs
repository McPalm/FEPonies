using UnityEngine;
using System.Collections.Generic;
using System;

public class Berserking : Passive, IAttackModifier, HealthObserver {

	bool active = false;

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

	public void Test(DamageData dd)
	{
		if(Active)
		{ 
			dd.damageMultipler *= 1.2f;
		}
	}

	void Activate()
	{
		active = true;
		GetComponent<SpriteRenderer>().color = new Color(1f, 0.3f, 0.3f);
	}

	void Deactivate()
	{
		active = false;
		GetComponent<SpriteRenderer>().color = Color.white;
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
	}
}