using UnityEngine;
using System.Collections.Generic;

// this makes an object bob up and down in its local position.
public class Bob : MonoBehaviour {


	public float range = 0.1f;
	public float speed = 1f;
	
	private Vector3 startPos;
	private float time = 0f;

	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime * speed;
		transform.localPosition = new Vector3(startPos.x, startPos.y+(Mathf.Sin (time)*range), startPos.z);
	}
}
