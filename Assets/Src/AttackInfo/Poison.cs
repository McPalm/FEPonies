using UnityEngine;
using System.Collections.Generic;

public class Poison : MonoBehaviour, IEffect {
	
	public DamageType damageType {
		get {
			return new DamageType(DamageType.POISON);
		}
		set {}
	}

	public void Apply (Tile target, Unit user)
	{
		StaticApply(target);
	}

	public int Apply (Tile target, Unit user, bool testAttack)
	{
		if(testAttack) return 5;
		else StaticApply(target);
		return 0;
	}

    public int Apply(Tile target, Unit user, bool testAttack, Tile testTile)
    {
        if (testAttack) return 5;
        else StaticApply(target);
        return 0;
    }

    static public void StaticApply(Tile target){
		if(target.Unit){
			PoisonDebuff pd = target.Unit.GetComponent<PoisonDebuff>();
			if(pd == null){
				target.Unit.gameObject.AddComponent<PoisonDebuff>();
			}else{
				pd.Stack(1, 5);
			}
		}
	}
}
