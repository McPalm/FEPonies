using UnityEngine;
using System.Collections.Generic;

public class Fog : EventTarget {

	public int right;
	public int down;

	private bool init = true;
	private Tile hideMe;

	private Fog nextRight;
	private Fog nextDown;

	public override void Notice ()
	{
		Destroy(gameObject);
	}


	// Use this for initialization
	void Start () {
		if(init){
			Setup(right, down);
		}

		// get tile under and make it not visible
		hideMe = TileGrid.Instance.GetTileAt(transform.position);
		if(hideMe) hideMe.Visible = false;
	}
	
	void Setup(int right, int down){
		init = false;

		if(right > 0){
			GameObject o = Instantiate(this.gameObject, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity) as GameObject;
			nextRight = o.GetComponent<Fog>();
			nextRight.Setup(right-1, down);
		}
		if(down > 0){
			GameObject o = Instantiate(this.gameObject, transform.position + new Vector3(0f, -1f, 0f), Quaternion.identity) as GameObject;
			nextDown = o.GetComponent<Fog>();
			nextDown.Setup(0, down-1);
		}
	}

	void OnDestroy(){
		if(hideMe) hideMe.Visible = true;
		if(nextDown) Destroy(nextDown.gameObject);
		if(nextRight) Destroy(nextRight.gameObject);
	}
}
