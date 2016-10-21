using UnityEngine;
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
	float scaling = 1f;

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
		float total = str + dex + i;
		strenght = str / total;
		dexterity = dex / total;
		intelligence = i / total;

		int spread = 0;
		if (str > 0) spread++;
		if (dex > 0) spread++;
		if (i > 0) spread++;

		strenght = strenght * 1.1f;

		if (spread == 2)
		{
			strenght = strenght * 1.2f;
			dexterity = dexterity * 1.2f;
			intelligence = intelligence * 1.2f;
		}
		if(spread == 3)
		{
			strenght = strenght * 1.4f;
			dexterity = dexterity * 1.4f;
			intelligence = intelligence * 1.4f;
		}
	}
	public void HighScaling()
	{
		scaling = 1.5f;
	}
	public void LowScaling()
	{
		scaling = 0.5f;
	}

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
		if (reach == LONGRANGE) advantages += 0.25f;
		if (defences == APIERCE) advantages += 0.5f;
		if (hitMod > 0) advantages += hitMod / 7f;
		if (critMod > 0) advantages += critMod / 5f;
		if (scaling > 0) advantages += 0.5f;

		// disadvantages
		if (hitMod < 0) disadvantages += hitMod / 10f;
		if (scaling < 0) disadvantages += 0.25f;

		// modify base damage to work with modifiers
		power /= 1f + advantages;
		power *= 1f + disadvantages;

		scaling = scaling * (1f + level * 0.05f);

		// grant the weapon appropiate range
		IReach ir;
		if (reach == MELEE) ir = new Melee();
		else if (reach == MELEEANDRANGE) ir = new RangeAndMelee();
		else if (reach == LONGRANGE) ir = new IncreasedRange();
		else ir = new Ranged();

		// Weapon Damage Stuffs
		WeaponDamage wd = GetWeaponDamage(power);

		wd.StrScale = strenght * scaling;
		wd.DexScale = dexterity * scaling;
		wd.IntScale = intelligence * scaling;

		w.attackInfo = new AttackInfo(ir, wd);

		w.buff.hitBonus += hitMod * 0.1f;
		w.buff.critBonus += critMod * 0.15f;

		level += 4;
		w.value = level * level * 2;
		return w;
	}

	private WeaponDamage GetWeaponDamage(float power)
	{
		WeaponDamage wd = new WeaponDamage();
		wd.BaseDamage = (int)(power * 1.5f);
		if (defences == APIERCE) wd.DefenceMulitiplier = 0.5f;
		if (defences == RESIST)
		{
			wd.DefenceMulitiplier = 0f;
			wd.ResistanceMultiplier = 1f;
		}
		if (defences == HYBRID)
		{
			wd.DefenceMulitiplier = 0.5f;
			wd.ResistanceMultiplier = 0.5f;
		}
		return wd;
	}
}
