using UnityEngine;
using System.Collections.Generic;

public class MageSlayer : Skill {


	public override string Name {
		get {
			return "Mageslayer";
		}
	}

	// Use this for initialization
	void Start () {
		Unit u = GetComponent<Unit>();

		Stats s = new Stats();
		s.resistance = 3;
		new DurationBuff(-1, s, u);

		IEffect e = u.AttackInfo.effect;
		DamageType t = e.damageType;
		t.MageSlayer = true;
		e.damageType = t;


	}
}
