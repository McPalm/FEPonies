using UnityEngine;
using System.Collections.Generic;

public class Portraits : MonoBehaviour {

	private static Dictionary<string, Texture2D> _sprites;

	// Use this for initialization
	void Start () {
		// load all portraits and put in a dictionary
		if(_sprites != null) return;
		LoadThings();
	}

	/// <summary>
	/// Gets the portrait with the specified name. If no such portrait exists, return null.
	/// </summary>
	/// <returns>The portrait.</returns>
	/// <param name="name">Name.</param>
	static public Texture2D GetPortrait(string name){
		if(_sprites == null) LoadThings();

		Texture2D ret = null;
		_sprites.TryGetValue(name, out ret);
		return ret;
	}

	static private void LoadThings(){
		_sprites = new Dictionary<string, Texture2D>();
		foreach(Texture2D s in Resources.LoadAll<Texture2D>("portraits/")){
			_sprites.Add(s.name, s);
		}
	}
}
