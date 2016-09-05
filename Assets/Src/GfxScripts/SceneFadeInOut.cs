using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

	static private SceneFadeInOut instance;
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private bool fadeToBlack = false;
	
	
	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		instance = this;
		transform.position = new Vector3(0.5f, 0.5f, 0);
	}
	
	
	void Update ()
	{
		// If the scene is starting...
		if(sceneStarting)
			// ... call the StartScene function.
			StartScene();
		else if(fadeToBlack)
			EndScene();
		else if(StateManager.Instance.State == GameState.fadeToBlack)
			StateManager.Instance.DebugPop();
	}
	
	
	void ClearLerp ()
	{
		// Lerp the colour of the texture between itself and transparent.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Mathf.Min(Time.deltaTime, 0.02f));
	}
	
	
	void BlackLerp ()
	{
		// Lerp the colour of the texture between itself and black.
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed *  Mathf.Min(Time.deltaTime, 0.02f));
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		ClearLerp();
		
		// If the texture is almost clear...
		if(GetComponent<GUITexture>().color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			
			// The scene is no longer starting.
			sceneStarting = false;
		}
	}

	void EndScene()
	{
		if(fadeToBlack){
			// fade to solid black
			BlackLerp();

			if(GetComponent<GUITexture>().color.a >= 0.95f){
				GetComponent<GUITexture>().color = Color.black;

				fadeToBlack = false;
			}
		}
	}


	static public void FadeToBlack(float duration = 1.5f)
	{
		StateManager.Instance.DebugPush(GameState.fadeToBlack);

		instance.fadeSpeed = duration;

		// Make sure the texture is enabled.
		instance.GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		instance.fadeToBlack = true;
		instance.sceneStarting = false;
	}

	static public void FadeToClear(float duration = 1.5f)
	{
		StateManager.Instance.DebugPush(GameState.fadeToBlack);

		instance.fadeSpeed = duration;
		
		// Make sure the texture is enabled.
		instance.GetComponent<GUITexture>().enabled = true;
		
		// Start fading towards black.
		instance.sceneStarting = true;
		instance.fadeToBlack = false;
	}
}