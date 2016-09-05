using UnityEngine;
using System.Collections.Generic;


// put this script anywhere on the scene to make enemies start facing right, and players facing left.
public class ReverseStartFacing : MonoBehaviour {

	public static bool reverse = false;


	void Awake () {
		reverse = true;
	}
}
