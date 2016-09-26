using UnityEngine;
using System.Collections;
using System;

public class SkipTurnEvent : EventTarget
{
	bool trigger = false;

	public override void Notice()
	{
		print("notice me sempai!");
		trigger = true;
	}

	void Update()
	{
		if (trigger)
		{
			print("doing stuffs!");
			foreach (Unit u in UnitManager.Instance.GetUnitsByTeam(UnitManager.PLAYER_TEAM))
			{
				u.HasActed = true;
			}
			trigger = false;
		}
	}
}
