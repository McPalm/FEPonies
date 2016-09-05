using UnityEngine;
using System.Collections;

public class ArmourPiercing : Passive {


	public override string Name {
		get {
			return "Armour Piercing Attacks!";
		}
	}

	// Use this for initialization
	void Start () {
		Unit u = GetComponent<Unit>();

		DamageType d =	u.AttackInfo.effect.damageType;
		d.ArmourPiercing = true;
		u.AttackInfo.effect.damageType = d;
	}
}
