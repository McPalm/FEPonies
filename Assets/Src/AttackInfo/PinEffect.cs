using UnityEngine;
using System.Collections;
using System;

public class PinEffect : IEffect {

	static public void StaticApply(DamageData attackData, int duration = 1){
		if(attackData.target && attackData.target.IsAlive)
			attackData.target.gameObject.AddComponent<RootDebuff>().initialize(duration+1, attackData.source);
		else
			Debug.LogWarning("Something went wrong!");
	}

	public int Apply(DamageData attackData)
	{
		StaticApply(attackData);
		return 10; // HACK
	}
}
