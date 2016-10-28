using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;

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

	// Use this for initialization
	void OnGUI()
	{

		if (state == EDIT) Edit();
		if (state == SELECT) Select();

	}

	private void Select()
	{
		remove = false;
		foreach (String name in SkillTreeDB.GetAllNames())
		{
			if (GUILayout.Button(name))
			{
				state = EDIT;
				target = name;
				changeName = name;
			}
		}
		GUILayout.Space(25f);

		if (GUILayout.Button("Add"))
		{
			SkillTreeDB.Expand();
		}
	}

	void Edit()
	{
		SkillTree st = SkillTreeDB.GetSkillTree(target);

		if (st == null) state = SELECT;

		GUILayout.Label(target);

		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();

		GUILayout.Label("New name");
		changeName = GUILayout.TextField(changeName);
		
		if (GUILayout.Button("Change name") && changeName.Length > 2)
		{
			SkillTreeDB.ChangeName(target, changeName);
			target = changeName;
		}

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();

		if (st.skills == null || st.skills.Count != 15)
		{
			st.skills = new List<SkillTree.SkillTreeLevel>();
			for (int i = 0; i < 15; i++)
			{
				st.skills.Add(new SkillTree.SkillTreeLevel());
			}
		}

		for (int i = 0; i < st.skills.Count; i++)
		{
			BuildLevel(st.skills[i], i);
		}

		if (GUILayout.Button("Verify"))
			Verify(st);
		if (GUILayout.Button("Back"))
		{
			state = SELECT;
			EditorUtility.SetDirty(SkillTreeDB.Instance);
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
	}

	void BuildLevel(SkillTree.SkillTreeLevel stl, int level)
	{
		
		

		EditorGUILayout.BeginHorizontal();
		
		stl.option1 = GUILayout.TextField(stl.option1, GUILayout.MinWidth(100f));

		bool one = (stl.Choise == 1);
		one = EditorGUILayout.Toggle(one, GUILayout.Width(13f));
		if (one) stl.Choise = 1;

		GUILayout.Label((level + 1).ToString(), GUILayout.Width(25f));

		bool two = (stl.Choise == 2);
		two = EditorGUILayout.Toggle(two, GUILayout.Width(13f));
		if (two) stl.Choise = 2;

		stl.option2 = GUILayout.TextField(stl.option2, GUILayout.MinWidth(100f));
		EditorGUILayout.EndHorizontal();
		if (!one && !two) stl.Choise = 0;
	}

	void Verify(SkillTree st)
	{
		foreach(SkillTree.SkillTreeLevel stl in st.skills)
		{
			if (stl.option1 == "" || stl.option1 == "Str" || stl.option1 == "Dex" || stl.option1 == "Agi" || stl.option1 == "Int")
				new object(); // This is editor only, so I dont give a shit how bad this is.
			else if (stl.option1 == "Str2" || stl.option1 == "Dex2" || stl.option1 == "Agi2" || stl.option1 == "Int2")
				new object();
			else if (stl.option1 == "AgiDex" || stl.option1 == "Weight2" || stl.option1 == "DexInt")
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
			else if (stl.option2 == "Str2" || stl.option2 == "Dex2" || stl.option2 == "Agi2" || stl.option2 == "Int2")
				new object();
			else if (stl.option2 == "AgiDex" || stl.option2 == "Weight2" || stl.option2 == "DexInt")
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
