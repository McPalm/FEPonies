using UnityEngine;
using System.Collections;
using System;

public class Push : MonoBehaviour, IEffect {

	public const int NORTH = 1;
	public const int EAST = 2;
	public const int SOUTH = 3;
	public const int WEST = 4;

	public DamageType damageType
	{
		get
		{
			return new DamageType();
		}

		set
		{
			
		}
	}


	public void Apply(Tile target, Unit user)
	{
		Apply(target, user, false);
	}

	public int Apply(Tile target, Unit user, bool testAttack)
	{
		if (testAttack) return 0;
		// TODO, calculate direction using user.
		Vector2 relative = user.transform.position - target.transform.position;
		if (relative.y > 0.5f) StaticApply(target, SOUTH);
		else if (relative.y < -0.5f) StaticApply(target, NORTH);
		else if (relative.x > 0.5f) StaticApply(target, WEST);
		else StaticApply(target, EAST);
		return 0;
	}

	/// <summary>
	/// Push a unit in target square in a given direction. but only if it can enter it.
	/// </summary>
	/// <param name="target"></param>
	/// <param name="direction, use the constants in this class to pick a direction"></param>
	static public bool StaticApply(Tile target, int direction)
	{
		print(target.isOccuppied);
		if (!target.isOccuppied) return false;
		Tile pushTo;
		switch (direction)
		{
			case NORTH: pushTo = target.North;
				break;
			case EAST: pushTo = target.East;
				break;
			case SOUTH: pushTo = target.South;
				break;
			case WEST: pushTo = target.West;
				break;
			default:
				throw new Exception(direction + " is not a valid input!");
		}
		if (pushTo == null) return false;
		if (pushTo.isOccuppied) return false;
		if (pushTo is WallTile) return false;
		if (!target.Unit.flight && pushTo is WaterTile) return false;
		target.Unit.MoveTo(pushTo);
		return true;
	}

	public int judgeAttack(Unit user, Unit target)
	{
		return 0;
	}

}
