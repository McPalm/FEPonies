using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(SkillTree))]
public class SkillTreeInspector : Editor {

	// Use this for initialization
	public override void OnInspectorGUI()
	{
		SkillTree st = (SkillTree)target;

		if (st.skills == null || st.skills.Count != 15)
		{
			st.skills = new List<SkillTree.SkillTreeLevel>();
			for (int i = 0; i < 15; i++)
			{
				st.skills.Add(new SkillTree.SkillTreeLevel());
			}
		}
			
		for(int i = 0; i < st.skills.Count; i++)
		{
			BuildLevel(st.skills[i], i);
		}

		if (GUILayout.Button("verify"))
			Verify(st);
		
	}

	void BuildLevel(SkillTree.SkillTreeLevel stl, int level)
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label(level.ToString());
		stl.option1 = GUILayout.TextField(stl.option1);
		stl.option2 = GUILayout.TextField(stl.option2);
		EditorGUILayout.EndHorizontal();
	}

	void Verify(SkillTree st)
	{
		foreach(SkillTree.SkillTreeLevel stl in st.skills)
		{
			if (stl.option1 == "" || stl.option1 == "Str" || stl.option1 == "Dex" || stl.option1 == "Agi" || stl.option1 == "Int")
				new object(); // This is editor only, so I dont give a shit how bad this is.
			else {
				try {
					AbilityLibrary.Instance.getTypeFromAbility(stl.option1);
				}
				catch (System.Exception)
				{
					stl.option1 = stl.option1 + "!";
				}
			}

			if (stl.option2 == "" || stl.option2 == "Str" || stl.option2 == "Dex" || stl.option2 == "Agi" || stl.option2 == "Int")
				new object();
			else
			{
				try
				{
					AbilityLibrary.Instance.getTypeFromAbility(stl.option2);
				}
				catch (System.Exception)
				{
					stl.option2 = stl.option2 + "!";
				}
			}
		}
	}
}
