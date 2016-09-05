using UnityEngine;
using System.Collections;
/// <summary>
/// Ability.
/// Override class when creating an ability. you MUST make use if the FinishUse() function when completing this ability.
/// </summary>
public abstract class Ability : Skill {

	public abstract void Use();

	/// <summary>
	/// You need to call this when you are done using the ability!
	/// ...unless you dont want to spend your turn...
	/// </summary>
	protected void FinishUse(){
		Unit.SelectedUnit.FinnishMovement();
	}
}
