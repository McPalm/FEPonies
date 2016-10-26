using UnityEngine;
using System.Collections.Generic;
using System;

public class CarefulAim : Passive, IAttackModifier, Buff, TurnObserver {

	Stats _stats;
	bool acted = true;
	int stacks = 0;

	public override string Name
	{
		get
		{
			return "Careful Aim";
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
			return _stats;
		}
	}

	public void Notify(int turn)
	{
		if(UnitManager.Instance.IsItMyTurn(GetComponent<Unit>()))
		{
			if(acted)
			{

			}
			else
			{
				stacks++;
				_stats.hitBonus = stacks * 0.05f;
			}
			acted = false;
		}
		
	}

	public void Test(DamageData dd)
	{
		dd.damageMultipler *= 1 + 0.1f * stacks;
		if(!dd.testAttack)
		{
			dd.RegisterCallback(Reset);
			acted = true;
		}
	}

	// Use this for initialization
	void Start () {
		_stats = new Stats();
		UnitManager.Instance.RegisterTurnObserver(this);
		GetComponent<Unit>().Character.AddBuff(this);
	}
	
	void Reset(DamageData dd)
	{
		_stats = new Stats();
		stacks = 0;
	}
}