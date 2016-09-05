using UnityEngine;
using System.Collections.Generic;

public class GravityField : AbilityWithManacost, TargetedAbility {

	private GameObject mine;

	public override string Name {
		get {
			return "Gravity Field";
		}
	}

	public override void Use ()
	{
		if (GetComponent<Mana>().CanCast(ManaCost)){
			TargetedAbilityInputManager.Instance.Listen(this, TargetedAbilityInputManager.AreaOfEffect.Burst);
		}
		else{
			OOM();
		}
	}

	public HashSet<Tile> GetAvailableTargets ()
	{
		Tile t = GetComponent<Unit>().Tile;
		HashSet<Tile> ret = new HashSet<Tile>(RangeAndMelee.StaticGetTiles(t));
		ret.UnionWith(IncreasedRange.StaticGetTiles(t));
		return ret;
	}

	public HashSet<Tile> GetAvailableTargets(Tile tile)
	{
		HashSet<Tile> ret = new HashSet<Tile>(RangeAndMelee.StaticGetTiles(tile));
		ret.UnionWith(IncreasedRange.StaticGetTiles(tile));
		return ret;
	}

	public override int ManaCost {
		get {
			return 1;
		}
	}

	public void Notify (Tile target)
	{
		Instantiate(mine, target.transform.position, Quaternion.identity);
		
		FinishUse();
	}

	// Use this for initialization
	void Start () {
		mine = Resources.Load("GravityField") as GameObject;
	}

}
