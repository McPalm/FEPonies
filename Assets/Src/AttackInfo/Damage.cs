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

	static public int StaticApply(Tile target, Unit user, DamageType typeOfAttack, bool testAttack = false){
		Unit targetUnit = target.Unit;
		if(targetUnit != null){
			// deliver damage to target.
			return targetUnit.Damage(user.AttackStat, typeOfAttack, testAttack);
			
		}else{
			Debug.LogError("Damage IEffect may target an empty tile!\n" +
			               "User=" + user + "\n" +
			               "Target=" + target);
			return 0;
		}

	}

	public int judgeAttack(Unit user, Unit target)
	{
		int damageDealt=Apply(target.Tile, user, true);
		if(target.retaliationsLeft == 0)
		{
			damageDealt += 20;
		}
		else if((user.AttackInfo.reach) is Melee)
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
