using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Ranged : MonoBehaviour, IReach {
	public List<Tile> GetTiles(Tile origin)
	{
		return staticGetTiles(origin);
	}

	public static List<Tile> staticGetTiles (Tile origin)
	{
		List<Tile> tiles = new List<Tile>();

		if(origin.North != null && origin.North.ShootThrough){
			if(origin.North.North != null) tiles.Add(origin.North.North);
			if(origin.North.East != null) tiles.Add(origin.North.East);
			if(origin.North.West != null) tiles.Add(origin.North.West);
		}
		if(origin.South != null&& origin.South.ShootThrough){
			if(origin.South.South != null) tiles.Add(origin.South.South);
			if(origin.South.East != null) tiles.Add(origin.South.East);
			if(origin.South.West != null) tiles.Add(origin.South.West);
		}
		if(origin.East != null&& origin.East.ShootThrough){
			if(origin.East.South != null) tiles.Add(origin.East.South);
			if(origin.East.East != null) tiles.Add(origin.East.East);
			if(origin.East.North != null) tiles.Add(origin.East.North);
		}
		if(origin.West != null&& origin.West.ShootThrough){
			if(origin.West.South != null) tiles.Add(origin.West.South);
			if(origin.West.West != null) tiles.Add(origin.West.West);
			if(origin.West.North != null) tiles.Add(origin.West.North);
		}

		// get all tiles within range of the origin

		return tiles;

	}
}
