using UnityEngine;
using System.Collections.Generic;

// this units attacks are magical!
public class MagicAttacks : MonoBehaviour {

	// Use this for initialization
	void Start () {
		IEffect e = GetComponent<Unit>().AttackInfo.effect;
		DamageType t = e.damageType;
		t.Magic = true;
		e.damageType = t;
	}
}
