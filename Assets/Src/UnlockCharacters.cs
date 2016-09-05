using UnityEngine;
using System.Collections.Generic;

public class UnlockCharacters : MonoBehaviour {

	public List<string> unlock;
	public List<string> disable;

	// Use this for initialization
	void Start () {

		foreach(string n in unlock){
			SaveFile.Active.UnlockCharacter(n);
		}
		foreach(string d in disable){
			SaveFile.Active.UnlockCharacter(d, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
