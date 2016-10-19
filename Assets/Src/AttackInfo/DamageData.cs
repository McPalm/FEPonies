using UnityEngine;
using System;

public class DamageData {


	public int baseDamage;
	public float defenceMultiplier;
	public float resistanceMultiplier;
	public bool crit;
	public bool hit;
	public float damageMultipler;

	Action<DamageData> callbacks;

	public int GetDamage(int defence, int resistance)
	{
		int dmg = (int)((baseDamage - defence * defenceMultiplier - resistance * resistanceMultiplier) * damageMultipler);
		if (crit) dmg *= 2;
		return Math.Max(dmg, 0);
	}

	public void Callback()
	{
		callbacks(this);
	}

	public void RegisterCallback(Action<DamageData> damageData)
	{
		callbacks += damageData;
	}
}
