using UnityEngine;
using System.Collections.Generic;

// Ability is outdated
public class Transfusion : Ability, TargetedAbility {
	public override void Use(){
		TargetedAbilityInputManager.Instance.Listen(this);
	}
	
	public override string Name{
		get{return "Transfusion";}
	}
	
	public void Notify(Tile target)
	{
		Debug.LogError("Transfusion not implemented!");
		Unit user = GetComponent<Unit>();
		// get my hp
		int heal = user.CurrentHP/4;
		// hurt self for one fourth that
		// user.Damage(heal, new DamageType(DamageType.TRUE));
		// heal target for double that
		target.Unit.Heal(heal*2);
		FinishUse();
	}
	
	public HashSet<Tile> GetAvailableTargets()
	{
		Unit host = GetComponent<Unit>();
		HashSet<Tile> retVal=new HashSet<Tile>();
		if(host == null){
			Debug.LogError("host not found, is transfution ability sitting on a unit?");
		}else{
			foreach(Tile t in RangeAndMelee.StaticGetTiles(host.Tile)){
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
			Debug.LogError("host not found, is transfution ability sitting on a unit?");
		}else{
			foreach(Tile t in RangeAndMelee.StaticGetTiles(tile)){
				if(t.isOccuppied && !host.isHostile(t.Unit)){
					retVal.Add(t);
				}
			}
		}
		return retVal;
	}

}