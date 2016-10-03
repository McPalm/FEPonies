using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Used by thrownable items.
/// </summary>
public class Thrown : Passive, TurnObserver
{

	bool active = true;
	int storedRetaliations = 0;
	bool threw = false;
	Unit owner;

	public override string Name
	{
		get
		{
			return "Thrown Weapon";
		}
	}

	public void Notify(int turn)
	{
		if(threw && UnitManager.Instance.IsItMyTurn(owner))
		{
			owner.Retaliations = storedRetaliations;
			threw = false;
		}
	}

	void FinishedAttackSequence(Unit u)
	{
		if(active && UnitManager.Instance.IsItMyTurn(owner) &! threw)
		{ 
			if (TileGrid.GetDelta(this, u) == 2)
			{
				storedRetaliations = owner.Retaliations;
				owner.Retaliations = 0;
				threw = true;
			}
		}
	}

	void Start()
	{
		UnitManager.Instance.RegisterTurnObserver(this);
		owner = GetComponent<Unit>();
	}
}
