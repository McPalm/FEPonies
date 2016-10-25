using UnityEngine;
using System.Collections;
using System;

public class Block : Passive, AttackBuff {

	private Unit user;
<<<<<<< HEAD
	private Stats stats;
=======
>>>>>>> dee71704e70c6d067d00bcb81c6fa6b32e4d6b39

	public override string Name
	{
		get
		{
			return "Block";
		}
	}

	public Stats Stats
	{
		get
		{
			return stats;
		}
	}

	void StartingAttackSequence(Unit u)
	{
		CalcStats(u);
		active = Roll;
		if (Roll) Particle.Block(transform.position);
	}

	private bool Roll
	{
		get
		{
			int roll = UnityEngine.Random.Range(0, 2);
			return (roll == 0);
		}
	}

	private void CalcStats(Unit u)
	{
		/*int damage = u.AttackInfo.effect.Apply(u.Tile, u, true, user.Tile);
		stats.defense = damage / 2;
		stats.resistance = damage / 2;*/
	}

	void FinishedAttackSequence(Unit u)
	{
		active = false;
	}

	public bool Affects(Unit u)
	{
		return (active && u == user);
	}

	void Awake()
	{
		user = GetComponent<Unit>();
		user.RegisterAttackBuff(this);
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		return active;
	}
}
