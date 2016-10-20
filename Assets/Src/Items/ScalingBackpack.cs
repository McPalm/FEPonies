using UnityEngine;
using System.Collections;
using System;

class ScalingBackpack : Backpack {

	
	public WeaponType wornWeapon;
	[Range(0, 2)]
	public int weight;

	void Start()
	{
		GenerateGear();
	}

	private void GenerateGear()
	{
		int level = GetComponent<Unit>().Character.Level;
		int str = GetComponent<Unit>().Character.ModifiedStats.strength;

		// generate armour
		if (weight == 0) weight = 0;
		else if(weight == 1) weight = str;
		else weight = str * 2;

		Armor a = ArmorDB.Instance.GetArmor(level, weight);

		Add(a);
		Equip(a);

		// generate weapon
		WeaponFactory wf = new WeaponFactory();
		wf.Level = level;

		switch (wornWeapon)
		{
			case WeaponType.axe:
				wf.Name = "Axe";
				wf.LowHit();
				wf.HighCrit();
				break;
			case WeaponType.sword:
				wf.HighHit();
				wf.Name = "Sword";
				break;
			case WeaponType.spear:
				wf.SetMeleeAndRange();
				wf.Name = "Spear";
				break;
			case WeaponType.dagger:
				wf.SetArmorPenetrating();
				wf.HighCrit();
				wf.Name = "Dagger";
				break;
			case WeaponType.crossbow:
				wf.SetLongRange();
				wf.Name = "Dagger";
				break;
			case WeaponType.tomb:
				wf.SetMeleeAndRange();
				wf.SetMagic();
				wf.Name = "Magic Tomb";
				break;
			default:
				wf.Name = "Weapon";
				break;
		}

		Weapon w = wf.GetWeapon();

		Add(w);
		Equip(w);

		Add(new HealingPotion());
	}

	
}

