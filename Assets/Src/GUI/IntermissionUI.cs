using UnityEngine;
using System.Collections;
using System;

public class IntermissionUI : MonoBehaviour, Observer {


	private const int ROOT = 0;
	private const int ROSTER = 1;
	private const int SAVE = 2;
	private const int LOAD = 3;
	private const int STATBLOCK = 4;
	private const int TALENTS = 5;

	private const int LEFT = 1;
	private const int CENTRE = 2;
	private const int RIGHT = 3;
	private const int UP = 4;
	private const int DOWN = 5;
	private const int DOWNRIGHT = 6;
	private const int TOPLEFT = 7;

	private int state = 0;

	public AutoLerp RootMenu;
	public AutoLerp RosterMenu;
	public AutoLerp Sheet;
	public AutoLerp Talents;

	private RosterMenu rosterMenu;
	private CharacterSheet characterSheet;

	RectTransform me;

	// Use this for initialization
	void Awake () {
		me = GetComponent<RectTransform>();
	}

	void Start()
	{
		rosterMenu = RosterMenu.GetComponent<RosterMenu>();
		rosterMenu.registerObserver(this);
		characterSheet = Sheet.GetComponent<CharacterSheet>();
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetButtonDown("Cancel")) Cancel();
	}

	public void ClickRoster()
	{
		if(state == ROOT)
		{
			roster();
		}
	}

	public void Cancel()
	{
		switch (state)
		{
			case ROSTER:
				root();
				break;
			case STATBLOCK:
			case TALENTS:
				roster();
				break;
		}
	}

	public void CharacterClick(int index)
	{
		if(state == ROSTER || state == TALENTS)
		{
			sheet();
		}
	}

	public void TalentScreen(int index)
	{
		if(state == STATBLOCK || state == ROSTER)
		{
			levelUp();
		}
	}

	void move(AutoLerp what, int where)
	{
		Vector3 destination = Vector3.zero;
		switch (where)
		{
			case LEFT:
				destination = me.position - new Vector3(Camera.main.pixelWidth, 0f, 0f);
				break;
			case RIGHT:
				destination = me.position + new Vector3(Camera.main.pixelWidth, 0f, 0f);
				break;
			case CENTRE:
				destination = me.position;
				break;
			case UP:
				destination = me.position + new Vector3(0f, Camera.main.pixelHeight, 0f); ;
				break;
			case DOWN:
				destination = me.position - new Vector3(0f, Camera.main.pixelHeight, 0f); ;
				break;
			case TOPLEFT:
				destination = me.position + new Vector3(-Camera.main.pixelWidth, Camera.main.pixelHeight, 0f); ;
				break;
			case DOWNRIGHT:
				destination = me.position + new Vector3(Camera.main.pixelWidth, -Camera.main.pixelHeight, 0f); ;
				break;
		}
		what.Lerp(destination);
	}


	void levelUp()
	{
		state = TALENTS;
		move(Talents, CENTRE);
		move(Sheet, UP);
		move(RosterMenu, TOPLEFT);
	}

	void sheet()
	{
		state = STATBLOCK;
		move(RosterMenu, LEFT);
		move(Sheet, CENTRE);
		move(Talents, DOWN);
	}

	void roster()
	{
		state = ROSTER;
		move(RosterMenu, CENTRE);
		move(RootMenu, LEFT);
		move(Sheet, RIGHT);
		move(Talents, DOWNRIGHT);
	}

	void root()
	{
		state = ROOT;
		move(RosterMenu, RIGHT);
		move(RootMenu, CENTRE);
	}

	public void Notify()
	{
		characterSheet.Build(rosterMenu.Character);
		sheet();
	}
}
