using UnityEngine;
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
    [SerializeField]
	string name;

	public string Name {
		set { name = value; }
		get { return name; }
	}
	public int value;
	public Sprite icon;
	public int stack = 1;
}