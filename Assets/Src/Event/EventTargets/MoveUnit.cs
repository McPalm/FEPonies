using UnityEngine;
using System.Collections;

public class MoveUnit : EventTarget{

	public Unit moveMe;
	public Tile moveTo;

	public override void Notice()
	{
		moveMe.MoveToAndAnimate(moveTo, true);
	}
}
