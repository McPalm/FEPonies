using UnityEngine;
using System.Collections;

public class AIBackpack : MonoBehaviour {

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
		Character c = GetComponent<Unit>().Character;

		Backpack b = c.Backpack;

		b.EmptyBackpack();

        int level = c.Level;
        int budget = level;
        if (armor != "")
        {
            Armor a = ArmorDB.Instance.GetArmor(armor, budget);
            if (a == null) Debug.LogError(armor + " not found in armor database");
            b.Add(a);
            b.Equip(a);
            budget += level - ArmorDB.Instance.GetLevel(a.Name);
        }
        else budget += 2;

        if (weapon != "")
        {
            Weapon w = WeaponDB.Instance.GetWeapon(weapon, budget);
            if (w == null) Debug.LogError(weapon + " not found in weapons database");
            b.Add(w);
            b.Equip(w);
        }
        foreach (string e in extras)
        {
            Item i = ItemFactory.CreateItem(e);
            if (i.Name == "Rubbish") Debug.LogError(e + " generated Rubbish.");
            if (!c.Backpack.Add(i)) Debug.LogWarning(c.Name + " ran out of inventory space.");
        }
	}
}
