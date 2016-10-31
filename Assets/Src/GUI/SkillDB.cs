using UnityEngine;
using System.Collections.Generic;

public class SkillDB : MonoBehaviour {

	static private SkillDB _instance;

	static public SkillDB Instance
	{
		get
		{
			if(Instance == null)_instance = Resources.Load<SkillDB>("SkillDB");
			return _instance;
		}
	}

	static public Sprite GetIcon(string name)
	{
		foreach(SkillContainer sc in _instance.library)
		{
			if (sc.name == name)
				return sc.icon;
		}
		Debug.LogError("Missing icon for " + name);
		return _instance.defaultSprite;
	}


	[SerializeField]
	Sprite defaultSprite;
	[SerializeField]
	List<SkillContainer> library;
	public bool sort = false;

	void OnDrawGizmos()
	{
		if(sort)
		{
			sort = false;
			library.Sort(delegate (SkillContainer a, SkillContainer b) { return a.name.CompareTo(b.name); });
		}
	}

	[System.Serializable]
	class SkillContainer
	{
		public string name;
		public Sprite icon;
		public string tooltip;
		[TextArea]
		public string description;
	}
}