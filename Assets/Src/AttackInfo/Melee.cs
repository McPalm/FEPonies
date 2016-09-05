using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Melee : MonoBehaviour, IReach {

	public List<Tile> GetTiles (Tile origin)
	{
		return StaticGetTiles(origin);
	}

	static public List<Tile> StaticGetTiles(Tile origin){
		// get tiles north, east, west and south of target.
		List<Tile> retVal = new List<Tile>();

		if(origin.North != null){
			retVal.Add(origin.North);
		}
		if(origin.South != null){
			retVal.Add(origin.South);
		}
		if(origin.West != null){
			retVal.Add(origin.West);
		}
		if(origin.East != null){
			retVal.Add(origin.East);
		}
		return retVal;
	}
}
