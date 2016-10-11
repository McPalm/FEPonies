using UnityEngine;
using System.Collections;
using System;

public class HealingPotion : Consumable
{

	public override int MaxStack
	{
		get
		{
			return 3;
		}
	}

	public override int Uses
	{
		get
		{
			return 1;
		}
	}

	public override bool use(Character user)
	{
		Unit u = user.GetComponent<Unit>();
		if (u = null) return false;
		if (u.damageTaken == 0) return false;
		else
		{
			u.Heal(15);
		}
		return true;
	}

	public HealingPotion()
	{
		Name = "Healing Potion";
	}
}
