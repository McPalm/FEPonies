using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour, IEffect {
    //TODO Add in damage multipliers
	public void Apply(DamageData attackData)
	{
        //Get damage multipliers and put them in attackData
        int dmg = StaticApply (attackData);
		//SendMessageUpwards("OnDamageDealt", dmg, SendMessageOptions.DontRequireReceiver);
	}

	public int Apply (DamageData attackData, bool testAttack)
	{
        //Get damage multipliers and put them in attackData
        return StaticApply(attackData, testAttack);
	}

    public int Apply(DamageData attackData, bool testAttack, Tile testTile)
    {
        //Get damage multipliers and put them in attackData
        return StaticApply(attackData, testAttack, testTile);
    }

    static public int StaticApply(DamageData attackData, bool testAttack = false, Tile testPosition = null){
		Unit targetUnit = attackData.target;
		if (testPosition == null) testPosition = attackData.source.Tile;
		if(targetUnit != null){
			// deliver damage to target.
			return targetUnit.Damage(attackData, testAttack);
			
		}else{
			Debug.LogError("Damage IEffect may target an empty tile!\n" +
			               "User=" + attackData.source + "\n" +
			               "Target=" + attackData.target);
			return 0;
		}

	}
}
