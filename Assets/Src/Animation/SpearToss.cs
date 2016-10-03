using UnityEngine;
using System.Collections;
using System;

public class SpearToss : MonoBehaviour, IAnimation {


	Tackle tackle;
	Arrow arrow;

	void Start()
	{
		arrow = gameObject.AddComponent<Arrow>();
		tackle = gameObject.AddComponent<Tackle>();
	}

	public void Animate(Unit source, Tile target, Action<Tile> tile, bool hit = true)
	{
		if (TileGrid.GetDelta(source, target) == 2)
			arrow.Animate(source, target, tile, hit);
		else
			tackle.Animate(source, target, tile, hit);


	}
}
