using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Class to handle what the unit are carrying, one unit will have one backpack
/// backpack - includes all items the unit are carrying
/// equippedArmor - the Armor currently equipped
/// equippedWeapon - the Weapon currently equipped
/// equippedTrinket - the Trinket currently equipped
/// capacity - number of items that fit into the backpack
/// </summary>
class Backpack : MonoBehaviour , IEnumerable<Item>, IEnumerable<Consumable>, IEnumerable<Weapon>
{
    private List<Item> backpack = new List<Item>();
    Armor equippedArmor;
    Weapon equippedWeapon;
    Equipment equippedTrinket;
    int capacity;

	/// <summary>
	/// Currently Equipped Armour
	/// </summary>
	public Armor EquippedArmour
	{
		get { throw new NotImplementedException(); }
	}

	/// <summary>
	/// Currently Equipped Weapon
	/// </summary>
	public Weapon EquippedWeapon
	{
		get { throw new NotImplementedException(); }
	}

	/// <summary>
	/// Currenlty Equipped Trinket
	/// </summary>
	public Equipment EquippedTrinket
	{
		get { throw new NotImplementedException(); }
	}

	/// <summary>
	/// Maxmium number of items that can fit into the backpack
	/// </summary>
	public int Capacity
	{
		get { return capacity; }
	}


	/// <summary>
	/// Gets enumerator for the backpack
	/// </summary>
	/// <returns>enumerator</returns>
	public IEnumerator<Item> GetEnumerator()
    {
        return backpack.GetEnumerator();
    }

    /// <summary>
    /// Gets enumerator for the backpack
    /// </summary>
    /// <returns>enumerator</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return backpack.GetEnumerator();
    }

    /// <summary>
    /// Gets enumerator for consumables in the backpack
    /// </summary>
    /// <returns>enumerator for consumables</returns>
    IEnumerator<Consumable> IEnumerable<Consumable>.GetEnumerator()
    {
        List<Consumable> consumables = new List<Consumable>();
        foreach (Item i in backpack)
        {
            if (i is Consumable)
            {
                consumables.Add((Consumable)i);
            }
        }
        return consumables.GetEnumerator();
    }

    /// <summary>
    /// Gets enumerator for consumables in the backpack
    /// </summary>
    /// <returns>enumerator for consumables</returns>
    IEnumerator<Weapon> IEnumerable<Weapon>.GetEnumerator()
    {
        List<Weapon> weapons = new List<Weapon>();
        foreach (Item i in backpack)
        {
            if (i is Weapon)
            {
                weapons.Add((Weapon)i);
            }
        }
        return weapons.GetEnumerator();
    }

    

    /// <summary>
    /// Adds an item to the backpack returns false if it couldn't. Most likely exceeeded capacity.
    /// </summary>
    /// <param name="toBeAdded">The Item to be added</param>
    /// <returns>true if successful, false if not. Most likely full backpack</returns>
    public bool  Add(Item toBeAdded)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Removes an item from the backpack returns false if it couldn't, maybe it didn't exist in the backpack? usually something wrong if this happens.
    /// </summary>
    /// <param name="toBeRemoved">The Item to be removed</param>
    /// <returns>true if succesful, false if not, if false something is probably wrong.</returns>
    public bool Remove(Item toBeRemoved)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Equips the Item, puts in the aplicable variable in this class
    /// </summary>
    /// <param name="toBeEquipped">The item to be equipped</param>
    /// <returns>Returns false if failed for some reason</returns>
    public bool Equip(Equipment toBeEquipped)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Unequips the Item, makes the variable point to null
    /// </summary>
    /// <param name="toBeUnEquipped">The item to be unequipped</param>
    /// <returns>Returns false if failed for some reason</returns>
    public bool UnEquip()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sorts the item list
    /// </summary>
    public void Sort()
    {
        throw new NotImplementedException();
    }

}