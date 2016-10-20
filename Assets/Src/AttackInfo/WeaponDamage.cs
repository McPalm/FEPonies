using UnityEngine;
using System.Collections;
using System;

public class WeaponDamage : IEffect{

	float strScale = 1f;
	float dexScale = 0f;
	float intScale = 0f;
	int baseDamage = 4;

	float defenceMultiplier = 1f;
	float resistanceMultiplier = 0f;

	public float StrScale
	{
		get
		{
			return strScale;
		}

		set
		{
			strScale = value;
		}
	}

	public float DexScale
	{
		get
		{
			return dexScale;
		}

		set
		{
			dexScale = value;
		}
	}

	public float IntScale
	{
		get
		{
			return intScale;
		}

		set
		{
			intScale = value;
		}
	}

	public int BaseDamage
	{
		get
		{
			return baseDamage;
		}

		set
		{
			baseDamage = value;
		}
	}

	public float DefenceMulitiplier
	{
		get
		{
			return defenceMultiplier;
		}

		set
		{
			defenceMultiplier = value;
		}
	}

	public float ResistanceMultiplier
	{
		get
		{
			return resistanceMultiplier;
		}

		set
		{
			resistanceMultiplier = value;
		}
	}

	public int Apply(DamageData attackData)
	{
		attackData.baseDamage = GetDamage(attackData.source.GetStatsAt(attackData.SourceTile, attackData.target));
		attackData.defenceMultiplier = defenceMultiplier;
		attackData.resistanceMultiplier = resistanceMultiplier;

		return Damage.StaticApply(attackData);
	}

	public WeaponDamage()
	{

	}

	/// <summary>
	/// Get the damage this weapon would deal with a certain stat array
	/// </summary>
	/// <param name="s">Stats of the character doing the attack.</param>
	/// <returns></returns>
	public int GetDamage(Stats s)
	{
		return baseDamage + (int)(s.strength * strScale + s.dexterity * dexScale + s.intelligence * intScale);
	}

}
