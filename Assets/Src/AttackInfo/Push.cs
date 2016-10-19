using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// An effect that pushes a unit one square in a given direction.
/// </summary>
public class Push : MonoBehaviour, IEffect {

	public const int NORTH = 1;
	public const int EAST = 2;
	public const int SOUTH = 3;
	public const int WEST = 4;

	public int Apply(DamageData attackData)
	{
		if (attackData.testAttack) return 0;
		// TODO, calculate direction using user.
		if (SmartStaticApply(attackData.target.Tile, attackData.source))
			return 1;
		return 0;
	}

    /// <summary>
    /// Push an enemy 1 tile away from the source of the attack.
    /// Does not properly support diagonals
    /// (But you can implement thatit if you want to)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="source"></param>
    /// <returns>true if the push happened.</returns>
    static public bool SmartStaticApply(Tile target, Component source)
	{
		Vector2 relative = source.transform.position - target.transform.position;
		if (relative.y > 0.5f) return StaticApply(target, SOUTH);
		else if (relative.y < -0.5f) return StaticApply(target, NORTH);
		else if (relative.x > 0.5f) return StaticApply(target, WEST);
		else return StaticApply(target, EAST);
		
	}

	/// <summary>
	/// Push a unit in target square in a given direction. but only if it can enter it.
	/// </summary>
	/// <param name="target"></param>
	/// <param name="direction, use the constants in this class to pick a direction"></param>
	static public bool StaticApply(Tile target, int direction)
	{
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
		if (!target.Unit.Character.Flight && pushTo is WaterTile) return false;
		target.Unit.MoveTo(pushTo); // implement test attack nonsense?
		return true;
	}

	public int judgeAttack(Unit user, Unit target)
	{
		return 0;
	}
}
