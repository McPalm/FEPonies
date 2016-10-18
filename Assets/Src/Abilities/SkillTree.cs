﻿using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class SkillTree : MonoBehaviour, Buff {

	[SerializeField]
	public List<SkillTreeLevel> skills;

	private Stats _stats;

	public Stats Stats
	{
		get
		{
			return _stats;
		}
	}

	/// <summary>
	/// Get the total attribute bonuses from skills.
	/// </summary>
	/// <returns></returns>
	public void CalculateStats(int level)
	{
		Stats r = new Stats();
		if (skills == null) return;
		level = System.Math.Min(level, skills.Count);
		for (int i = 0; i < level; i++)
		{
			string s = skills[i].Selected;
			if (s == "Str") r.strength++;
			else if (s == "Dex") r.dexterity++;
			else if (s == "Agi") r.agility++;
			else if (s == "Int") r.intelligence++;
		}
		_stats = r;
	}

	[System.Serializable]
	public class SkillTreeLevel
	{
		int choise = 0;
		public string option1 = "";
		public string option2 = "";

		public string Selected
		{
			get
			{
				if (Choise == 1) return option1;
				if (Choise == 2) return option2;
				return "";
			}
			set
			{
				if (value == option1) Choise = 1;
				else if (value == option2) Choise = 2;
				else Choise = 0;
			}
		}

		public int Choise
		{
			get
			{
				return choise;
			}

			set
			{
				if (value == 0 || value == 1) choise = value;
				else if (value == 2 && option2 != "") choise = value;
				
			}
		}
	}
}