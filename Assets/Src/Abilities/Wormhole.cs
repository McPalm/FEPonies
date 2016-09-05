using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// Wormhole.
/// Activated ability that swap the location of the user and one unit within 3+IN/5 squares
/// </summary>

public class Wormhole : AbilityWithManacost, TargetedAbility {

	private int range = 0;

	public override string Name {
		get {
			return "Wormhole";
		}
	}

	public override void Use ()
	{
		if (GetComponent<Mana>().CanCast(ManaCost)){
			TargetedAbilityInputManager.Instance.Listen(this, TargetedAbilityInputManager.AreaOfEffect.SingleTile);
		}
		else{
			OOM();
		}
	}

	public override int ManaCost {
		get {
			return 1;
		}
	}

	public void Notify (Tile target)
	{
		Tile temp = null;
		Unit client = target.Unit;
		Unit user = GetComponent<Unit>();
		Tile myTile = user.Tile;
		foreach(Tile t in TileGrid.Instance){
			if(t.isOccuppied) continue;
			temp = t;
			break;
		}
		client.MoveTo(temp);
		user.MoveTo(target);
		client.MoveTo(myTile);
		FinishUse();
	}

	public HashSet<Tile> GetAvailableTargets ()
	{
		return GetAvailableTargets(GetComponent<Unit>().Tile);
	}

	public HashSet<Tile> GetAvailableTargets (Tile tile)
	{
		HashSet<Tile> ret = new HashSet<Tile>();

		foreach(Unit u in UnitManager.Instance.GetUnits()){
			int d = TileGrid.GetDelta(this, u);
			if(d > 0 && d <= range){
				ret.Add(u.Tile);
			}
		}
		return ret;
	}

	// Use this for initialization
	void Start () {

	}

	void UnitSelected(){
		CalcRange();
	}

	void CalcRange(){
		range = GetComponent<Unit>().ModifiedStats.intelligence/5+3;
	}
}
