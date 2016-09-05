using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IReach{
	/// <summary>
	/// Gets all tiles that are within reach of the origin tile. With no regard for who is in that tile, an ITargetFilter will be used for that end.
	/// </summary>
	/// <returns>The tiles.</returns>
	/// <param name="origin">Origin of the attack.</param>
	List<Tile> GetTiles(Tile origin);
}
