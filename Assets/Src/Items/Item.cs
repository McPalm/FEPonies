using System;
using System.Collections.Generic;

/// <summary>
/// base class for all kinds of Items that can be put into a backpack
/// name - name of the item, is displayed in the backpack
/// value - the value of the item in silver pieces (sp)
/// icon - a icon for the item, is displayed in the backpack
/// </summary>
public class Item
{
    string name;
    int value;
    UnityEngine.UI.Image icon;
}