using UnityEngine;
using System.Collections;

public class PoisonAndDamage : MonoBehaviour, IEffect {

	private DamageType _damageType;
	public DamageType damageType {
		get {
			return _damageType;
		}
		set {
			_damageType = value;
		}
	}

	public void Apply (Tile target, Unit user)
	{
		int dmg = StaticApply(target, user);
		SendMessageUpwards("OnDamageDealt", dmg, SendMessageOptions.DontRequireReceiver);
		_damageType.Critical = false;
	}

	public int Apply (Tile target, Unit user, bool testAttack)
	{
		return Damage.StaticApply(target, user, damageType, testAttack);
	}

    public int Apply(Tile target, Unit user, bool testAttack, Tile testTile)
    {
        return Damage.StaticApply(target, user, damageType, testAttack);
    }

    public int StaticApply(Tile target, Unit user){
		Poison.StaticApply(target);
		return Damage.StaticApply(target, user, damageType);
	}	
}
