using UnityEngine;
using System.Collections.Generic;

abstract public class AbilityWithManacost : Ability {

	public abstract int ManaCost{
		get;
	}

	new protected void FinishUse(){
		GetComponent<Mana>().ManaRemaining-= ManaCost;
		Unit.SelectedUnit.FinnishMovement();
	}

	protected void OOM(){
		GUInterface.Instance.PrintMessage("Not enough Mana!");
	}
}
