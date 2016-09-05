using UnityEngine;
using System.Collections;

public class FocusCamera : EventTarget {

	public override void Notice ()
	{
		CameraControl.FocusOn(transform.position);
	}
}
