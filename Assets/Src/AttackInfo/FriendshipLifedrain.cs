using UnityEngine;
using System.Collections;

/// <summary>
/// Friendship lifedrain.
/// Twilight sparkle special spell, heals allies to her equal to 1/4 to the damage dealt by the spell.
/// </summary>
public class FriendshipLifedrain : MonoBehaviour, IEffect {

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
		int damageDealt = target.Unit.Damage(user.AttackStat,_damageType);
		if(user.Tile.North != null){
			if(user.Tile.North.Unit != null && !user.Tile.North.Unit.isHostile(user)){
				user.Tile.North.Unit.Heal(damageDealt/3);
			}
		}
		if(user.Tile.South != null){
			if(user.Tile.South.Unit != null && !user.Tile.South.Unit.isHostile( user)){
				user.Tile.South.Unit.Heal(damageDealt/3);
			}
		}
		if(user.Tile.East != null){
			if(user.Tile.East.Unit != null && !user.Tile.East.Unit.isHostile(user)){
				user.Tile.East.Unit.Heal(damageDealt/3);
			}
		}
		if(user.Tile.West != null){
			if(user.Tile.West.Unit != null && !user.Tile.West.Unit.isHostile(user)){
				user.Tile.West.Unit.Heal(damageDealt/3);
			}
		}
		//throw new System.NotImplementedException ();

		SendMessageUpwards("OnDamageDealt", damageDealt, SendMessageOptions.DontRequireReceiver);
		_damageType.Critical = false;
	}

	public int Apply (Tile target, Unit user, bool testAttack)
	{
		return target.Unit.Damage(user.AttackStat, _damageType, true);
	}

	public int judgeAttack(Unit user, Unit target)
	{
		throw new System.NotImplementedException();
	}
}
