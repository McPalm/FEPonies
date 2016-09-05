using UnityEngine;
using System.Collections;

public class SnapToGrid : MonoBehaviour {


	void OnDrawGizmosSelected() {
		if(!Application.isPlaying){
			float x = Mathf.Round(transform.position.x);
			float y = Mathf.Round(transform.position.y);

			transform.position = new Vector3(x, y, transform.position.z);
		}
	}
}
