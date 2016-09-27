using System;
using System.Collections.Generic;

/// <summary>
/// Every consumable item inherit from this class. Most notably potions.
/// use() is when you use the item, returns false if it isn't used for some reason.
/// </summary>
public abstract class Consumable : Item
{
    public abstract bool use(Unit user);
	public abstract int MaxStack
	{
		get;
	}
	public abstract int Uses
	{
		get;
	}
}
