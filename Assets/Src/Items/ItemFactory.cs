using UnityEngine;
using System.Collections.Generic;

public class ItemFactory {

	static public Item CreateItem(string name)
	{
		WeaponFactory wf = new WeaponFactory(name);
		switch (name)
		{
			case "Healing Potion":
				return new HealingPotion();

		}
		Item i = ArmorDB.Instance.GetArmor(name);
		if (i != null) return i;
		i = WeaponDB.Instance.GetWeapon(name);
		if (i != null) return i;

		i = new Item();
		i.Name = "Rubbish";
		return i;
	}
}
