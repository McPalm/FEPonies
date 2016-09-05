using UnityEngine;
using System.Collections.Generic;

public class Wings : AbilityWithManacost, TargetedAbility {

	public override int ManaCost {
		get {
			return 3;
		}
	}

	public void Notify (Tile target)
	{
		new DurationBuff(999, new Stats(new UnitMove(1, MoveType.flying), 0, 0, 0, 0, 0, 0), target.Unit);
		FinishUse();
	}

	public HashSet<Tile> GetAvailableTargets()
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		if(host == null){
			Debug.LogError("host not found, is the gossamer wings ability sitting on a unit?");
		}else{
			retVal.Add(host.Tile);
			foreach(Tile t in Melee.StaticGetTiles(host.Tile)){
				if(t.isOccuppied && !host.isHostile(t.Unit)){
					retVal.Add(t);
				}
			}
		}
		return retVal;
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		if(host == null){
			Debug.LogError("host not found, is the gossamer wings ability sitting on a unit?");
		}else{
			retVal.Add(host.Tile);
			foreach(Tile t in Melee.StaticGetTiles(tile)){
				if(t.isOccuppied && !host.isHostile(t.Unit)){
					retVal.Add(t);
				}
			}
		}
		return retVal;	}

	public override string Name {
		get {
			return "Wings";
		}
	}

	public override void Use ()
	{
		if(GetComponent<Mana>().CanCast(ManaCost)) TargetedAbilityInputManager.Instance.Listen(this);
		else OOM();
	}
}
