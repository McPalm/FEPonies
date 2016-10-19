using UnityEngine;
using System.Collections.Generic;

public class Lasso : Ability, TargetedAbility {

	public override string Name {
		get {return "Lasso";}
	}

	public override void Use ()
	{
		TargetedAbilityInputManager.Instance.Listen(this);
	}


	public void Notify (Tile target)
	{
		DamageData dd = new DamageData();
		if(target.Unit) dd.target = target.Unit;
		dd.source = GetComponent<Unit>();

		Pin.StaticApply(dd);
		FinishUse();
	}

	public System.Collections.Generic.HashSet<Tile> GetAvailableTargets ()
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		foreach(Tile t in RangeAndMelee.StaticGetTiles(host.Tile)){
			if(t.isOccuppied && host.isHostile(t.Unit)&&!t.Unit.invisible){
				retVal.Add(t);
			}
		}
		return retVal;
	}

	public System.Collections.Generic.HashSet<Tile> GetAvailableTargets (Tile tile)
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		foreach(Tile t in RangeAndMelee.StaticGetTiles(tile)){
			if(t.isOccuppied && host.isHostile(t.Unit)&&!t.Unit.invisible){
				retVal.Add(t);
			}
		}
		return retVal;
	}

}
