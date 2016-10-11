using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public string Name;

	public int level = 1;
	[Range(15, 25)]
	public int hp = 20;
	[Range(1, 6)]
	public int strength = 3;
	[Range(1, 6)]
	public int dexterity = 3;
	[Range(1, 6)]
	public int agility = 3;
	[Range(1, 6)]
	public int intelligence = 3;
	public bool flight = false;
	[Range(4, 7)]
	public int movement = 5;
	public AttackInfo attackInfo;

	Stats baseStats;
	HashSet<Buff> buffs;

	public Stats ModifiedStats
	{
		get
		{
			Stats r = baseStats;
			foreach(Buff b in buffs)
			{
				r += b.Stats;
			}
			return r;
		}
	}

	void Awake()
	{
		baseStats = new Stats();
		baseStats.maxHP = hp;
		baseStats.strength = strength;
		baseStats.dexterity = dexterity;
		baseStats.agility = agility;
		baseStats.intelligence = intelligence;
		baseStats.movement.moveSpeed = movement;
		baseStats.movement.moveType = (flight) ? MoveType.flying : MoveType.walking;
		// calculateSkillBonuses();
	}

	private void calculateSkillBonuses()
	{
		Debug.LogWarning("Not Yet implemented!");
	}

	public void AddBuff(Buff b)
	{
		if (buffs == null) buffs = new HashSet<Buff>();
		buffs.Add(b);
	}

	public void RemoveBuff(Buff b)
	{
		buffs.Remove(b);
	}
}
