using UnityEngine;
using System.Collections.Generic;

public class Wingslayer : Passive, Buff {

	private Stats buff;
	private bool _active = false;
	private Unit host;

	// Use this for initialization
	void Start () {
		IEffect e = GetComponent<Unit>().AttackInfo.effect;
		DamageType t = e.damageType;
		t.AntiAir = true;
		e.damageType = t;

		buff = new Stats();
		buff.hitBonus = 0.3f;
		host = GetComponent<Unit>();

		BuffManager.Instance.Add(this);
	}

	public bool Affects (Unit u)
	{
		return _active && u == host;
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

	void StartingAttackSequence(Unit target){
		_active = target.flight;
	}

	void FinishedAttackSequence(Unit u){
		_active = false;
	}

}
