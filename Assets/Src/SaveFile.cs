/*
 * Save File. an adaptor class for unitys data saving system. PlayerPrefs.
 * This can be used to extract and deposit any sort of data related to persistance.
 * It does as well have utilitites for updating things in the game to match the database version of it.
 * When you extend teh capabilitite of this class. Start by adding a todo note in the savecheckpoint and loadcheckpoint
 * 
 * 
 * 
 * 
 */

using UnityEngine;
using System.Collections.Generic;

public class SaveFile{
	static private SaveFile _activeSaveFile;
	static public SaveFile Active{
		get{return _activeSaveFile;}
	}

	private string slot;
	private List<string> _playerUnits;

	public SaveFile (string slot = "default")
	{
		_activeSaveFile = this;
		this.slot = slot;
		_playerUnits = new List<string>(_names);
	}


	private string[] _names = {
		"Tinker Spark",
		"Dawn",
		"Ma Cherie",
		"Rusty",
		"Syrah",
		"Moonlight",
		"Luna"
	};

	/// <summary>
	/// Gets the unit.
	/// </summary>
	/// <returns>Gameobject of the Unit to be placed on the map.</returns>
	/// <param name="name">Name of the unit.</param>
	public Unit GetUnit(string name){
		if(_playerUnits.Contains(name)){
			return Resources.Load<GameObject>("PCs/" + name).GetComponent<Unit>();
		}
		return null;
	}

	/// <summary>
	/// Grants the X.
	/// </summary>
	/// <returns><c>true</c> if the unit did level up, <c>false</c> otherwise.</returns>
	/// <param name="name">Name of the unit.</param>
	/// <param name="ammount">Experience granted.</param>
	public bool GrantXP(Unit unit, float ammount){
		if(_playerUnits.Contains(unit.name)){
			float exp = PlayerPrefs.GetFloat(slot + unit.name + "exp", 0f);
			float newExp = exp + ammount;
		//	Debug.Log("Granting " +  ammount.ToString() + " experience to " + unit.name);
			if((int)newExp != (int)exp){
				PlayerPrefs.SetFloat(slot + unit.name + "exp", newExp);
				UpdateUnit(unit);
				return true;
			}
			PlayerPrefs.SetFloat(slot + unit.name + "exp", newExp);
		//	Debug.Log("New total is " + PlayerPrefs.GetFloat(slot + unit.name + "exp", 0f));
			return false;
		}
		Debug.Log(unit.name + " is not a player!");
		return false;
	}

	/// <summary>
	/// Updates a unit on stage to match its configuration
	/// </summary>
	/// <param name="unit">Unit.</param>
	public void UpdateUnit(Unit unit){
		if(_playerUnits.Contains(unit.name)){
			float exp = PlayerPrefs.GetFloat(slot + unit.name + "exp", 0f);
			unit.level = 1 + (int)exp;
			unit.RecalcBaseStats();

		}
	}

	public void UpdateUnit(IEnumerable<Unit> units){
		foreach(Unit u in units){
			UpdateUnit(u);
		}
	}

	/// <summary>
	/// Gets the level of a unit.
	/// </summary>
	/// <returns>The level.</returns>
	/// <param name="name">Name.</param>
	public int GetLevel(string name){
		return 1 + (int)PlayerPrefs.GetFloat(slot + name + "exp", 0f);
	}

	public void SetLevel(string name, float level){
		if(_playerUnits.Contains(name)){
			if(level < 1f || level > 20f){
				Debug.LogWarning("Tried to set level of " + name + " to " + level + ". Can only set character to a level in the range between 1 to 20");
			}
			PlayerPrefs.SetFloat(slot + name + "exp", level-1f);
		}else{
			Debug.LogWarning("Tried to set level on a character that does not exist. Check if the name " + name + " is properly spelled.");
		}
	}

