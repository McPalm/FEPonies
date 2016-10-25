using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
/// <summary>
/// Class to handle what the unit are carrying, one unit will have one backpack
/// backpack - includes all items the unit are carrying
/// equippedArmor - the Armor currently equipped
/// equippedWeapon - the Weapon currently equipped
/// equippedTrinket - the Trinket currently equipped
/// capacity - number of items that fit into the backpack
/// </summary>
public class Backpack : IEnumerable<Item>, IEnumerable<Consumable>, IEnumerable<Weapon>, Buff
{
    private List<Item> backpack = new List<Item>();
    [SerializeField]
    Armor equippedArmor;
    [SerializeField]
    Weapon equippedWeapon;
    [SerializeField]
    Equipment equippedTrinket;
    [SerializeField]
    int capacity = 5;
	[NonSerialized]
	Character owner;
	Stats _equipmentStats;

	/// <summary>
	/// Currently Equipped Armor
	/// </summary>
	public Armor EquippedArmor
	{
		get { return equippedArmor; }
	}

	/// <summary>
	/// Currently Equipped Weapon
	/// </summary>
	public Weapon EquippedWeapon
	{
		get { return equippedWeapon; }
	}

	/// <summary>
	/// Currenlty Equipped Trinket
	/// </summary>
	public Equipment EquippedTrinket
	{
		get { return equippedTrinket; }
	}

	/// <summary>
	/// Maxmium number of items that can fit into the backpack
	/// </summary>
	public int Capacity
	{
		get { return capacity; }
	}

	public Stats Stats
	{
		get
		{
			return _equipmentStats;
		}
	}

	public Character Owner
	{
		get
		{
			return owner;
		}

		set
		{
			owner = value;
			owner.AddBuff(this);
		}
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
		if (backpack.Count >= Capacity) return false;
		backpack.Add(toBeAdded);
		return true;
    }

    /// <summary>
    /// Removes an item from the backpack returns false if it couldn't, maybe it didn't exist in the backpack? usually something wrong if this happens.
    /// </summary>
    /// <param name="toBeRemoved">The Item to be removed</param>
    /// <returns>true if succesful, false if not, if false something is probably wrong.</returns>
    public bool Remove(Item toBeRemoved)
    {
        if(toBeRemoved is Equipment)
		{
			UnEquip((Equipment)toBeRemoved);
		}
		backpack.Remove(toBeRemoved);
		return true;
    }

    /// <summary>
    /// Equips the Item, puts in the aplicable variable in this class
    /// </summary>
    /// <param name="toBeEquipped">The item to be equipped</param>
    /// <returns>Returns false if failed for some reason</returns>
    public bool Equip(Equipment toBeEquipped)
    {
		if (backpack.Contains(toBeEquipped))
		{
			if (toBeEquipped is Armor)
			{
				if ((toBeEquipped as Armor).weight > owner.ModifiedStats.CarryingCapacity * 2) return false;
				equippedArmor = (Armor)toBeEquipped;
				countStats();
				return true;
			}
			else if (toBeEquipped is Weapon)
			{
				equippedWeapon = (Weapon)toBeEquipped;
				countStats();
				return true;
			}
			else
			{
				equippedTrinket = toBeEquipped;
				countStats();
				return true;
			}
		}
		throw new Exception("Trying to equip an item not in the backpack!");
    }

    /// <summary>
    /// Unequips the Item, makes the variable point to null
    /// </summary>
    /// <param name="toBeUnEquipped">The item to be unequipped</param>
    /// <returns>Returns false if failed for some reason</returns>
    public bool UnEquip(Equipment toBeUnequipped)
    {
        if(toBeUnequipped == equippedArmor)
		{
			equippedArmor = null;
			countStats();
			return true;
		}
		else if(toBeUnequipped == equippedWeapon)
		{
			equippedWeapon = null;
			countStats();
			return true;
		}
		else if(toBeUnequipped == equippedTrinket)
		{
			equippedTrinket = null;
			countStats();
			return true;
		}
		return false;
    }

    /// <summary>
    /// Sorts the item list
    /// </summary>
    public void Sort()
    {
        throw new NotImplementedException();
    }

	/// <summary>
	/// Recalclate the attribute boosts granted by the item.
	/// </summary>
	void countStats()
	{
		_equipmentStats = new Stats();
		if(EquippedArmor != null) _equipmentStats = EquippedArmor.buff;
		if(EquippedWeapon != null) _equipmentStats += EquippedWeapon.buff;
		if(EquippedTrinket != null) _equipmentStats += EquippedTrinket.buff;

		// calculate encumberance
		if(equippedArmor != null)
		{
			if (equippedArmor.weight > owner.ModifiedStats.CarryingCapacity)
			{
				_equipmentStats.movement.moveSpeed--;
				_equipmentStats.dodgeBonus -= (equippedArmor.weight - owner.ModifiedStats.CarryingCapacity) * 0.05f;
			}
			else
			{
				float ratio = 1f - (equippedArmor.weight / owner.ModifiedStats.CarryingCapacity);
				_equipmentStats.dodgeBonus = ratio * 0.1f;
			}
		}
	}

	/// <summary>
	/// Checks if the item is equipped.
	/// </summary>
	/// <param name="e"></param>
	/// <returns></returns>
	public bool IsEquipped(Equipment e)
	{
		if (EquippedArmor == e) return true;
		if (EquippedWeapon == e) return true;
		if (EquippedTrinket == e) return true;
		return false;
	}

	public bool Use(int item)
	{
		if(item > backpack.Count)
		{
			Debug.LogError("Used item out of range. Tried to use item #" + item);
			return false;
		}
		if(backpack[item] is Consumable)
		{
			if (((Consumable)backpack[item]).use(owner)){
				backpack[item].stack--;
				if(backpack[item].stack == 0)
				{
					Remove(backpack[item]);
				}
				return true;
			}
		}
		return false;
	}

    /// <summary>
    /// Removes all items in the backpack
    /// </summary>
    public void EmptyBackpack()
    {
        backpack.Clear();
    }
}