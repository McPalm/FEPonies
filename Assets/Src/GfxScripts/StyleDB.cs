using UnityEngine;
using System.Collections;

public class StyleDB : MonoBehaviour {

	static private StyleDB instance;
	static public StyleDB Instance{
		get{
			if(instance == null){
				instance = Resources.Load<GameObject>("StyleDB").GetComponent<StyleDB>();
			}
			return instance;
		}
	}

	public GUIStyle style;

	public GUIStyle white;
}
