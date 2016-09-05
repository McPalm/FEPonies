using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnterZoneEvent : MonoBehaviour, Observer {
	public int team=0; // the team that it triggers for, -1 for anyteam.
	public int range=0; //how far away it should reach, 0 is in the same tile
	public List<EventTarget> events;
	// Use this for initialization
	void Awake()
	{
		History.Instance.registerObserver(this);
	}
	
	public void Notify()
	{
		Tile dest = History.Instance.Peek().movement;
		if(dest != null){
			if((Mathf.Abs(dest.transform.position.x-transform.position.x)+Mathf.Abs(dest.transform.position.y-transform.position.y)) <= range
			   && ((dest.Unit.team==team) || team == -1))
			{
				triggerEvents();
			}
		}
	}

	// Update is called once per frame
	void Update () {
	}

	void triggerEvents()
	{
		History.Instance.unregisterObserver(this);
		foreach( EventTarget o in events)
		{
			if(o != null){
				o.Notice();
			}
		}
	}

	void OnDestroy(){

		History.Instance.unregisterObserver(this);
	}
}
