using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IncreasedRange : IReach {
	
	public List<Tile> GetTiles (Tile origin)
	{
		return StaticGetTiles(origin);
	}

	static public List<Tile> StaticGetTiles (Tile origin){
		List<Tile> tiles = new List<Tile>();
		
		if(origin.North != null && origin.North.ShootThrough){
			if(origin.North.North != null && origin.North.North.ShootThrough){
				tiles.Add(origin.North.North);
				if(origin.North.North.North != null) tiles.Add(origin.North.North.North);
			}
			if(origin.North.East != null && origin.North.East.ShootThrough){
				tiles.Add(origin.North.East);
				if(origin.North.East.North != null) tiles.Add(origin.North.East.North);
			}
			if(origin.North.West != null && origin.North.West.ShootThrough){
				tiles.Add(origin.North.West);
				if(origin.North.West.North != null) tiles.Add(origin.North.West.North);
			}
		}
		if(origin.South != null && origin.South.ShootThrough){
			if(origin.South.South != null && origin.South.South.ShootThrough){
				tiles.Add(origin.South.South);
				if(origin.South.South.South != null) tiles.Add(origin.South.South.South);
			}
			if(origin.South.East != null && origin.South.East.ShootThrough){
				tiles.Add(origin.South.East);
				if(origin.South.East.South != null) tiles.Add(origin.South.East.South);
			}
			if(origin.South.West != null && origin.South.West.ShootThrough){
				tiles.Add(origin.South.West);
				if(origin.South.West.South != null) tiles.Add(origin.South.West.South);
			}
		}
		if(origin.East != null && origin.East.ShootThrough){
			if(origin.East.East != null && origin.East.East.ShootThrough){
				tiles.Add(origin.East.East);
				if(origin.East.East.East != null) tiles.Add(origin.East.East.East);
			}
			if(origin.East.South != null && origin.East.South.ShootThrough){
				tiles.Add(origin.East.South);
				if(origin.East.South.East != null) tiles.Add(origin.East.South.East);
			}
			if(origin.East.North != null && origin.East.North.ShootThrough){
				tiles.Add(origin.East.North);
				if(origin.East.North.East != null) tiles.Add(origin.East.North.East);
			}
		}
		if(origin.West != null && origin.West.ShootThrough){
			if(origin.West.West != null && origin.West.West.ShootThrough){
				tiles.Add(origin.West.West);
				if(origin.West.West.West != null) tiles.Add(origin.West.West.West);
			}
			if(origin.West.North != null && origin.West.North.ShootThrough){
				tiles.Add(origin.West.North);
				if(origin.West.North.West != null) tiles.Add(origin.West.North.West);
			}
			if(origin.West.South != null && origin.West.South.ShootThrough){
				tiles.Add(origin.West.South);
				if(origin.West.South.West != null) tiles.Add(origin.West.South.West);
			}
		}
		
		return tiles;
	}
}