using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


	int state = TITLE;

	const int TITLE = 0;
	const int MAIN = 1;

	private const int LEFT = 1;
	private const int CENTRE = 2;
	private const int RIGHT = 3;
	private const int UP = 4;
	private const int DOWN = 5;
	private const int DOWNRIGHT = 6;
	private const int TOPLEFT = 7;

	public AutoLerp Title;
	public AutoLerp TitleMenu;

	void Awake(){
		if(SaveFile.Active == null){
			new SaveFile();
		}
	}

	void Update()
	{
		if(state == TITLE && Input.anyKeyDown) ShowTitleMenu();
		else if(state == MAIN && Input.GetButtonDown("Cancel")) ShowTitleScreen();
	}

	public void NewGame()
	{
		Story.Instance.Checkpoint = LevelDB.Instance.GetFirstLevel();
		Story.Instance.Save();
		LevelManager.Instance.LoadFromCheckpoint();
	}

	public void Load()
	{
		Story.Instance.Load();
		LevelManager.Instance.LoadFromCheckpoint();
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void ShowOptions()
	{

	}

	public void ShowTitleMenu()
	{
		move(Title, UP);
		move(TitleMenu, CENTRE);
		state = MAIN;
	}

	public void ShowTitleScreen()
	{
		move(Title, CENTRE);
		move(TitleMenu, DOWN);
		state = TITLE;
	}

	void move(AutoLerp what, int where)
	{
		Vector3 centre = transform.position;
		Vector3 destination = Vector3.zero; 
		switch (where)
		{
			case LEFT:
				destination = centre - new Vector3(Camera.main.pixelWidth, 0f, 0f);
				break;
			case RIGHT:
				destination = centre + new Vector3(Camera.main.pixelWidth, 0f, 0f);
				break;
			case CENTRE:
				destination = centre;
				break;
			case UP:
				destination = centre + new Vector3(0f, Camera.main.pixelHeight, 0f); ;
				break;
			case DOWN:
				destination = centre - new Vector3(0f, Camera.main.pixelHeight, 0f); ;
				break;
			case TOPLEFT:
				destination = centre + new Vector3(-Camera.main.pixelWidth, Camera.main.pixelHeight, 0f); ;
				break;
			case DOWNRIGHT:
				destination = centre + new Vector3(Camera.main.pixelWidth, -Camera.main.pixelHeight, 0f); ;
				break;
		}
		print(what);
		what.Lerp(destination);
	}
}
