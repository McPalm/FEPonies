using System;
using System.Collections.Generic;


public static class ItemString
{
    /// <summary>
    /// Takes an itemname a string and returns an Item
    /// </summary>
    /// <param name="itemName">The string that is to be translated</param>
    /// <returns></returns>
    public static Item StringToItem(string itemName)
    {
        //TODO actually making this thing translate strings to items
        Item rv = new HealingPotion();
        rv.Name = "TestPotion";
        return rv;
    }
}
