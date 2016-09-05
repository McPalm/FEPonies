using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour, IEffect {

	public DamageType damageType {
		get {
			return new DamageType();
		}
		set {}
	}

	public int healAmmount = 15;

	public void Apply (Tile target, Unit user)
	{
		target.Unit.Heal(healAmmount);
	}

	public int Apply (Tile target, Unit user, bool testAttack)
	{
		throw new System.NotImplementedException ();
	}

	public int judgeAttack(Unit user, Unit target)
	{
		return target.damageTaken;
	}

	public bool Defence {
		get {
			return true;
		}
	}
}
