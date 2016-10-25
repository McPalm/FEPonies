using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
class ScalingBackpack : Backpack {

	[SerializeField]
	WeaponType wornWeapon;
	[Range(0, 2)]
	public int weight;

	void Start()
	{
		GenerateGear();
	}

	private void GenerateGear()
	{
		int level = Owner.Level;
		Stats stats = Owner.ModifiedStats;
		int str = stats.strength;
		int dex = stats.dexterity;
		int inte = stats.intelligence;

		// generate armour
		if (weight == 0) weight = 0;
		else if(weight == 1) weight = str;
		else weight = str * 2;

		Armor a = ArmorDB.Instance.GetArmor(level, weight);

		Add(a);
		Equip(a);

		// generate weapon
		Weapon w = WeaponDB.Instance.GetWeapon(level, wornWeapon, (dex > str && dex > inte));

		Add(w);
		Equip(w);

		Add(new HealingPotion());
	}

	
}

