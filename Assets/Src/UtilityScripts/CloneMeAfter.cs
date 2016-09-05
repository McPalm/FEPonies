using UnityEngine;
using System.Collections.Generic;

public class CloneMeAfter : MonoBehaviour {
	
	public float time = 0.2f;

	private float elapsedTime = 0f;

	public Vector3 offset;
	public int clones = 5;

	public bool compensateForLag = true;


	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= time && clones > 0){
			clones--;
			float comp = 1f;
			if(compensateForLag){
				comp = comp / elapsedTime * time;
			}

			Instantiate(gameObject, transform.position + offset * comp, transform.localRotation);
			Destroy(this);
		}
	}
}
