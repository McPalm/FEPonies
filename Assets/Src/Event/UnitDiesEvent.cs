using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitDiesEvent : MonoBehaviour {

	public List<EventTarget> events;
	// Use this for initialization

	public void UnitDeath(){
		triggerEvents();
	}

	void triggerEvents()
	{
		foreach( EventTarget o in events)
		{
			o.Notice();
		}
	}
}
