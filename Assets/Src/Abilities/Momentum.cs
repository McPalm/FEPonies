using UnityEngine;
using System.Collections.Generic;
using System;

public class Momentum : Passive, IAttackModifier, Buff {

	Stats _activeStats;
	Stats _sleepingStats;
	int _triggerTurn = -5;

	public override string Name
	{
		get
		{
			return "Momentum";
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
			return (IsActive) ? _activeStats : _sleepingStats;
		}
	}

	public bool IsActive
	{
		get
		{
			return UnitManager.Instance.currTurn < _triggerTurn+2;
		}
	}

	public void Test(DamageData dd)
	{
		if (IsActive) dd.damageMultipler *= 1.1f;
		dd.RegisterCallback(EndOfAttack);
	}

	// Use this for initialization
	void Start () {
		_activeStats = new Stats();
		_activeStats.movement.moveSpeed = 1;
		_sleepingStats = new Stats();
		GetComponent<Unit>().Character.AddBuff(this);
	}
	
	void EndOfAttack(DamageData dd)
	{
		if(dd.FinalDamage >= dd.target.CurrentHP)
		{
			Debug.Log("Thats a killing blow!");
			_triggerTurn = UnitManager.Instance.currTurn;
		}
	}
}