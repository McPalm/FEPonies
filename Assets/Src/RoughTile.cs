using UnityEngine;
using System.Collections.Generic;

public class RoughTile : Tile {

	public override TileType Type {
		get {
			return TileType.rough;
		}
	}
	
	public override int Movecost{
		get
		{
			return 2;
		}
	}
}
