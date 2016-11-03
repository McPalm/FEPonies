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

    private bool isTrain = false;
    private int cash;

    public bool IsTrain
    {
        get
        {
            return isTrain;
        }
    }

    public GearPackage()
    {

    }

    public GearPackage(Backpack source)
    {
        foreach(Item i in source)
        {
            backpack.Add(i.Name);
        }
        if (source.EquippedArmor != null)
        {
            equippedArmor = source.EquippedArmor.Name;
        }
        if (source.EquippedWeapon != null)
        {
            equippedWeapon = source.EquippedWeapon.Name;
        }
        if (source.EquippedTrinket != null)
        {
            equippedTrinket = source.EquippedTrinket.Name;
        }
    }

    public GearPackage(Train source)
    {
        foreach (Item i in source)
        {
            backpack.Add(i.Name);
        }
        isTrain = true;
        cash = source.Cash;
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

    public Train GetTrain()
    {
        Train tempBackpack = new Train();
        foreach (string item in backpack)
        {
            Item tempItem = ItemFactory.CreateItem(item);
            tempBackpack.Add(tempItem);
        }
        tempBackpack.Cash = cash;
        return tempBackpack;
    }
}
