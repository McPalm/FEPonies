using UnityEngine;
using System.Collections.Generic;
using System;

public class TileGate : Tile {

	public GameObject gateThingy;

	bool _open;

	public bool Open
	{
		get { return _open; }
		set {
			_open = value;
			if (_open) gateThingy.SetActive(false);
			else gateThingy.SetActive(true);
		}
	}
	public override TileType Type
	{
		get
		{
			return (_open) ? TileType.regular : TileType.wall;
		}
	}
	public override bool ShootThrough
	{
		get
		{
			return _open;
		}
	}
	public override void GetReachableTiles(UnitMove move, HashSet<Tile> reachTiles, Unit team)
	{
		if (_open) base.GetReachableTiles(move, reachTiles, team);
		return;
	}
}
