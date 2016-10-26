using UnityEngine;
using System.Collections.Generic;
using System;

public class Study : Ability, IAttackModifier, TargetedAbility {

	public List<Unit> targets;

	public override string Name
	{
		get
		{
			return "Study";
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public HashSet<Tile> GetAvailableTargets()
	{
		return GetAvailableTargets(GetComponent<Unit>().Tile);
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		Unit u = GetComponent<Unit>();
		HashSet<Unit> units = UnitManager.Instance.GetUnitsByHostility(u);
		HashSet<Tile> ret = new HashSet<Tile>();
		foreach(Unit e in units)
		{
			if(TileGrid.GetDelta(u, e) < 5)  ret.Add(e.Tile);
		}

		return ret;
	}

	public void Notify(Tile target)
	{
		targets.Add(target.Unit);
		FinishUse();
	}

	public void Test(DamageData dd)
	{
		int stacks = 0;
		foreach(Unit t in targets)
		{
			if (t == dd.target) stacks++;
		}
		if (!dd.testAttack)
		{
			for (int i = 0; i < stacks; i++)
			{
				targets.Remove(dd.target);
			}
		}
		if(stacks > 0)
		{
			dd.flatBonus += stacks * GetComponent<Unit>().Character.ModifiedStats.intelligence;
		}
	}

	public override void Use()
	{
		TargetedAbilityInputManager.Instance.Listen(this);
	}
}