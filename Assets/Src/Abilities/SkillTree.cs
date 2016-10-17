using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class SkillTree : MonoBehaviour, Buff {

	[SerializeField]
	public List<SkillTreeLevel> skills;
	[SerializeField]
	int[] choises;

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

		for(int i = 0; i < level; i++)
		{
			string s = skills[i].option1;
			if (s == "Str") r.strength++;
			else if (s == "Dex") r.dexterity++;
			else if (s == "Agi") r.agility++;
			else if (s == "Int") r.intelligence++;
		}

		_stats = r;
	}

	static public Sprite GetSprite(string skill)
	{
		throw new System.NotImplementedException();
	}

	[System.Serializable]
	public class SkillTreeLevel
	{
		public string option1;
		public string option2;
	}
}
