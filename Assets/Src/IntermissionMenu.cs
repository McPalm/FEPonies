using UnityEngine;
using System.Collections.Generic;

public class IntermissionMenu : MonoBehaviour {

	private subMenu _sub = subMenu.Root;
	private List<Unit> _rooster;
	private int _zeroIndex = 0;
	private string _selectedPony;
	private string[] _skills;
	private string _selectedSkill = "";
	private string _skillDescription = "select ability for details";
	private List<string> _learnedSkills;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(StateManager.Instance.State == GameState.dialogue) return;
		switch(_sub){
		case subMenu.Root:
			DrawRoot();
			break;
		case subMenu.Rooster:
			DrawRooster();
			break;
		case subMenu.SkillOverview:
			DrawSkillOverview();
			break;
		case subMenu.ViewPonySkills:
			DrawPonySkillWindow();
			break;
		}

	}

	private void DrawRoot(){
		int anchorX = Screen.width/2-150;
		int anchorY = Screen.height/2-150;
		// continue
		if(GUI.Button(new Rect(anchorX, anchorY, 300, 30), "Next Battle!")){
			LevelManager.Instance.StartBattle();
		}
		// view stats
		if(GUI.Button(new Rect(anchorX, anchorY += 40, 300, 30), "My little Ponies")){
			_rooster = SaveFile.Active.GetUnlockedCharacters();
			_sub = subMenu.Rooster;
			_zeroIndex = 0;
		}
		//
		if(GUI.Button(new Rect(anchorX, anchorY += 40, 300, 30), "Skills")){
			_rooster = SaveFile.Active.GetUnlockedCharacters();
			_sub = subMenu.SkillOverview;
			_zeroIndex = 0;
		}
		// exit to main
		if(GUI.Button(new Rect(anchorX, anchorY += 40, 300, 30), "Main Menu")){
			LevelManager.Instance.MainMenu(false);
		}
	}

	private void DrawRooster(){
		int rootX = Screen.width/2-300;
		int rootY = 60;

		// calc capacity
		int cap = Screen.height;
		cap -= rootY + 30 + 60 + 30; // top padding, header, bot padding, bot button
		cap /= 40;

		// boundingBox
		GUI.Box(new Rect(rootX-5, rootY, 610, cap*40+50), "");

		// heading
		DrawRoosterCell(rootX, rootY, "  Pony", "Lvl.", "HP", "Atk.", "Str.", "Agi.", "Int.", "Def.", "Res.", "Hit", "Dodge", false);

		// list with character
		rootY += 30;
		SaveFile.Active.UpdateUnit(_rooster);
		for(int i = 0; i+_zeroIndex < _rooster.Count && i < cap; i++){
			DrawRoosterCell(rootX, rootY+40*(i), _rooster[i+_zeroIndex]);
		}
		// scrollbar if needed
		if(cap < _rooster.Count){
			// show scroll buttons
			if(GUI.Button(new Rect(rootX+630, rootY, 30, 60), "^")) _zeroIndex--;
			if(GUI.Button(new Rect(rootX+630, rootY + cap*40-70, 30, 60), "v")) _zeroIndex++;
			// constrain so we wont go OOB
			if(_zeroIndex < 0) _zeroIndex = 0;
			if(_zeroIndex+cap > _rooster.Count) _zeroIndex--;
			
		}

		// cancel button in the bottom.
		if(GUI.Button(new Rect(Screen.height/2-100, Screen.height-60, 200, 30), "Back")){
			_sub = subMenu.Root;
		}
	}

	private void DrawRoosterCell(int x, int y, Unit u){
		DrawRoosterCell(x, y, u.name, u.level.ToString(), u.ModifiedStats.maxHP.ToString(),
		                u.AttackStat.ToString(), u.ModifiedStats.strength.ToString(), u.ModifiedStats.agility.ToString(), u.ModifiedStats.intelligence.ToString(),
		                u.ModifiedStats.defense.ToString(), u.ModifiedStats.resistance.ToString(),
		                (Mathf.Round(u.ModifiedStats.Hit*100f) + "%").ToString(), (Mathf.Round(u.ModifiedStats.Dodge*100f) + "%").ToString(), 
		                true);
	}

	private void DrawRoosterCell(int x, int y, string name, string level, string hitpoints, string attack, string str, string agi, string inte, string defence, string resistance, string hit, string dodge, bool frame){
		if(frame) GUI.Button(new Rect(x, y, 600, 30), "");
		int margain = x+10;
		GUI.Label(new Rect(margain, y+3, 100, 26), name);
		GUI.Label(new Rect(margain+=100, y+3, 30, 26), level);
		GUI.Label(new Rect(margain+=30, y+3, 30, 26), hitpoints);
		GUI.Label(new Rect(margain+=30, y+3, 30, 26), attack);
		GUI.Label(new Rect(margain+=45, y+3, 30, 26), str);
		GUI.Label(new Rect(margain+=30, y+3, 30, 26), agi);
		GUI.Label(new Rect(margain+=30, y+3, 30, 26), inte);
		GUI.Label(new Rect(margain+=45, y+3, 30, 26), defence);
		GUI.Label(new Rect(margain+=30, y+3, 30, 26), resistance);
		GUI.Label(new Rect(margain+=42, y+3, 37, 26), hit);
		GUI.Label(new Rect(margain+=42, y+3, 37, 26), dodge);
		//GUI.Label(new Rect(margain+=30, y+3, 100, 26), item);
	}

	private void DrawSkillOverview(){
		int rootX = Screen.width/2-300;
		int rootY = 60;
		
		// calc capacity
		int cap = Screen.height;
		cap -= rootY + 30 + 60 + 30; // top padding, header, bot padding, bot button
		cap /= 40;
		
		// boundingBox
		GUI.Box(new Rect(rootX-5, rootY, 610, cap*40+50), "");
		
		// heading
		//DrawRoosterCell(rootX, rootY, "Name", "Lvl.", "HP", "Atk.", "Def.", "Res.", "???", false);
		DrawSkillCell(rootX, rootY, "  Pony", "Available Skillpoints", false);

		// list with character
		SaveFile sv = SaveFile.Active;
		rootY += 30;
		SaveFile.Active.UpdateUnit(_rooster);
		for(int i = 0; i+_zeroIndex < _rooster.Count && i < cap; i++){
			string name = _rooster[i+_zeroIndex].name;
			if(DrawSkillCell(rootX, rootY+40*(i), name, "    " + sv.skillpointsAvailable(name).ToString())){
				_selectedPony = name;
				_sub = subMenu.ViewPonySkills;
				_skills = SkillDB.GetSkills(name);
				_zeroIndex = 0;
				_skillDescription = "select ability for details";
				_learnedSkills = new List<string>(SaveFile.Active.GetAbilities(name));
				break;
			}
		}
		// scrollbar if needed
		if(cap < _rooster.Count){
			// show scroll buttons
			if(GUI.Button(new Rect(rootX+630, rootY, 30, 60), "^")) _zeroIndex--;
			if(GUI.Button(new Rect(rootX+630, rootY + cap*40-70, 30, 60), "v")) _zeroIndex++;
			// constrain so we wont go OOB
			if(_zeroIndex < 0) _zeroIndex = 0;
			if(_zeroIndex+cap > _rooster.Count) _zeroIndex--;
			
		}
		
		// cancel button in the bottom.
		if(GUI.Button(new Rect(Screen.height/2-100, Screen.height-60, 200, 30), "Back")){
			_sub = subMenu.Root;
			_selectedSkill = "";
		}
	}

	private bool DrawSkillCell(int x, int y, string name, string skillpoints, bool button = true){
		if(button)if(GUI.Button(new Rect(x, y, 600, 30), "")) return true;
		int margain = x+10;
		GUI.Label(new Rect(margain, y+3, 150, 26), name);
		GUI.Label(new Rect(margain+= 150, y+3, 200, 26), skillpoints);
		return false;
	}

	private void DrawPonySkillWindow(){
		int anchorX = Screen.width/2-300;
		int anchorY = 100;
		// pony name at top!
		GUI.Box(new Rect(anchorX, anchorY-40, 600, 30), "");
		DrawSkillCell(anchorX, anchorY-40, _selectedPony, SaveFile.Active.skillpointsAvailable(_selectedPony).ToString(), false);

		// calc cap
		int cap = Screen.height;
		cap -= anchorY + 30 + 60 + 30; // top padding, header, bot padding, bot button
		cap /= 40;

		// get available skills
		for(int i = 0; i+_zeroIndex < _skills.Length && i < cap; i++){
			string skill = _skills[i+_zeroIndex];
			string preformat = "";
			string postformat = "";
			if(skill == _selectedSkill){preformat += "<b>"; postformat += "</b>";}
			if(_learnedSkills.Contains(skill)){preformat += "<color=#ffa500ff>"; postformat = "</color>" + postformat;}
			if(GUI.Button(new Rect(anchorX+410, anchorY+40*(i), 190, 30), preformat + skill + postformat)){
				//Debug.Log(_skills[i+_zeroIndex]);
				_selectedSkill = skill;
				_skillDescription = SkillDB.GetDescription(skill);
			}
		}

		// show skill description
		GUI.Box(new Rect(anchorX, anchorY, 400, cap*40+50), "");
		GUI.Box(new Rect(anchorX+5, anchorY+5, 390, cap*40+40), string.Format(_skillDescription, _selectedPony), StyleDB.Instance.white);

		// Learn button!
		if(_selectedSkill.Length > 0){
			bool canlearn = SaveFile.Active.skillpointsAvailable(_selectedPony) > 0;
			string msg = "Need 1 Skillpoint to Learn.";
			if(canlearn) msg = "Learn";
			if(_learnedSkills.Contains(_selectedSkill)){
				msg = "Already Known";
				canlearn = false;
			}
			if(GUI.Button(new Rect(anchorX+100,  cap*40+80, 200, 39), msg)){
				if(canlearn){
					SaveFile.Active.LearnAbility(_selectedPony, _selectedSkill);
					_learnedSkills.Add(_selectedSkill);
				}
			}
		}

		// scrollbar if needed
		if(cap < _skills.Length){
			// show scroll buttons
			if(GUI.Button(new Rect(anchorX+630, anchorY, 30, 60), "^")) _zeroIndex--;
			if(GUI.Button(new Rect(anchorX+630, anchorY + cap*40-70, 30, 60), "v")) _zeroIndex++;
			// constrain so we wont go OOB
			if(_zeroIndex < 0) _zeroIndex = 0;
			if(_zeroIndex+cap > _skills.Length) _zeroIndex--;

		}

		// cancel button in the bottom.
		if(GUI.Button(new Rect(Screen.height/2-100, Screen.height-60, 200, 30), "Back")){
			_sub = subMenu.SkillOverview;
			_zeroIndex = 0;
		}
	}

	private enum subMenu{
		Root = 0,
		Rooster,
		SkillOverview,
		ViewPonySkills
	}
}
