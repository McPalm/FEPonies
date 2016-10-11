using UnityEngine;
using System.Collections.Generic;
using System;


/// <summary>
/// Duelist.
/// Increased dodge and damage and crit when you are getting attacked.
/// </summary>
public class Riposte : Passive, AttackBuff{


	private Unit _unit;
	private Stats _buff;
	private bool _autoCrit = false;

	public Stats Stats {
		get {
			return _buff;
		}
	}

	public override string Name {
		get {
			return "Riposte";
		}
	}

	void calcBuff(){
		_buff = new Stats();
		_buff.hitBonus = 1f;
		_buff.critBonus = 1f;
	}

	// Use this for initialization
	void Start () {
		_unit = GetComponent<Unit>();
		if (_unit)
		{
			calcBuff();
			_unit.RegisterAttackBuff(this);
		}
	}

	void OnDodgeAttack(Unit other){
		_autoCrit = true;
	}

	void FinishedAttackSequence(Unit u){
		_autoCrit = false;
	}

	void OnDamageDealt(int damage){
		_autoCrit = false;
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		return _autoCrit;
	}
}
