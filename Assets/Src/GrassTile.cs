using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrassTile : Tile {

	public static bool isFirst=true;

	public override TileType Type {
		get {
			return TileType.grass;
		}
	}

	public override int Movecost{
		get
		{
			return 2;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void Start()
	{
		if(isFirst)
		{
			BuffManager.Instance.Add(new GrassBuff());
		}
		isFirst=false;


	}

	private class GrassBuff : Buff
	{
		public bool Affects(Unit u)
		{
			if(u.Tile is GrassTile)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public Stats Stats
		{
			get
			{
				Stats retValue=new Stats();
				retValue.dodgeBonus=0.2f;
				return retValue;
			}
		}
	}
}