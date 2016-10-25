using UnityEngine;
using System.Collections.Generic;
using System;

public class BladeDancing : Passive, IAttackModifier, Buff, TurnObserver {

	int t3 = 0;
	int t2 = 0;
	int t1 = 0;
	int t0 = 0;

	Stats _stats;

	public int Stacks
	{
		get
		{
			return t0 + t1 + t2 + t3;
		}
	}

	public override string Name
	{
		get
		{
			return "Blade Dancing";
		}
	}

	public Stats Stats
	{
		get
		{
			_stats.hitBonus = 0.04f * Stacks;
			return _stats;
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	void Start()
	{
		GetComponent<Unit>().Character.AddBuff(this);
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	public void OnDodgeAttack(Unit attacker)
	{
		print("Dodged!");
		t0++;
	}

	public void Notify(int turn)
	{
		if (UnitManager.Instance.IsItMyTurn(GetComponent<Unit>()))
		{
			t3 = t2;
			t2 = t1;
			t1 = t0;
			t0 = 0;
			print("Stacks: " + Stacks);
		}
	}

	public void Test(DamageData dd)
	{
		dd.damageMultipler *= 1f + Stacks * 0.04f;
	}
}