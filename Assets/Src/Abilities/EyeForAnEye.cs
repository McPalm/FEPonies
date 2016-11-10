using UnityEngine;
using System.Collections.Generic;
using System;

public class EyeForAnEye : Passive, HealthObserver, Observer, IAttackModifier, TurnObserver
{
	Unit user;
	int stacks = 0;
	bool removeAtEndOfTurn = false;
	int distance = 2;

	public override string Name
	{
		get
		{
			return "Eye for an Eye";
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public int Distance
	{
		get
		{
			return distance;
		}

		set
		{
			distance = value;
		}
	}

	public int Stacks
	{
		get
		{
			return stacks;
		}
	}

	HashSet<Unit> allies;

	void Start()
	{
		allies = new HashSet<Unit>();
		user = GetComponent<Unit>();
		UpdateWatchList();
		UnitManager.Instance.registerObserver(this);
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	void UpdateWatchList()
	{
		foreach (Unit u in UnitManager.Instance.GetUnitsByFriendliness(user))
		{
			if (!allies.Contains(u) && u != user)
			{
				StartWatching(u);
			}
		}
	}

	public void StartWatching(Unit u)
	{
		if (allies.Add(u))
		{
			u.RegisterHealthObserver(this);
		}
		else
			Debug.LogWarning(u.name + " is already on Eye for an Eye watchlist!?");
	}

	public void Notify()
	{
		UpdateWatchList();
	}

	public void NotifyHealth(Unit unit, int change)
	{
		if(change < 0)
		{
			if(! UnitManager.Instance.IsItMyTurn(user) && TileGrid.GetDelta(unit, user) <= Distance)
			{
				Particle.ExlamationPoint(new Vector3(transform.position.x, transform.position.y + 0.3f));
				stacks++;
			}
		}

	}

	public void Test(DamageData dd)
	{
		if (stacks > 0)
			dd.damageMultipler = 1f + 0.1f * stacks;
	}

	public void Notify(int turn)
	{
		if(UnitManager.instance.IsItMyTurn(user))
		{
			removeAtEndOfTurn = true;
		}else if(removeAtEndOfTurn)
		{
			removeAtEndOfTurn = false;
			stacks = 0;
		}
	}
}