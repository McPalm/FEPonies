//------------------------------------------------------------------------------
// <auto-generated>
//     Denna kod har genererats av ett verktyg.
//     Körtidsversion:4.0.30319.34014
//
//     Ändringar i denna fil kan orsaka fel och kommer att förloras om
//     koden återgenereras.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class Petrify : IEffect {

	public DamageType damageType {
		get {
			return new DamageType();
		}
		set {}
	}

	public bool Defence {
		get {
			return true;
		}
	}
	
	public int Apply (Tile target, Unit user, bool testAttack)
	{
		throw new System.NotImplementedException ();
	}
	
	public void Apply (Tile target, Unit user)
	{
		StaticApply(target, user);
	}
	
	static public void StaticApply(Tile target, Unit user){
		if(target.Unit && target.Unit.IsAlive && target.Unit.GetComponent<Petrification>() == null)
			target.Unit.gameObject.AddComponent<Petrification>();
		else
			Debug.LogWarning("Tried to petrify an empty square!");
	}
	
	public int judgeAttack(Unit user, Unit target)
	{
		throw new System.NotImplementedException();
	}
}


