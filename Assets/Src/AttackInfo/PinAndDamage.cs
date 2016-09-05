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

	public int StaticApply(Tile target, Unit user, DamageType dtyp){
		Pin.StaticApply(target, user, duration);
		return Damage.StaticApply(target, user, dtyp); // may kill target, do last. Just to keep the sequence more safe.
	}

	public int judgeAttack(Unit user, Unit target)
	{
		int damageDealt=Damage.StaticApply(target.Tile, user, _damageType, true);
		if((user.AttackInfo.reach) is Melee)
		{
			if(target.AttackInfo.reach is Ranged||target.AttackInfo.reach is IncreasedRange)
			{
				damageDealt+=20;
			}
		}
		else if(user.AttackInfo.reach is Ranged)
		{
			if(target.AttackInfo.reach is Melee)
			{
				damageDealt+=20;
			}
		}
		else if(user.AttackInfo.reach is RangeAndMelee)
		{
			if(target.AttackInfo.reach is Melee||target.AttackInfo.reach is Ranged||target.AttackInfo.reach is IncreasedRange)
			{
				damageDealt+=20;
			}
		}
		else if(user.AttackInfo.reach is IncreasedRange)
		{
			if(target.AttackInfo.reach is Melee||target.AttackInfo.reach is Ranged||target.AttackInfo.reach is RangeAndMelee)
			{
				damageDealt+=20;
			}
		}
		return damageDealt;
	}
}
