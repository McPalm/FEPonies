using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	/// <summary>
	/// Add this to the level of ALL enemies across all levels. makes the game harder. Keep at zero please...  for now...
	/// </summary>
	static public int levelBonus = 0;
	int level = 0;

	void OnGUI(){


		if(GUI.Button( new Rect (Screen.width/4, Screen.height/24*5, Screen.width/2, Screen.height/12), "New Game")){
			PlayerPrefs.DeleteAll();
			SaveFile.Active.SaveCheckpoint(1);
			LevelManager.Instance.LoadFromCheckpoint();
		}
		if(GUI.Button( new Rect(Screen.width/4, Screen.height/24*8, Screen.width/2, Screen.height/12), "Continue")){
			LevelManager.Instance.LoadFromCheckpoint();
		}
		level = int.Parse(GUI.TextField( new Rect(Screen.width/4, Screen.height/24*11, 200, Screen.height/12), "" + level));

		if(GUI.Button( new Rect(Screen.width/4+210, Screen.height/24*11, Screen.width/2-200, Screen.height/12), "Start At Level " + level)){
			SaveFile.Active.LoadCheckpoint();
			SaveFile.Active.SaveCheckpoint(level);
			LevelManager.Instance.LoadFromCheckpoint();
		}

		if(GUI.Button( new Rect(Screen.width - 200, Screen.height-50, 100, 30), "Reset")){ // HACK so much fucking remove this!
			PlayerPrefs.DeleteAll();
		}


	}

	void Awake(){
		if(SaveFile.Active == null){
			new SaveFile();
		}
	}

	public void ShowLevel(int x, int y, string name){
		GUI.Box(new Rect(x, y, 100, 60), name);
		GUI.Label(new Rect(x+20, y+34, 80, 30), "Level " + SaveFile.Active.GetLevel(name));
	}
}
