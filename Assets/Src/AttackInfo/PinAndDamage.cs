using UnityEngine;
using System.Collections;

public class PinAndDamage : MonoBehaviour, IEffect {

	private DamageType _damageType;
	public DamageType damageType {
		get {
			return _damageType;
		}
		set {
			_damageType = value;
		}
	}

	public int Apply (Tile target, Unit user, bool testAttack)
	{
		return Damage.StaticApply(target, user, _damageType, testAttack);
	}

	public int duration = 1;

	public void Apply (Tile target, Unit user)
	{
		int dmg = StaticApply(target, user, _damageType);
		SendMessageUpwards("OnDamageDealt", dmg, SendMessageOptions.DontRequireReceiver);
		_damageType.Critical = false;
	}

    public int Apply(Tile target, Unit user, bool testAttack, Tile testTile)
    {
        return Damage.StaticApply(target, user, _damageType, testAttack);
    }

    public int StaticApply(Tile target, Unit user, DamageType dtyp){
		Pin.StaticApply(target, user, duration);
		return Damage.StaticApply(target, user, dtyp); // may kill target, do last. Just to keep the sequence more safe.
	}
}
