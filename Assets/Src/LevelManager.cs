using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private bool _inevitable = false;
    
    static private LevelManager instance;

	static public LevelManager Instance{
		get{
			if(instance == null){
				new GameObject("LevelManager").AddComponent<LevelManager>();
			}
			return instance;
		}
	}

    public bool isLoaded = false;

    // fields
    private string nextLevel;
	private float aniDuration;
	private string msg;
	private bool fadeToBlack = false;

	void Awake(){
		if(instance == null){
			instance = this;
			SceneManager.sceneLoaded += SceneLoaded;
			DontDestroyOnLoad(gameObject);
		}else{
			Destroy(gameObject);
		}
	}

	void SceneLoaded(Scene scene, LoadSceneMode m)
	{
		isLoaded = true;
    }

    void Update(){
		if(StateManager.Instance.State == GameState.LevelManagerAnimation){
			aniDuration-= Time.deltaTime;
		}else if(_inevitable)
			aniDuration-= Time.deltaTime*0.02f;
	}

	void OnGUI(){
		if(StateManager.Instance.State == GameState.LevelManagerAnimation){

			if(aniDuration < 0){
				if(fadeToBlack){
					SceneFadeInOut.FadeToBlack();
					fadeToBlack = false;
				}else{
					StateManager.Instance.DebugPop();
					GotoNextLevel();
				}
			}

			GUI.Box(new Rect(Screen.width/2-50, Screen.height/2-12, 100, 25), msg);

		}
	}

	/// <summary>
	/// Win the current level. Saves the checkpoint and prepares for next level.
	/// </summary>
	public void Win(){
		// goes to next level, if there is any, or return ot the main menu.
		StateManager.Instance.Clear();
		nextLevel = LevelDB.Instance.GetNextLevel(nextLevel);
		if(nextLevel == "-1") nextLevel = LevelDB.Instance.GetMainMenu();
        else Story.Instance.Checkpoint=nextLevel;
        Story.Instance.Save();
        StateManager.Instance.DebugPush(GameState.LevelManagerAnimation);
		aniDuration = 2f;
		msg = "VICTORY!";
		_inevitable = true;
		fadeToBlack = true;
	}
	/// <summary>
	/// Fail the current level. Reverts to latest checkpoint.
	/// </summary>
	public void Lose(){
		// return to main menu
		StateManager.Instance.Clear();
		StateManager.Instance.DebugPush(GameState.LevelManagerAnimation);
		aniDuration = 2f;
		msg = "DEFEAT!";
		nextLevel = LevelDB.Instance.GetMainMenu();
		_inevitable = true;
		fadeToBlack = true;
	}

	public void LoadFromCheckpoint(){
        nextLevel = Story.Instance.Checkpoint;
		GotoNextLevel();
	}

	public void MainMenu(bool defeatMessage = true){
        // goes to the main menu!
        nextLevel = LevelDB.Instance.GetMainMenu(); ;
		if(defeatMessage){
			aniDuration = 2f;
			msg = "DEFEAT!";
		}
		StateManager.Instance.DebugPush(GameState.LevelManagerAnimation);
	}
	/// <summary>
	/// Starts the battle from an intermission
	/// </summary>
	public void StartBattle(){
		nextLevel = LevelDB.Instance.GetNextLevel(nextLevel);
        if (nextLevel == "-1") nextLevel = LevelDB.Instance.GetMainMenu();
        StateManager.Instance.DebugPush(GameState.LevelManagerAnimation);
		aniDuration = 0f;
		SceneFadeInOut.FadeToBlack();
		//GotoNextLevel();
	}

	private void GotoNextLevel(){
		_inevitable = false;
		StateManager.Instance.Clean();
		BuffManager.Instance.Clear();
        isLoaded = false;
		SceneManager.LoadScene(nextLevel);
        if (LevelDB.Instance.isBattleLevel(nextLevel))
        {
            SceneManager.LoadScene("BattleGUIScene", LoadSceneMode.Additive);
        }
	}
}
