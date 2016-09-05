using UnityEngine;
using System.Collections;

public interface ITargetFilter{

	/// <summary>
	/// Used to filter if a target is a valid target. Such as enemy, ally or unoccupied tile or any other algorithm.
	/// </summary>
	/// <returns><c>true</c>, if target was validated, <c>false</c> otherwise.</returns>
	/// <param name="target">Target tile.</param>
	/// <param name="user">User.</param>
	bool ValidateTarget(Tile target, Unit user);
}
