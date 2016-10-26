using UnityEngine;
using System.Collections.Generic;
using System;

public class Advice : Ability, TargetedAbility {

	public override string Name
	{
		get
		{
			return "Advice";
		}
	}

	public HashSet<Tile> GetAvailableTargets()
	{
		return GetAvailableTargets(GetComponent<Unit>().Tile);
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal = new HashSet<Tile>();
		if (host == null)
		{
			Debug.LogError("host not found for Cure");
		}
		else
		{
			
			foreach (Tile t in Melee.StaticGetTiles(tile))
			{
				if (t.isOccuppied && !host.isHostile(t.Unit))
				{
					retVal.Add(t);
				}
			}
			foreach (Tile t in IncreasedRange.StaticGetTiles(tile))
			{
				if (t.isOccuppied && !host.isHostile(t.Unit))
				{
					retVal.Add(t);
				}
			}
		}
		return retVal;
	}

	public void Notify(Tile target)
	{
		AdviceBuff ab = target.Unit.gameObject.AddComponent<AdviceBuff>();
		int i = Mathf.RoundToInt(GetComponent<Unit>().Character.ModifiedStats.intelligence * 0.75f);
		ab.Initialize(i);

		FinishUse();
	}

	public override void Use()
	{
		TargetedAbilityInputManager.Instance.Listen(this);
	}
}