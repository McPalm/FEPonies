/// <summary>
/// Camera control.
/// This script can be put anywhere on the scene to enable camera contols. On the camera would be a good idea
/// </summary>

using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	static public int BottomBorder = 100;

	// variables modifiable from Unity
	public float scrollSpeed = 1;
	public float xBorderScrollZone = 0.1f;
	public float yBorderScrollZone = 0.1f;
	public float zoomSpeed = 1.1f;
	public bool borderScroll = false;
	public bool enableLerp;
	
	[Range(1, 10)]
	public int Drag = 1;
	// private fields
	static private Vector3 _focusTarget;
	static private float _focusTimer = 1f;
	private const float _focusSpeed = 0.1f;
	private float _focusFrameDelay = 0f;
	private Vector3 _PressPosition;
	private Vector3 _Velocity;
	private Vector3 _PressScreenPoisition;
#if UNITY_ANDROID
	private float _startMagnitude = 0f;
	private float _startZoom = 1f;
	private int _lastFingerNumber = 0;
#endif

	// Update is called once per frame
	void Update () {

		Camera cam = Camera.main;
		if(Input.GetButtonDown("Zoom")){
			// Debug.Log("Zoom?");
			// cam.orthographicSize *= Input.GetAxis("Zoom")*Time.deltaTime;
			if(Input.GetAxis("Zoom") < 0){
				cam.orthographicSize *= zoomSpeed;
			}else{
				cam.orthographicSize /= zoomSpeed;
			}
		}else if(Input.GetAxis("Mouse ScrollWheel") != 0){
			// Debug.Log("Zoom?");
			// cam.orthographicSize *= Input.GetAxis("Zoom")*Time.deltaTime;
			if(Input.GetAxis("Mouse ScrollWheel") < 0){
				cam.orthographicSize *= zoomSpeed;
			}else{
				cam.orthographicSize /= zoomSpeed;
			}
		}

		Vector3 mousePos = Input.mousePosition;

		float mx = mousePos.x / Screen.width;
		float my = mousePos.y / Screen.height;

		if(StateManager.Instance.State == GameState.movingCamera){
			if(_focusFrameDelay > 0){
				cam.transform.position = _focusTarget * _focusSpeed + cam.transform.position * (1f-_focusSpeed);
				_focusTimer += 0.03f;
				_focusFrameDelay -= 0.03f;
			}
			_focusFrameDelay += Time.deltaTime;
			if(_focusTimer > 1f){
				StateManager.Instance.DebugPop();
			}
		}
		// Border Scrolling
		else if(borderScroll && mx > -0.015f && mx < 1.015f && my > -0.015f && my < 1.015f){
			Vector3 cameraPos = cam.transform.position;
			if(mx < xBorderScrollZone){
				//Debug.Log("left!");
				float dx = (xBorderScrollZone-mx)/xBorderScrollZone;
				cameraPos.x -= scrollSpeed*Time.deltaTime*dx;
			}else if(mx > 1f-xBorderScrollZone){
				// Debug.Log("right!");
				float dx = (mx-1f+xBorderScrollZone)/xBorderScrollZone;
				cameraPos.x += scrollSpeed*Time.deltaTime*dx;
			}if(my < yBorderScrollZone){
				// Debug.Log("up!");
				float dy = (yBorderScrollZone-my)/xBorderScrollZone;
				cameraPos.y -= scrollSpeed*Time.deltaTime*dy;
			}else if(my > 1f-yBorderScrollZone){
				// Debug.Log("down!");
				float dy = (my-1f+yBorderScrollZone)/yBorderScrollZone;
				cameraPos.y += scrollSpeed*Time.deltaTime*dy;
			}

			cam.transform.position = cameraPos;
		}

		if(StateManager.Instance.State != GameState.movingCamera){ // if moving camera, no player move camera!

#if UNITY_STANDALONE

			if(Input.GetMouseButtonDown(1)){
				// register where we press down the button.
				_PressPosition = MousePosition.Get();
				_Velocity = Vector3.zero;
				_PressScreenPoisition = Input.mousePosition;
			}else if(Input.GetMouseButton(1)){
				Vector3 _curr = _PressPosition - MousePosition.Get();
				cam.transform.position = cam.transform.position + _curr;
				_Velocity = (_Velocity+_curr/Time.deltaTime)/2;
			}else if(enableLerp){
				float momentum = 1 / Mathf.Pow(10, Drag);
				_Velocity *=  Mathf.Pow(momentum, Time.deltaTime);
				if(_Velocity.magnitude < 0.01f) _Velocity = Vector3.zero;
				cam.transform.position += _Velocity*Time.deltaTime;
			}
			if (Input.GetMouseButtonUp(1))
			{
				//check if we moved the mouse since pressing RMB
				if((_PressScreenPoisition - Input.mousePosition).magnitude < 45f)
				{
					// open character sheet

					if (Input.mousePosition.y > BottomBorder && MousePosition.GetTile().isOccuppied)
					{
						GameState s = StateManager.Instance.State;
						if(s == GameState.playerTurn ||s == GameState.unitSelected)
							BattleSheet.Instance.Open(MousePosition.GetTile().Unit);
					}
				}
			}
#endif
#if UNITY_EDITOR

			if(Input.GetMouseButtonDown(1)){
				// register where we press down the button.
				_PressPosition = MousePosition.Get();
			}else if(Input.GetMouseButton(1)){
				Vector3 _curr = _PressPosition - MousePosition.Get();
				cam.transform.position = cam.transform.position + _curr;
			}
#endif
#if UNITY_ANDROID


			// drag move
			if(Input.touchCount == 1)
			{
				if(_lastFingerNumber != 1){
					_PressPosition = MousePosition.Get();
					_Velocity = Vector3.zero;
				}else{
					Vector3 _curr = _PressPosition - MousePosition.Get();
					cam.transform.position = cam.transform.position + _curr;
					_Velocity = (_Velocity+_curr/Time.deltaTime)/2;
				}
			}
			// pinch zoom plus double drag move
			else if(Input.touchCount == 2){
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				float magnitude = (touchOne.position-touchZero.position).magnitude;
				if(_lastFingerNumber != 2){
					_startMagnitude = magnitude;
					_PressPosition = MousePosition.Get();
					_Velocity = (_Velocity+_curr/Time.deltaTime)/2;
				}else{
					Camera.main.orthographicSize = (Camera.main.orthographicSize * 3 +  _startZoom * _startMagnitude / magnitude) / 4;
					Vector3 _curr = _PressPosition - MousePosition.Get();
					cam.transform.position = cam.transform.position + _curr;
					_Velocity = (_Velocity+_curr/Time.deltaTime)/2;
				}
			}else{
				_startMagnitude = 0f;
				_startZoom = Camera.main.orthographicSize;
				if(enableLerp){
					float momentum = 1 / Mathf.Pow(10, Drag);
					_Velocity *=  Mathf.Pow(momentum, Time.deltaTime);
					if(_Velocity.magnitude < 0.01f) _Velocity = Vector3.zero;
					cam.transform.position += _Velocity*Time.deltaTime;
				}

			}
			_lastFingerNumber = Input.touchCount;
#endif
		}
		Constrain();
	}

	private void Constrain(){
		Camera cam = Camera.main;
		Vector3 cameraPos = cam.transform.position;

		float
			maxX = TileGrid.Instance.maxX - cam.orthographicSize * cam.aspect + 0.5f,
			minX = TileGrid.Instance.minX + cam.orthographicSize * cam.aspect - 0.5f,
			maxY = TileGrid.Instance.maxY - cam.orthographicSize + 0.5f,
			minY = TileGrid.Instance.minY + cam.orthographicSize - 0.5f - (float)BottomBorder / cam.pixelHeight * cam.orthographicSize * 2; // size of a tile in pixels

		if(maxX < minX){
			minX = maxX = (minX+maxX)/2;
		}if(maxY < minY){
			minY = maxY = (minY+maxY)/2;
		}

		cameraPos.x = (cameraPos.x > maxX) ? maxX : cameraPos.x;
		cameraPos.x = (cameraPos.x < minX) ? minX : cameraPos.x;
		cameraPos.y = (cameraPos.y > maxY) ? maxY : cameraPos.y;
		cameraPos.y = (cameraPos.y < minY) ? minY : cameraPos.y;
		cameraPos.z = -10;

		cam.transform.position = cameraPos;
	}

	/// <summary>
	/// Focus the camrea at the target position
	/// </summary>
	/// <param name="m">Position to move the camera to.</param>
	static public void FocusOn(Vector3 target){
		_focusTimer = 0f;
		_focusTarget = target;
		StateManager.Instance.Push(GameState.movingCamera);
	}

	/// <summary>
	/// Determines if the specified location is on-screen.
	/// </summary>
	/// <returns><c>true</c> if is the specified location is on-screen; otherwise, <c>false</c>.</returns>
	/// <param name="location">Location.</param>
	static public bool IsOnScreen(Vector3 location){
		Vector3 viewPos = Camera.main.WorldToViewportPoint(location);
		if (viewPos.x > 1f) return false;
		if (viewPos.x < 0f) return false;
		if (viewPos.y > 1f) return false;
		if (viewPos.y < 0f) return false;
		return true;
	}
}
