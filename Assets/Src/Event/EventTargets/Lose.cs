using UnityEngine;
using System.Collections;

public class Lose : EventTarget {
	
	public override void Notice ()
	{
		LevelManager.Instance.Lose();
	}
}
