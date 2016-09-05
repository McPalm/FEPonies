using UnityEngine;
using System.Collections.Generic;

public class PresetCharacter : MonoBehaviour {

	public string unitName;
	public bool setLevel = false;
	[Range(0f, 20f)]
	public float level;
	public List<string> abilities;

	// Use this for initialization
	void Awake () {
		if(setLevel) SaveFile.Active.SetLevel(unitName, level);
		foreach(string ability in abilities){
			SaveFile.Active.LearnAbility(unitName, ability);
		}
	}
}
