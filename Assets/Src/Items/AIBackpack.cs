using UnityEngine;
using System.Collections;

class AIBackpack : Backpack {

	[SerializeField]
	private string weapon;
	[SerializeField]
	private string armor;
	[SerializeField]
	private string trinket;
	[SerializeField]
	private string[] extras;

	void Start()
	{
		int level = GetComponent<Character>().Level;
		int budget = level;
		if (armor != "")
		{
			Armor a = ArmorDB.Instance.GetArmor(armor, budget);
			if (a == null) Debug.LogError(armor + " not found in armor database");
			Add(a);
			Equip(a);
			budget += level - ArmorDB.Instance.GetLevel(a.Name);
		}
		else budget += 2;

		if (weapon != "")
		{
			Weapon w = WeaponDB.Instance.GetWeapon(weapon, budget);
			if (w == null) Debug.LogError(weapon + " not found in weapons database");
			Add(w);
			Equip(w);
		}
		foreach (string e in extras)
		{
			Item i = ItemFactory.CreateItem(e);
			if (i.Name == "Rubbish") Debug.LogError(e + " generated Rubbish.");
			if (!Add(i)) Debug.LogWarning(gameObject.name + " ran out of inventory space.");
		}
	}
}
