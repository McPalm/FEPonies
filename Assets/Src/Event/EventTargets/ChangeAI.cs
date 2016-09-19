/// <summary>
/// Event target that change the AI of the unit it sits on.
/// </summary>


using UnityEngine;
using System.Collections;

public class ChangeAI : EventTarget {



	public AITypes newAI;

	public override void Notice ()
	{
		if(this){
			// finds the Unit it sits on.
			Unit u = gameObject.GetComponent<Unit>();
			switch(newAI){
			case AITypes.Aggressive :
				u.ChangeAI(gameObject.AddComponent<Aggressive>());
				break;
			case AITypes.Defensive :
				u.ChangeAI(gameObject.AddComponent<Defensive>());
				break;
			case AITypes.Stationary :
				u.ChangeAI(gameObject.AddComponent<Stationary>());
				break;
			}
		}
	}
}
