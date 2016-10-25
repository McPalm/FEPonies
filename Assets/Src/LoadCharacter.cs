using UnityEngine;
using System.Collections.Generic;

public class LoadCharacter : MonoBehaviour {

	[SerializeField]
	string rosterCharacter;
	[SerializeField]
	Character presetCharacter;

	// Use this for initialization
	void Awake()
	{
		if(rosterCharacter == "")
		{
			// use preset character
			GetComponent<Unit>().Character = presetCharacter;
		}
		else
		{
			// get a character from the roster
			GetComponent<Unit>().Character = UnitRoster.Instance.GetCharacter(rosterCharacter);
		}


	}
}