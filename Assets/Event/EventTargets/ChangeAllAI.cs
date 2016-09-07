using UnityEngine;
using System.Collections;

public class ChangeAllAI : EventTarget {

	public AITypes changeTo = AITypes.Aggressive;
	public int team = 1;
	public bool includingBosses = false;

	public override void Notice ()
	{
		// find all enemy units on field.
		foreach(Unit u in UnitManager.Instance.GetUnitsByTeam(team)){
			if(u.isBoss &! includingBosses) continue;
            AIUtility.AITypeChanger(u, changeTo);
		}



	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
