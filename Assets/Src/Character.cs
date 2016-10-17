using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Character : MonoBehaviour {

	public string Name;
	public Sprite MugShot;

	[SerializeField]
	SkillTree skilltree;

	[SerializeField]
	int level = 1;
	[SerializeField]
	[Range(15, 25)]
	int hp = 20;
	[SerializeField]
	[Range(1, 6)]
	int strength = 3;
	[SerializeField]
	[Range(1, 6)]
	int dexterity = 3;
	[SerializeField]
	[Range(1, 6)]
	int agility = 3;
	[SerializeField]
	[Range(1, 6)]
	int intelligence = 3;
	[SerializeField]
	private bool flight = false;
	[SerializeField]
	[Range(4, 7)]
	int movement = 5;
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

	public bool Flight
	{
		get
		{
			return flight;
		}

		set
		{
			flight = value;
		}
	}

	public int Level
	{
		get
		{
			return level;
		}

		set
		{
			level = value;
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
		baseStats.movement.moveType = (Flight) ? MoveType.flying : MoveType.walking;

		if (buffs == null) buffs = new HashSet<Buff>();
		if (skilltree == null) skilltree = GetComponent<SkillTree>();
		if (skilltree == null) skilltree = gameObject.AddComponent<SkillTree>();
			skilltree.CalculateStats(level);
		buffs.Add(skilltree);
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
