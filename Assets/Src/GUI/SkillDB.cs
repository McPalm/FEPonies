using UnityEngine;
using System.Collections.Generic;

public class SkillDB : MonoBehaviour {

	static SkillDB _instance;
	static public SkillDB Instance
	{
		get
		{
			if(_instance == null)_instance = Resources.Load<SkillDB>("SkillDB");
			return _instance;
		}
	}

	static public Sprite GetIcon(string name)
	{
		foreach (SkillContainer sc in Instance.library)
		{
			if (sc.name == name)
				return sc.icon;
		}
		Debug.LogWarning("Missing icon for " + name);
		return _instance.defaultSprite;
	}

	static public string GetToolTip(string name)
	{
		foreach (SkillContainer sc in Instance.library)
		{
			if (sc.name == name)
				return sc.name + "\n" + sc.tooltip;
		}
		Debug.LogWarning("Missing tooltip for " + name);
		return name;
	}

	static public string GetDescription(string name, string userName)
	{
		foreach (SkillContainer sc in Instance.library)
		{
			if (sc.name == name)
				return string.Format(sc.description, userName);
		}
		Debug.LogWarning("Missing description for " + name);
		return name;
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
#pragma warning disable
		public string name;
		public Sprite icon;
		public string tooltip;
		[TextArea]
		public string description;
	}
}