using UnityEngine;
using System.Collections.Generic;

public class ConstantMove : MonoBehaviour {

	public Vector3 move;

	// Update is called once per frame
	void Update () {
		transform.localPosition += move * Time.deltaTime;
	}
}
