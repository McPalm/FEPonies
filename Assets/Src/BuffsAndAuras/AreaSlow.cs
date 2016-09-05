using UnityEngine;
using System.Collections.Generic;

public class AreaSlow : MonoBehaviour {

	public int radius;

	private HashSet<Tile> _affectedTiles;

	// Use this for initialization
	void Start () {
		_affectedTiles = TileGrid.Instance.GetTilesAt(transform.position, radius);

		foreach(Tile t in _affectedTiles){
			t.AddSlow(this);
		}
	}

	void OnDestroy(){
		foreach(Tile ti in _affectedTiles){
			ti.RemoveSlow(this);
		}
	}
}
