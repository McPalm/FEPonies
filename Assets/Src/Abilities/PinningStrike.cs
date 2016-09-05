using UnityEngine;
using System.Collections.Generic;

public class PinningStrike : Ability, TargetedAbility {

	public int uses = 2;

	public override string Name {
		get {return "PinningStrike";}
	}

	public override void Use ()
	{
		if(uses > 0){
			TargetedAbilityInputManager.Instance.Listen(this);
		}else{
			GUInterface.Instance.PrintMessage("Out of uses!");
		}
	}

	public void Notify (Tile target)
	{
		Unit host = GetComponent<Unit>();
		Pin.StaticApply(target, host);
		uses--;
		host.StartAttackSequence (target.Unit);
		//FinishUse();
	}

	public System.Collections.Generic.HashSet<Tile> GetAvailableTargets ()
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		foreach(Tile t in Melee.StaticGetTiles(host.Tile)){
			if(t.isOccuppied && host.isHostile(t.Unit)&&!t.Unit.invisible){
				retVal.Add(t);
			}
		}
		return retVal;
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		foreach(Tile t in Melee.StaticGetTiles(tile)){
			if(t.isOccuppied && host.isHostile(t.Unit)&&!t.Unit.invisible){
				retVal.Add(t);
			}
		}
		return retVal;
	}
}
