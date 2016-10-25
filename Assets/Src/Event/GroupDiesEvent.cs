using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupDiesEvent : MonoBehaviour, Observer {
	public int group=0;//What group the event triggers on killing;
	public int team=1;//What team the group belongs to. Mostly Computer i imagine.
	#pragma warning disable
	private bool hasSpawned=false;
	public List<EventTarget> events;

	public void Notify()
	{
		Debug.LogError("GroupDiesEvent not Supported!");
	}

	void Awake()
	{
		Debug.LogError("GroupDiesEvent not Supported!");
		UnitManager.Instance.registerObserver(this);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void triggerEvents()
	{
		UnitManager.Instance.unregisterObserver(this);
		Debug.Log ("All units in group "+group+" died");
		foreach( EventTarget o in events)
		{
			o.Notice();
		}
	}
}
