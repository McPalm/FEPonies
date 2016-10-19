using UnityEngine;
using System;

public class DamageData {


	public int baseDamage=1;
	public float defenceMultiplier=1f;
	public float resistanceMultiplier=0f;
	public bool crit=false;
    public bool hit = true;
	public float damageMultipler=1f;
    public Unit source;
    public Unit target;
	public bool testAttack = false;

	private Tile sourceTile;
	private int finalDamage = -1;

	Action<DamageData> callbacks;

	public Tile SourceTile
	{
		get
		{
			if (sourceTile == null) return source.Tile;
			return sourceTile;
		}

		set
		{
			sourceTile = value;
		}
	}

	public int FinalDamage
	{
		get
		{
			return finalDamage;
		}
	}

	public int ApplyDefences(int defence, int resistance)
	{
		int dmg = (int)((baseDamage - defence * defenceMultiplier - resistance * resistanceMultiplier) * damageMultipler);
		if (crit) dmg *= 2;
		finalDamage = Math.Max(dmg, 0);
		return finalDamage;
	}

	public void Callback()
	{
		if(callbacks != null) callbacks(this);
	}

	public void RegisterCallback(Action<DamageData> damageData)
	{
		callbacks += damageData;
	}
}
