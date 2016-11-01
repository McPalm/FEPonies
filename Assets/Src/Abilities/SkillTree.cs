using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class SkillTree : Buff {

	[SerializeField]
	public List<SkillTreeLevel> skills;
	[NonSerialized]
	private Stats _stats;

	public SkillTree()
	{
		_stats = new Stats();
	}

	/// <summary>
	/// Craetesa a new Skilltree that is an identical copy of a previous one.
	/// </summary>
	/// <param name="o"></param>
	public SkillTree(SkillTree o)
	{
		if (o == null) Debug.LogError("Tried to copy a tree that is null!");
		skills = new List<SkillTreeLevel>();
		foreach(SkillTreeLevel stl in o.skills)
		{
			skills.Add(new SkillTreeLevel(stl));
		}
		_stats = o._stats;
	}


	public Stats Stats
	{
		get
		{
			return _stats;
		}
	}

	public int PointsSpent
	{
		get
		{
			int rv = 0;
			foreach(SkillTreeLevel stl in skills)
			{
				if (stl.Choise == 0) return rv;
				rv++;
			}
			return rv;
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
			else if (s == "Str2") r.strength += 2;
			else if (s == "Dex2") r.dexterity += 2;
			else if (s == "Agi2") r.agility += 2;
			else if (s == "Int2") r.intelligence += 2;
			else if (s == "AgiDex")
			{
				r.agility++;
				r.dexterity++;
			}
			else if (s == "DexInt")
			{
				r.intelligence++;
				r.dexterity++;
			}
			else if (s == "Weight2") r.carryBonus += 2;
		}
		_stats = r;
	}

	[System.Serializable]
	public class SkillTreeLevel
	{
		[SerializeField]
		int choise = 0;
		public string option1 = "";
		public string option2 = "";

		public SkillTreeLevel()
		{

		}

		public SkillTreeLevel(SkillTreeLevel o)
		{
			choise = o.choise;
			option1 = o.option1;
			option2 = o.option2;
		}

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
