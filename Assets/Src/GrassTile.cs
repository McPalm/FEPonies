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

		BuffArea mybuff = gameObject.AddComponent<BuffArea>();
		Stats stats = new Stats();
		stats.dodgeBonus = 0.2f;
		mybuff.Initialize(0, stats, true);

		// retValue.dodgeBonus=0.2f;

	}
}