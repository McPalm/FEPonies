using UnityEngine;
using System.Collections;

public interface IEffect{
	/// <summary>
	/// Apply an effect to the target tile.
	/// </summary>
	/// <param name="target">Target tile for the effect.</param>
	/// <param name="user">User of this effect, not neccecary for all implementations.</param>
	void Apply(Tile target, Unit user);

	DamageType damageType{
		set;
		get;
	}

	int Apply(Tile target, Unit user, bool testAttack);
    int Apply(Tile target, Unit user, bool testAttack, Tile testTile);
}
