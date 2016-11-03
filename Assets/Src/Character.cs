using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Character {

	[SerializeField]
	string name;
    [NonSerialized]
    Sprite mugShot;
    [NonSerialized]
    Sprite sprite;
	[NonSerialized]
	Unit unit;

    [NonSerialized]
	SkillTree skilltree;
	[NonSerialized]
	Backpack backpack;

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
	[SerializeField, Range(0, 5)]
	int mana = 0;
	[SerializeField]
	private bool flight = false;

    [NonSerialized]
	Stats baseStats;
    [NonSerialized]
    HashSet<Buff> buffs;

    public Character(Character toCopy)
    {
        name = toCopy.Name; 
        mugShot = toCopy.MugShot;
        sprite = toCopy.Sprite;
        unit = toCopy.Unit;
        if(toCopy.Skilltree != null) skilltree = new SkillTree(toCopy.Skilltree);
		if (toCopy.Backpack != null)
		{
			Backpack temp = new Backpack(toCopy.Backpack);
			Backpack = temp;
		}
        level = toCopy.level;
        hp = toCopy.hp;
        strength = toCopy.strength;
        dexterity = toCopy.dexterity;
        agility = toCopy.agility;
        intelligence = toCopy.intelligence;
        flight = toCopy.flight;
    }

	public Stats ModifiedStats
	{
		get
		{
			Stats r = baseStats;
			if (buffs == null) buffs = new HashSet<Buff>();
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
			baseStats.maxHP = hp * (4 + level) / 5;
			level = value;
		}
	}

	public SkillTree Skilltree
	{
		get
		{
			return skilltree;
		}
        set
        {
            skilltree = value;
        }
	}

	public Backpack Backpack
	{
		get
		{
			if (backpack == null) backpack = new Backpack();
			return backpack;
		}

		set
		{
			backpack = value;
			backpack.Owner = this;
		}
	}

	public Sprite MugShot
	{
		get
		{
			return mugShot;
		}

		set
		{
			mugShot = value;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}

		set
		{
			name = value;
		}
	}

	public Sprite Sprite
	{
		get
		{
			return sprite;
		}

		set
		{
			sprite = value;
		}
	}

	public Unit Unit
	{
		get
		{
			return unit;
		}

		set
		{
			unit = value;
		}
	}

	Character()
	{
		
	}

	public void Initialize(Unit u)
	{
		Unit = u;

		baseStats = new Stats();
		baseStats.maxHP = hp * (9 + level) / 10;
		baseStats.strength = strength;
		baseStats.dexterity = dexterity;
		baseStats.agility = agility;
		baseStats.intelligence = intelligence;
		baseStats.maxMana = mana;
		baseStats.movement.moveSpeed = (Flight) ? 6 : 5;
		baseStats.movement.moveType = (Flight) ? MoveType.flying : MoveType.walking;

		if (buffs == null) buffs = new HashSet<Buff>();
        if (skilltree == null) skilltree = new SkillTree();
		skilltree.CalculateStats(level);
		buffs.Add(skilltree);
		if(backpack == null) Backpack = new Backpack();
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
