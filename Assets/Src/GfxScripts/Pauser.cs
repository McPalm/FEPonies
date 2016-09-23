using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {

	static private Pauser instance;
	static public Pauser Instance{
		get{
			if(instance == null){
				GameObject go = new GameObject("Pauser");
				instance = go.AddComponent<Pauser>();
			}
			return instance;
		}
	}

	public bool lockme = false;

	void Update(){
		if(Input.GetButtonDown("Cancel")){
			if(StateManager.Instance.State == GameState.paused){
				UnPause();
			}else if(CanPause){
				Pause();
			}
		}
		lockme = false;
	}



	void OnGUI(){
		if(StateManager.Instance.State == GameState.paused){

			int starty = Screen.height/2-150;
			int startx = Screen.width/2-150;
			GUI.Label(new Rect (Screen.width/2-25, starty, 50, 300), "Paused");

			if(GUI.Button (new Rect(startx+20, starty+30, 260, 30), "Unpause")){
				UnPause();
			}
			if(GUI.Button (new Rect(startx+20, starty+70, 260, 30), "Main Menu")){
				LevelManager.Instance.MainMenu();
			}
		}
	}




	public void Pause(){
		StateManager.Instance.DebugPush(GameState.paused);
	}

	public void UnPause(){
		if(StateManager.Instance.State == GameState.paused){
			StateManager.Instance.DebugPop();
		}
	}

	public bool CanPause{
		get{
			GameState gs = StateManager.Instance.State;
			if (gs == GameState.unitSelected) return false; // cancel used in this state
			if (gs == GameState.characterSheet) return false; // cancel used in this state
			if (lockme) return false;
			return true;
		}
	}
}
