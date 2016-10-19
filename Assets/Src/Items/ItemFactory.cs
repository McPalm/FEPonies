using UnityEngine;
using System.Collections;

public class ItemFactory : MonoBehaviour {

	static public Item CreateItem(string name)
	{
		ArmorFactory af = new ArmorFactory(name);
		WeaponFactory wf = new WeaponFactory(name);
		switch(name)
		{
			case "Healing Potion":
				return new HealingPotion();
			case "Banded Mail":
				af.Level = 1;
				af.Resistance = 0.3f;
				af.Weight = 3;
				return af.GetArmour();
			case "Simple Robes":
				af.Level = 1;
				af.Resistance = 1f;
				af.Weight = 0;
				return af.GetArmour();
			case "Leather Barding":
				af.Level = 1;
				af.Resistance = 0.5f;
				af.Weight = 1;
				return af.GetArmour();
			case "Ring Mail":
				af.Level = 1;
				af.Resistance = 0.5f;
				af.Weight = 2;
				return af.GetArmour();
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

		Item i = new Item();
		i.Name = "Rubbish";
		return i;
	}
}
