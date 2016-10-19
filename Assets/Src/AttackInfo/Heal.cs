using UnityEngine;
using System;

public class Heal : MonoBehaviour, IEffect {

	public int healAmmount = 15;

	public int Apply (DamageData damageData)
	{
		// (Tile target, Unit user)
		damageData.target.Heal(healAmmount);
		return Math.Min(damageData.target.damageTaken, healAmmount); // HACK
	}
}
