using UnityEngine;
using System.Collections.Generic;

public class SimpleAnimateType : MonoBehaviour {

	public int type;
	
	// Use this for initialization
	void Update () {
		GetComponent<Animator>().SetInteger("type", type);
	}
}
