using UnityEngine;
using System.Collections.Generic;
using System;

public class Grit : Passive, IDefenceModifiers
{
	public override string Name
	{
		get
		{
			return "Grit";
		}
	}

	public int Priority
	{
		get
		{
			return -10;
			// grit should always always always be last
		}
	}

	public void Test(DamageData dd)
	{
		Stats s = dd.target.GetStatsAt(dd.target.Tile);
		if (dd.ApplyDefences(s.defense, s.resistance) >= dd.target.CurrentHP)
		{
			dd.damageMultipler *= 0.5f;
		}
	}
}