using UnityEngine;
using System.Collections.Generic;

public class Sap : Ability, TargetedAbility {

	Unit _unit;

	void Start(){
		_unit = GetComponent<Unit>();
	}

	public void Notify (Tile target)
	{
		target.Unit.gameObject.AddComponent<SapDebuff>();
		DamageData dd = new DamageData();
		Stats s = _unit.GetStatsAt(_unit.Tile, target.Unit);
		dd.damageMultipler = 0.5f;
		dd.defenceMultiplier = 0.5f;
		dd.target = target.Unit;
		dd.source = _unit;
		Damage.StaticApply(dd);

		FinishUse();
	}

	public HashSet<Tile> GetAvailableTargets()
	{
		HashSet<Tile> retVal=new HashSet<Tile>();

		foreach(Tile t in Melee.StaticGetTiles(_unit.Tile)){
			if(t.isOccuppied && _unit.isHostile(t.Unit)){
				retVal.Add(t);
			}
		}

		return retVal;
	}
	
	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		HashSet<Tile> retVal=new HashSet<Tile>();

		foreach(Tile t in Melee.StaticGetTiles(tile)){
			if(t.isOccuppied && _unit.isHostile(t.Unit)){
				retVal.Add(t);
			}
		}

		return retVal;
	}

	public override string Name {
		get {
			return "Sap";
		}
	}

	public override void Use ()
	{
		TargetedAbilityInputManager.Instance.Listen(this);
	}
}
