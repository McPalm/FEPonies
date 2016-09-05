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
	
	public int StaticApply(Tile target, Unit user){
		Poison.StaticApply(target);
		return Damage.StaticApply(target, user, damageType);
	}

	public int judgeAttack(Unit user, Unit target)
	{
		int damageDealt=Damage.StaticApply(target.Tile, user, damageType, true);
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
