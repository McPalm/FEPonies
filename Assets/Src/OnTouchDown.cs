//	OnTouchDown.cs
//	Allows "OnMouseDown()" events to work on the iPhone(android).
//	Attack to the main camera.



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// this should simulate a mouse click if you tap the screen. That is toucht eh screen, and then release that touch rapidily afterwards.
public class OnTouchDown : MonoBehaviour
{
	#if UNITY_ANDROID
	private float _startTouch;

	void Start(){
		DontDestroyOnLoad(gameObject);

		// set framerate for andoids
		Application.targetFrameRate = 30;
	}

	void Update () {
		// Code for OnMouseDown in the iPhone. Unquote to test.
		if(Input.touchCount == 1){
			if(Input.GetTouch(0).phase.Equals(TouchPhase.Ended) && _startTouch < 0.3f){
				Vector3 poz = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				Tile t = TileGrid.Instance.GetTileAt(poz);
				if(t != null){
					t.SendMessage("Clicked");
				}
			}
			_startTouch += Time.deltaTime;
		}else{
			_startTouch = 0f;
		}
	}
	#endif
}


