using UnityEngine;
using System.Collections.Generic;

public class Norange : MonoBehaviour, IReach {
	
	public List<Tile> GetTiles (Tile origin)
	{
		return StaticGetTiles(origin);
	}
	
	static public List<Tile> StaticGetTiles(Tile origin){
		return new List<Tile>();;
	}
}
