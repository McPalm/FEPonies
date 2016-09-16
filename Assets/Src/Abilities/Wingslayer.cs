using UnityEngine;
using System.Collections.Generic;
using System;

public class Wingslayer : Passive, AttackBuff {

	private Stats buff;
	private bool _active = false;
	private Unit host;

	// Use this for initialization
	void Start () {

		buff.hitBonus = 0.3f;
		host = GetComponent<Unit>();
		buff.might = host.level / 2 + 3;
		host.RegisterAttackBuff(this);
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		return target.flight;
	}

	public Stats Stats {
		get{
			return buff;
		}
	}

	public override string Name {
		get {
			return "Wingslayer";
		}
	}
}
