using UnityEngine;
using System.Collections;

public class Win : EventTarget {

	public override void Notice ()
	{
		LevelManager.Instance.Win();
	}
}