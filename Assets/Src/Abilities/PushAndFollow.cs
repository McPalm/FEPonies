using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PushAndFollow : Ability, TargetedAbility {

	private Unit _unit;

	public override string Name
	{
		get
		{
			return "Shove";
		}
	}

	public HashSet<Tile> GetAvailableTargets()
	{
		return GetAvailableTargets(_unit.Tile);
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		HashSet<Tile> rv = new HashSet<Tile>();

		foreach(Tile t in Melee.StaticGetTiles(tile))
		{
			if (t.isOccuppied)
			{
				rv.Add(t);
			}
		}

		return rv;
	}

	public void Notify(Tile target)
	{
		if(Push.SmartStaticApply(target, _unit))
		{
			if (target is WaterTile && !_unit.flight)
			{
				FinishUse();
			}
			else
			{
				_unit.MoveTo(target);
				FinishUse();
			}
			
		}
		else
		{
			GUInterface.Instance.PrintMessage("Cannot push here!");
		}
	}

	public override void Use()
	{
		TargetedAbilityInputManager.Instance.Listen(this);
	}

	// Use this for initialization
	void Awake () {
		_unit = GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
