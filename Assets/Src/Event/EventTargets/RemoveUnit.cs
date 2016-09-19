using UnityEngine;
using System.Collections;

public class RemoveUnit : EventTarget {

	public override void Notice ()
	{
		GetComponent<Unit>().Death();
	}
}
