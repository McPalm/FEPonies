using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	/// <summary>
	/// Add this to the level of ALL enemies across all levels. makes the game harder. Keep at zero please...  for now...
	/// </summary>
	static public int levelBonus = 0;

	void OnGUI(){


		if(GUI.Button( new Rect (Screen.width/4, Screen.height/24*5, Screen.width/2, Screen.height/12), "New Game")){
            Story.Instance.Checkpoint = LevelDB.Instance.GetFirstLevel();
            Story.Instance.Save();
			LevelManager.Instance.LoadFromCheckpoint();
		}
		if(GUI.Button( new Rect(Screen.width/4, Screen.height/24*8, Screen.width/2, Screen.height/12), "Continue")){
            Story.Instance.Load();
			LevelManager.Instance.LoadFromCheckpoint();
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
