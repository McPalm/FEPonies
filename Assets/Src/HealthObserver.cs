using UnityEngine;
using System.Collections;


// use this to observe the health change of a single unit.
// send the implementing obserer to <Unit>.RegisterHealthObserver(this);
// no unregister is implemented yet, cuz Im a bum!
public interface HealthObserver{
	/// <summary>
	/// Notifies the health.
	/// </summary>
	/// <param name="unit">Unit whos health was changed.</param>
	/// <param name="change">Change in health.</param>
	void NotifyHealth(Unit unit, int change);
}
