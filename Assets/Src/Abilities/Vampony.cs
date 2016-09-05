using UnityEngine;
using System.Collections;

public class Vampony : Passive {

	public override string Name {
		get {
			return "Vampony";
		}
	}

	public void OnDamageDealt(int value){
		int i = (value+1)/2;
		if(i > 0){
			GetComponent<Unit>().Heal(i);
		}
	}
}
