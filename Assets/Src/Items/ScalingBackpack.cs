using UnityEngine;
using System.Collections;
using System;

class ScalingBackpack : Backpack {

	public WeaponType wornWeapon;
	[Range(0, 3)]
	public int weight;

	void Start()
	{
		GenerateGear();
		base.Start();
	}

	private void GenerateGear()
	{
		int level = GetComponent<Unit>().level;

		// generate armour
		Armor a = new Armor();
		a.weight = weight;
		int defence = weight * 2 - 1 + level / 2;
		int resistance = level / 3;

		if (weight == 0)
		{
			defence = 0;
			resistance += 2;
			a.name = "Robes";
		}

		a.buff.defense = defence;
		a.buff.resistance = resistance;
		if (weight == 1) a.name = "Light Armour";
		if (weight == 2) a.name = "Medium Armour";
		if (weight == 3) a.name = "Heavy Armour";

		Add(a);
		Equip(a);

		// generate weapon
		Weapon w = new Weapon();

		switch (wornWeapon)
		{
			case WeaponType.axe:
				w.buff.hitBonus = -0.2f;
				w.buff.might = level + 5;
				w.name = "Axe";
				break;
			case WeaponType.sword:
				w.buff.hitBonus = 0.1f;
				w.buff.critBonus = 0.1f;
				w.buff.might = level + 3;
				w.name = "Sword";
				break;
			case WeaponType.spear:
				w.buff.might = level + 4;
				w.name = "Spear";
				break;
			default:
				w.buff.might = level + 4;
				w.name = "Weapon";
				break;
		}

		Add(w);
		Equip(w);
	}

	
}

