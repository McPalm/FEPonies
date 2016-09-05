using UnityEngine;
using System.Collections;

public class Sniper : Passive {

	public override string Name {
		get {
			return "Sniper!";
		}
	}

	// Use this for initialization
	void Start () {
		// find the IREACH
		AttackInfo info = GetComponentInChildren<AttackInfo>();
		foreach(MonoBehaviour m in info.GetComponents<MonoBehaviour>())
		{
			if(m is IReach){
				DestroyImmediate(m);
				break;
			}		
		}
		// swap with an IncreasedRange
		info.reach = info.gameObject.AddComponent<IncreasedRange>();

	}

}
