using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WallTile : Tile {

	public Animator animator;

	public void Start(){
		if(animator){
			if(West)
				animator.SetBool("West", West.Type == TileType.wall);
			if(East)
				animator.SetBool("East", East.Type == TileType.wall);
			if(North)
				animator.SetBool("North", North.Type == TileType.wall);
			if(South)
				animator.SetBool("South", South.Type == TileType.wall);
		}
	}

	public override TileType Type {
		get {
			return TileType.wall;
		}
	}
	public override bool ShootThrough {
		get {
			return false;
		}
	}
	public override void GetReachableTiles (UnitMove move, HashSet<Tile> reachTiles, Unit team)
	{
		return;
	}
}
