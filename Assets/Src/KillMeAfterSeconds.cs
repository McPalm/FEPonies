using UnityEngine;
using System.Collections;

public class KillMeAfterSeconds : MonoBehaviour {

	[Range(0.1f, 10f)]
	public float seconds = 1f;
	private int frames;
	private int currentFrame = 0;

	void Start(){
		frames = (int)(seconds*50f);
		if(frames < 0) frames = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(currentFrame == frames)
			Destroy(gameObject);
		currentFrame++;
	}
}
