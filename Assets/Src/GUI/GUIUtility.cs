using UnityEngine;
using System.Collections.Generic;

public static class GUIUtility{

	public static string StatsToString(Stats s, string divider = "\n")
	{
		string r = "";
		if(s.maxHP > 0) r += "+" + s.maxHP + " HP" + divider;
		if (s.strength > 0) r += "+" + s.strength + " STR" + divider;
		if (s.agility > 0) r += "+" + s.agility + " AGI" + divider;
		if (s.dexterity > 0) r += "+" + s.dexterity + " DEX" + divider;
		if (s.intelligence > 0) r += "+" + s.intelligence + " INT" + divider;
		if (s.defense > 0) r += "+" + s.defense + " Defence" + divider;
		if (s.resistance > 0) r += "+" + s.resistance + " Resistance" + divider;

		if (s.dodgeBonus > 0) r += "+" + FloatToPercent(s.dodgeBonus) + " Dodge" + divider;
		if (s.dodgeBonus < 0) r += FloatToPercent(s.dodgeBonus) + " Dodge" + divider;
		if (s.hitBonus > 0) r += "+" + FloatToPercent(s.hitBonus) + " Hit" + divider;
		if (s.hitBonus < 0) r += FloatToPercent(s.hitBonus) + " Hit" + divider;
		if (s.critBonus > 0) r += "+" + FloatToPercent(s.critBonus) + " Crit" + divider;
		if (s.critBonus < 0) r += FloatToPercent(s.critBonus) + " Crit" + divider;
		if (s.critDodgeBonus > 0) r += "+" + FloatToPercent(s.critDodgeBonus) + " Crit Avoid" + divider;
		if (s.critDodgeBonus < 0) r += FloatToPercent(s.critDodgeBonus) + " Crit Avoid" + divider;

		if (s.carryBonus > 0) r += "+" + s.carryBonus + " Carry Capacity" + divider;
		if (s.maxMana > 0) r += "+" + s.maxMana + " Mana" + divider;
		if (r.Length == 0) return r;
		return r.Substring(0, r.Length- divider.Length);
	}

	static public string FloatToPercent(float f)
	{
		return Mathf.Round(f * 100) + "%";
	}
}