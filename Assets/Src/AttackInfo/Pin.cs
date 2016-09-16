using UnityEngine;
using System.Collections;

public class Pin : IEffect {

	public DamageType damageType {
		get {
			return new DamageType();
		}
		set {}
	}

	public int Apply (Tile target, Unit user, bool testAttack)
	{
		throw new System.NotImplementedException ();
	}

	public void Apply (Tile target, Unit user)
	{
		StaticApply(target, user);
	}

	static public void StaticApply(Tile target, Unit user, int duration = 1){
		if(target.Unit && target.Unit.IsAlive)
			target.Unit.gameObject.AddComponent<RootDebuff>().initialize(duration+1, user);
		else
			Debug.LogWarning("Tried to root an empty square!");
	}

    public int Apply(Tile target, Unit user, bool testAttack, Tile testTile)
    {
        throw new System.NotImplementedException();
    }
}
