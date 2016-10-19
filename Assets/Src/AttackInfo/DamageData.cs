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
