using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

<<<<<<< HEAD
[CustomEditor(typeof(SkillTree))]
public class SkillTreeInspector : Editor {
=======
public class SkillTreeInspector : EditorWindow{


	[MenuItem("FE Clone/Skill Tree Inspector")]
	static void Init()
	{
		SkillTreeInspector window = (SkillTreeInspector)EditorWindow.GetWindow(typeof(SkillTreeInspector));
		window.Show();
	}


	const int SELECT = 0;
	const int EDIT = 1;
	int state = SELECT;
	bool remove = false;

	string target = "";
	string changeName = "enter name";
>>>>>>> dee71704e70c6d067d00bcb81c6fa6b32e4d6b39

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
<<<<<<< HEAD
		
=======
		if (GUILayout.Button("Back"))
		{
			state = SELECT;
			AssetDatabase.SaveAssets();
		}
		if (GUILayout.Button((remove) ? "Abort" : "Remove"))
			remove = !remove;
		if(remove)
		{
			if (GUILayout.Button("CONFIRM!"))
			{
				SkillTreeDB.Remove(target);
				state = SELECT;
				remove = false;
			}
		}
>>>>>>> dee71704e70c6d067d00bcb81c6fa6b32e4d6b39
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
			else if (stl.option1 == "" || stl.option1 == "Str2" || stl.option1 == "Dex2" || stl.option1 == "Agi2" || stl.option1 == "Int2")
				new object();
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
			else if (stl.option2 == "" || stl.option2 == "Str2" || stl.option2 == "Dex2" || stl.option2 == "Agi2" || stl.option2 == "Int2")
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
