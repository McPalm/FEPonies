using UnityEngine;
using System.Collections.Generic;

public class Spin : MonoBehaviour {

	[Range(-10f, 10f)]
	public float rotationsPerSecond = 0f;
	public bool randomDirection = false;


	private float _time = 0f;
	private bool _flip;

	void Start(){
		_flip = randomDirection && Random.Range(0, 2) > 0;
	}

	// Update is called once per frame
	void Update () {

		if(_flip) _time -= Time.deltaTime;
		else _time += Time.deltaTime;

		transform.localRotation = Quaternion.AngleAxis(_time*360f*rotationsPerSecond, Vector3.forward);

	}
}
