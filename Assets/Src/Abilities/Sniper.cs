using UnityEngine;
using System.Collections;

public class Sniper : Passive {

	public override string Name {
		get {
			return "Sniper!";
		}
	}

	// Use this for initialization
	void Start () {
		Unit u = GetComponent<Unit>();
		u.AttackInfo.Reach = new IncreasedRange();
	}

}
