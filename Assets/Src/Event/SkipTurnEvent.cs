using UnityEngine;
using System.Collections;
using System;

public class SkipTurnEvent : EventTarget
{
	bool trigger = false;

	public override void Notice()
	{
		trigger = true;
	}

	void Update()
	{
		if (trigger)
		{
			foreach (Unit u in UnitManager.Instance.GetUnitsByTeam(UnitManager.PLAYER_TEAM))
			{
				u.HasActed = true;
			}
			trigger = false;
		}
	}
}