	/// <summary>
	/// Saves the current temporary data to a checkpoint.
	/// </summary>
	public void SaveCheckpoint(int checkpoint){

		foreach(string name in _names){
			// save character experience
			PlayerPrefs.SetFloat(slot + "checkpoint" + name + "exp", PlayerPrefs.GetFloat(slot + name + "exp", 0f));
			// lock/unlock characters
			PlayerPrefs.SetInt(slot + "checkpoint" + name + "unlocked", PlayerPrefs.GetInt(slot + name + "unlocked", 0));
			// save abilitites
			for(int i = 0; i < 5; i++){
				PlayerPrefs.SetString(slot + "checkpoint" + name + "ability#" + i, PlayerPrefs.GetString(slot + name + "ability#" + i, ""));
			}
		}
		PlayerPrefs.SetInt(slot + "checkpoint#", checkpoint);

		// save to disk at end of thingy
		PlayerPrefs.Save();
	}
	/// <summary>
	/// Loads the current checkpoint into the temporary datapool.
	/// </summary>
	public int LoadCheckpoint(){

		foreach(string name in _names){
			// load character experience
			PlayerPrefs.SetFloat(slot+ name + "exp", PlayerPrefs.GetFloat(slot + "checkpoint" + name + "exp", 0f));
			// lock/unlock characters
			PlayerPrefs.SetInt(slot + name + "unlocked", PlayerPrefs.GetInt(slot + "checkpoint" + name + "unlocked", 0));
			// load abilitites
			for(int i = 0; i < 5; i++){
				PlayerPrefs.SetString(slot + name + "ability#" + i, PlayerPrefs.GetString(slot + "checkpoint" + name + "ability#" + i, ""));
			}
		}
		return PlayerPrefs.GetInt(slot + "checkpoint#", 0);
	}

	/// <summary>
	/// Whether this a character is unlocked or not.
	/// If you ask for a character that does not exist it returns false and print an warning.
	/// </summary>
	/// <returns><c>true</c> if this instance is unlocked the specified name; otherwise, <c>false</c>.</returns>
	/// <param name="name">Name.</param>
	public bool IsUnlocked(string name){
		// see if unit exist among names. if it aint, print error and return false
		if(!_playerUnits.Contains(name))
		{
			Debug.LogWarning(name + " does not exist amongs valid player characters!");
			return false;
		}
		// extract information whenever or not the character is unlocked
		return(PlayerPrefs.GetInt(slot + name + "unlocked", 0) == 1);
	}
	/// <summary>
	/// Unlocks the character.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="unlock">If set to <c>true</c> unlock, if set to <c>false</c> lock.</param>
	public void UnlockCharacter(string name, bool unlock = true){
		// see if unit exist among names. if it aint, print error and abort
		if(!_playerUnits.Contains(name))
		{
			Debug.LogWarning(name + " does not exist amongs valid player characters!");
			return;
		}
		// write character as locked/unlocked based on the input data
		PlayerPrefs.SetInt(slot + name + "unlocked", (unlock) ? 1 : 0);
	}

	public List<Unit> GetUnlockedCharacters(){
		List<Unit> rv = new List<Unit>(16);
		foreach(string s in _playerUnits){
			// see if character is unlocked.
			if(IsUnlocked(s)){
				// get character if unlocked
				rv.Add(GetUnit(s));
			}
		}
		return rv;
	}

	/// <summary>
	/// Learns the ability.
	/// </summary>
	/// <param name="unit">Unit.</param>
	/// <param name="ability">Classname of the Ability.</param>
	public void LearnAbility(string unit, string ability){
		Debug.Log("Teaching: " + ability + " to " + unit);
		for(int i = 0; i < 5; i++){
			string temp = PlayerPrefs.GetString(slot + unit + "ability#" + i, "");

			if(temp.Length == 0){
				PlayerPrefs.SetString(slot + unit + "ability#" + i, ability);
				break;
			}
		}
	}

	public string[] GetAbilities(string unit){
		string[] rv = new string[5];
		for(int i = 0; i < 5; i++){
			string temp = PlayerPrefs.GetString(slot + unit + "ability#" + i, "");
			rv[i] = temp;
		}
		return rv;
	}

	public int skillpointsAvailable(string name){

		int known = 0;
		foreach(string s in GetAbilities(name)){
			if(s.Length > 0) known++;
		}

		int max = GetLevel(name)/3+1;
		return max - known;
	}

	public void BestowAbilitites(Unit u){
		foreach(string s in GetAbilities(u.name)){
			if(s.Length > 0){
				UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(u.gameObject, "Assets/Src/SaveFile.cs (239,5)", s);
			}
		}
	}
}
