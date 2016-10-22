using UnityEngine;
using System.Collections.Generic;

public class SkillTreeDB : MonoBehaviour {

	static SkillTreeDB instance;
	static SkillTreeDB Instance
	{
		get
		{
			if (instance == null) instance = Resources.Load<SkillTreeDB>("SkilltreeDB");
			return instance;
		}
	}

	[SerializeField]
	List<TreeContainer> trees;

	static SkillTree GetSkillTree(string name)
	{
		foreach(TreeContainer st in Instance.trees)
		{
			if (st.name == name)
				return st.tree;
		}
		Debug.LogError(name + " not found in the skilltree database");
		return null;
	}

	[System.Serializable]
	private class TreeContainer
	{
		public string name;
		public SkillTree tree;
	}
}