using System;
using System.Collections.Generic;

/// <summary>
/// buff - the statboosts you get for equipping this equipment
/// ability - ev ability you get for equipping this. Can be null
/// </summary>
public class Equipment : Item
{
	public Stats buff;
	public Skill ability;
}
