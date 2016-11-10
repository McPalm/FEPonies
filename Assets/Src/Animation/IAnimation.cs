using UnityEngine;
using System.Collections;

/// <summary>
/// Interface for handling animtations for attacks etc etc. Has two in data.
/// an Unit.ApplyEffect() call has to be made at some point during the animation! This is the moment when the damage is applied..  or effect, of the spell.
/// </summary>
public interface IAnimation{
	/// <summary>
	/// Animate an attack with the specified source and target. Both source and target is nullable, be carefull when using them.
	/// Enters the GameState.unitAttack when called, and exits it when the animation is completed.
	/// </summary>
	/// <param name="source">Source, unit who peforms the attack/cast the spell etc.</param>
	/// <param name="target">Target.</param>
	void Animate(Unit source, Tile target, System.Action<Tile> tile, bool hit=true);

	/// <summary>
	/// Stops an animation from play enough to pop the animation state.
	/// Some sfx may still persist.
	/// </summary>
	void Cancel();
}
