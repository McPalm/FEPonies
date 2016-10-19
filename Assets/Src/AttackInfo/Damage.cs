using UnityEngine;
using System.Collections;
using System;

public class Damage : MonoBehaviour, IEffect {
    //TODO Add in damage multipliers
	public int Apply(DamageData attackData)
	{
        //Get damage multipliers and put them in attackData
        return StaticApply (attackData);
		//SendMessageUpwards("OnDamageDealt", dmg, SendMessageOptions.DontRequireReceiver);
	}

    static public int StaticApply(DamageData attackData){
		Unit targetUnit = attackData.target;
		if(targetUnit != null){
			// deliver damage to target.
			return targetUnit.Damage(attackData);
			
		}else{
			Debug.LogError("Damage IEffect may target an empty tile!\n" +
			               "User=" + attackData.source + "\n" +
			               "Target=" + attackData.target);
			return 0;
		}

	}
}
