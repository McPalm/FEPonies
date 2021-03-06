﻿using UnityEngine;
using System;

public class WeaponFactory {

	string name;
	int level = 1;
	int reach = 1;
	int defences = 1;
	int hitMod = 0;
	int critMod = 0;

	const int MELEE = 1;
	const int RANGE = 2;
	const int MELEEANDRANGE = 3;
	const int LONGRANGE = 4;

	const int ARMOR = 1;
	const int APIERCE = 2;
	const int RESIST = 3;
	const int HYBRID = 4;

	float strenght = 0;
	float dexterity = 0;
	float intelligence = 0;


	IAnimation anim = null;

	public WeaponFactory(string name = "")
	{
		this.name = name;
	}

	public int Level
	{
		set
		{
			if (value < 1) Debug.LogError("Item level cannot be lower than 1!");
			else level = value;
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

	/// <summary>
	/// Make the weapon a regular melee weapon
	/// </summary>
	public void SetMelee()
	{
		reach = MELEE;
	}
	/// <summary>
	/// Make the weapon a regular ranged weapon
	/// </summary>
	public void SetRange()
	{
		reach = RANGE;
	}
	/// <summary>
	/// Make the weapon a long range weapon
	/// </summary>
	public void SetLongRange()
	{
		reach = LONGRANGE;
	}
	/// <summary>
	/// Make the weapon a melee or range, like spells.
	/// </summary>
	public void SetMeleeAndRange()
	{
		reach = MELEEANDRANGE;
	}
	/// <summary>
	/// Makes the weapon apply against regular defence
	/// </summary>
	public void SetPhysical()
	{
		defences = ARMOR;
	}
	/// <summary>
	/// Makes the weapon ignore half armour
	/// </summary>
	public void ArmorPenetrating()
	{
		defences = APIERCE;
	}
	/// <summary>
	/// Makes the weapon apply against resistance instead of defence
	/// </summary>
	public void Magic()
	{
		defences = RESIST;
	}
	/// <summary>
	/// Makes the weapon being resisted by a split of armour and resistance
	/// </summary>
	public void SetHybrid()
	{
		defences = HYBRID;
	}
	/// <summary>
	/// Makes the weapon criticall hit more often
	/// </summary>
	public void HighCrit(int ammount = 1)
	{
		critMod += ammount;
	}
	public void HighHit(int ammount = 1)
	{
		hitMod += ammount;
	}
	public void LowHit(int ammount = 1)
	{
		hitMod = -ammount;
	}
	public void SetScaling(int str, int dex, int i)
	{
		strenght = str;
		dexterity = dex;
		intelligence = i;
	}
	public void BowAnimation()
	{
		anim = new Arrow();
	}
	public void SpearAnimation()
	{
		anim = new SpearToss();
	}
	public void LightningBolt()
	{
		anim = new ElectricAnimation();
	}
	public void Fireball()
	{
		anim = new FireballAnimation();
	}

	/// <summary>
	/// Get the weapon you generated.
	/// </summary>
	/// <returns></returns>
	public Weapon GetWeapon()
	{
		Weapon w = new Weapon();
		w.Name = Name;
		w.buff = new Stats();

		float power = level + 4;
		float advantages = 0f;
		float disadvantages = 0f;

		// advantages
		if (reach == MELEEANDRANGE) advantages += 0.25f;
		if (reach == LONGRANGE) advantages += 0.15f;
		if (defences == APIERCE) advantages += 0.5f;
		if (hitMod > 0) advantages += hitMod / 20f;
		if (critMod > 0) advantages += critMod / 20f;

		// disadvantages
		if (hitMod < 0) disadvantages += hitMod / 20f;

		// modify base damage to work with modifiers
		power /= 1f + advantages;
		power *= 1f + disadvantages;

		// grant the weapon appropiate range
		IReach ir;
		if (reach == MELEE) ir = new Melee();
		else if (reach == MELEEANDRANGE) ir = new RangeAndMelee();
		else if (reach == LONGRANGE) ir = new IncreasedRange();
		else ir = new Ranged();

		// Weapon Damage Stuffs
		WeaponDamage wd = new WeaponDamage();

		wd.StrScale = strenght * 0.25f;
		wd.DexScale = dexterity * 0.25f;
		wd.IntScale = intelligence * 0.25f;
		wd.BaseDamage = (int)power;

		w.attackInfo = new AttackInfo(ir, wd, null, anim);

		w.buff.hitBonus += hitMod * 0.1f;
		w.buff.critBonus += critMod * 0.15f;

		level += 4;
		w.value = level * level * 2;

		w.icon = SpriteLibrary.GetIcon("weapon");

		return w;
	}

	Sprite sprite;
}
