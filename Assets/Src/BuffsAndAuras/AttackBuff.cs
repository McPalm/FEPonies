using UnityEngine;
using System.Collections;

/// <summary>
/// Registered on the Unit with the AttackBuff.
/// Call unit.RegisterAttackBuff(this);
/// </summary>
public interface AttackBuff{

	/// <summary>
	/// Find out whenever or not the attack applies
	/// This needs to be consistent, no random variables in this function.
	/// </summary>
	/// <param name="target"></param>
	/// <param name="where the unit stands when making the attack."></param>
	/// <returns></returns>
	bool Applies(Unit target, Tile source, Tile targetLocation);

	/// <summary>
	/// The stat buff if it applies.
	/// </summary>
	Stats Stats{
		get;
	}

}
