using UnityEngine;
using System.Collections.Generic;
using System;

public class SingBuff : MonoBehaviour, IAttackModifier
{
	Sing artist;

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public void Test(DamageData dd)
	{
		if (IsActive && WithinRange(dd.SourceTile))
			dd.damageMultipler *= 1.15f;
	}

	public void Initialize(Sing artist)
	{
		this.artist = artist;
	}

	bool WithinRange(Component location)
	{
		return TileGrid.GetDelta(location, artist) < 4;
	}

	bool IsActive
	{
		get
		{
			return artist.Active;
		}
	}
}