using UnityEngine;
using System.Collections.Generic;

// used to make each succesfull clone created by the CloneMeAfter fade out

public class GravityParticleFade : MonoBehaviour {

	public int max;



	// Use this for initialization
	void Start () {
		int gag = GetComponent<CloneMeAfter>().clones;

		float alpha = (float)gag / (float)max;

		GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
