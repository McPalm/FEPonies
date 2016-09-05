using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangeAndMelee : MonoBehaviour, IReach {


	public List<Tile> GetTiles(Tile origin){
		return StaticGetTiles(origin);
	}

	static public List<Tile> StaticGetTiles (Tile origin)
	{
		List<Tile> tiles = new List<Tile>();

		if(origin.North != null && origin.North.ShootThrough){
			tiles.Add(origin.North);
			if(origin.North.North != null) tiles.Add(origin.North.North);
			if(origin.North.East != null) tiles.Add(origin.North.East);
			if(origin.North.West != null) tiles.Add(origin.North.West);
		}
		if(origin.South != null&& origin.South.ShootThrough){
			tiles.Add(origin.South);
			if(origin.South.South != null) tiles.Add(origin.South.South);
			if(origin.South.East != null) tiles.Add(origin.South.East);
			if(origin.South.West != null) tiles.Add(origin.South.West);
		}
		if(origin.East != null&& origin.East.ShootThrough){
			tiles.Add(origin.East);
			if(origin.East.South != null) tiles.Add(origin.East.South);
			if(origin.East.East != null) tiles.Add(origin.East.East);
			if(origin.East.North != null) tiles.Add(origin.East.North);
		}
		if(origin.West != null&& origin.West.ShootThrough){
			tiles.Add(origin.West);
			if(origin.West.South != null) tiles.Add(origin.West.South);
			if(origin.West.West != null) tiles.Add(origin.West.West);
			if(origin.West.North != null) tiles.Add(origin.West.North);
		}

		return tiles;
	}
}
