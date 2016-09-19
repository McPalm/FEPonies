using UnityEngine;
using System.Collections;
using System;

public class FollowThrough: Passive, AttackBuff {

	private Stats _stats;
	private bool active = false;
	private Unit _unit;

	public override string Name
	{
		get
		{
			return "Follow Through";
		}
	}

	public Stats Stats
	{
		get
		{
			return _stats;
		}
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		return active;
	}




	// Use this for initialization
	void Start () {
		_stats.hitBonus = 0.2f;
		_stats.crit = -0.2f;
		_stats.might = -4;

		_unit = GetComponent<Unit>();

		_unit.RegisterAttackBuff(this);
	}
	
	public void OnMissAttack(Unit target)
	{
		print("Miss!");
		active = true;
		_unit.doubleAttack = true;
	}

	public void FinishedAttackSequence(Unit u)
	{
		active = false;
		_unit.doubleAttack = false;
	}
}
