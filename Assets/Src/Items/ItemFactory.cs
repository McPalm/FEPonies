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
			case "Spear":
				wf.Level = 1;
				wf.SetMeleeAndRange();
				return wf.GetWeapon();
			case "Axe":
				wf.Level = 1;
				wf.HighCrit();
				wf.LowHit();
				return wf.GetWeapon();
			case "Sword":
				wf.Level = 1;
				wf.HighHit();
				return wf.GetWeapon();
			case "Crossbow":
				wf.Level = 1;
				wf.SetLongRange();
				return wf.GetWeapon();
			case "Wand":
				wf.Level = 1;
				wf.SetMagic();
				wf.SetMeleeAndRange();
				return wf.GetWeapon();

		}
		Item i = ArmorDB.Instance.GetArmor(name);
		if (i != null) return i;

		i = new Item();
		i.Name = "Rubbish";
		return i;
	}
}
