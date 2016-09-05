using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Large Unit
/// Put this on units to decrease their dodge.. but also slightly boost base damage and hit.
/// </summary>
public class Large : MonoBehaviour {

	DurationBuff db;

	// Use this for initialization
	void Start () {
		Stats buff = new Stats();
		buff.dodgeBonus = -0.2f;
		buff.baseAttackMod = 0.1f;
		buff.hitBonus = 0.1f;

		db = new DurationBuff(-1, buff, gameObject.GetComponent<Unit>());
	}
	
	// Update is called once per frame
	void OnDestroy(){
		db.Destroy();
	}
}
