using UnityEngine;
using System.Collections.Generic;

public class LoadCharacter : MonoBehaviour {

	[SerializeField]
	string rosterCharacter;
	[SerializeField]
	string dbCharacter;
	[SerializeField]
	Character presetCharacter;
	[SerializeField]
	int setLevel = -1;

	// Use this for initialization
	void Awake()
	{
		Character loadedCharacter;
		if(dbCharacter != "")
		{
			CharacterDB cdb = Resources.Load<CharacterDB>("CharacterDB");
			loadedCharacter = cdb.GetCharacter(dbCharacter);
		}
		else if(rosterCharacter != "")
		{
			// get a character from the roster
			loadedCharacter = UnitRoster.Instance.GetCharacter(rosterCharacter);
		}
		else
		{
			// use preset character
			Debug.LogWarning("Prefferably load from roster or database instead of hardcoding a character here.");
			loadedCharacter = presetCharacter;
		}

		if (setLevel > 0) loadedCharacter.Level = setLevel;
		GetComponent<Unit>().Character = loadedCharacter;

	}
}