using UnityEngine;
using System.Collections.Generic;

public class GrowIn : MonoBehaviour {

	public float duration = 1f;

	private float _time = 0f;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		_time += Time.deltaTime/duration;
		transform.localScale = new Vector3(_time, _time, _time);
		if(_time > 1f){
			transform.localScale = new Vector3(1, 1, 1);
			Destroy(this);
		}
	}
}
