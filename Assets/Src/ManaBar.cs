using UnityEngine;
using System.Collections.Generic;

public class ManaBar : MonoBehaviour {

	public List<Transform> dots;
	private Mana _mana;
	private Unit client;

	static public ManaBar Create(Transform parent){
		GameObject go = Instantiate(Resources.Load<GameObject>("ManaBar"), parent.position, Quaternion.identity) as GameObject;
		go.transform.parent = parent;
		go.GetComponent<ManaBar>()._mana = parent.GetComponent<Mana>();
		return go.GetComponent<ManaBar>();
	}

	void Start(){
		client = transform.parent.GetComponent<Unit>();
	}

	// Update is called once per frame
	void Update () {

		for(int i = 0; i < dots.Count; i++){
			dots[i].GetComponent<Renderer>().enabled = (i < _mana.ManaRemaining);
		}
		FaceRight = client.FaceRight;
	}

	public bool FaceRight{
		set{if (value){
				transform.localScale = new Vector3(1, 1, 1);
			}else{
				transform.localScale = new Vector3(-1, 1, 1);
			}
		}
	}
}


