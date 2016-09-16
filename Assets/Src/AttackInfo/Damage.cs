using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour, IEffect {

	private DamageType _damageType;
	public DamageType damageType {
		get {
			return _damageType;
		}
		set {
			_damageType = value;
		}
	}

	public void Apply(Tile target, Unit user)
	{
		int dmg = StaticApply (target, user, _damageType);
		SendMessageUpwards("OnDamageDealt", dmg, SendMessageOptions.DontRequireReceiver);
		_damageType.Critical = false;
	}

	public int Apply (Tile target, Unit user, bool testAttack)
	{
		return StaticApply(target, user, _damageType, testAttack);
	}

    public int Apply(Tile target, Unit user, bool testAttack, Tile testTile)
    {
        return StaticApply(target, user, _damageType, testAttack, testTile);
    }

    static public int StaticApply(Tile target, Unit user, DamageType typeOfAttack, bool testAttack = false, Tile testPosition = null){
		Unit targetUnit = target.Unit;
		if (testPosition == null) testPosition = user.Tile;
		if(targetUnit != null){
			// deliver damage to target.
			Stats s = user.GetStatsAt(testPosition, targetUnit);
			int damage = s.strength + s.might;
			return targetUnit.Damage(damage, typeOfAttack, testAttack);
			
		}else{
			Debug.LogError("Damage IEffect may target an empty tile!\n" +
			               "User=" + user + "\n" +
			               "Target=" + target);
			return 0;
		}

	}
}
