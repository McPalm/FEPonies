using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterTile : Tile {
	 
	public override TileType Type {
		get {
			return TileType.water;
		}
	}

	public override void GetReachableTiles (UnitMove move, HashSet<Tile> reachTiles, Unit team)
	{
		if(move.moveType!=MoveType.flying)
		{
			return;
		}
		base.GetReachableTiles(move, reachTiles, team);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
