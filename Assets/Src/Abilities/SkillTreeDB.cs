using UnityEngine;
using System.Collections.Generic;

public class SkillTreeDB : MonoBehaviour {

	static SkillTreeDB instance;
	public static SkillTreeDB Instance
	{
		get
		{
			if (instance == null) instance = Resources.Load<SkillTreeDB>("SkilltreeDB");
			return instance;
		}
	}

	[SerializeField]
	List<TreeContainer> trees;

	public static SkillTree GetSkillTree(string name)
	{
		foreach(TreeContainer st in Instance.trees)
		{
			if (st.name == name)
				return st.tree;
		}
		Debug.LogError(name + " not found in the skilltree database");
		return null;
	}

	public static List<string> GetAllNames()
	{
		List<string> rs = new List<string>();
		foreach(TreeContainer tc in Instance.trees)
		{
			rs.Add(tc.name);
		}
		return rs;
	}

	public static void Expand()
	{
		Instance.trees.Add(new TreeContainer());
	}

	public static void ChangeName(string oldName, string newName)
	{
		foreach (TreeContainer tc in Instance.trees)
		{
			if (tc.name == oldName)
			{
				tc.name = newName;
				break;
			}
		}
	}

	public static void Remove(string name)
	{
		TreeContainer target = null;
		foreach (TreeContainer tc in Instance.trees)
		{
			if (tc.name == name)
				target = tc;
		}
		if(target != null)
		{
			Instance.trees.Remove(target);
		}
	}

	public static SkillTree GetSkillTreeClone(string name)
	{
		return new SkillTree(GetSkillTree(name));
	}

	[System.Serializable]
	private class TreeContainer
	{
		public string name;
		public SkillTree tree;

		public TreeContainer()
		{
			name = "new";
			tree = new SkillTree();
		}
	}
}