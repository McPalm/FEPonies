using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class GearPackage
{
    [SerializeField]
    private List<string> backpack = new List<string>();
    [SerializeField]
    private string equippedArmor;
    [SerializeField]
    private string equippedWeapon;
    [SerializeField]
    private string equippedTrinket;

    public GearPackage()
    {

    }

    public GearPackage(Backpack source)
    {
        foreach(Item i in source)
        {
            backpack.Add(i.Name);
        }
        equippedArmor = source.EquippedArmor.Name;
        equippedWeapon = source.EquippedWeapon.Name;
        equippedTrinket = source.EquippedTrinket.Name;
    }

    public Backpack GetBackpack()
    {
        Backpack tempBackpack = new Backpack();
        foreach(string item in backpack)
        {
            Item tempItem = ItemFactory.CreateItem(item);
            tempBackpack.Add(tempItem);
        }
        foreach(Item i in tempBackpack)
        {
            if (equippedArmor==i.Name||equippedWeapon==i.Name||equippedTrinket==i.Name)
            {
                tempBackpack.Equip((Equipment)i);
            }
        }
        return tempBackpack;
    }
}
