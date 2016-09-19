using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TotalWipeEvent : MonoBehaviour, Observer {
	[Range(0, 1)]
	public int team=1;//What team the eent triggers on wiping;
	public List<EventTarget> events;

	public void Notify()
	{
		if(LevelManager.Instance.isLoaded){
			if (UnitManager.Instance.GetUnitsByTeam(team).Count==0)
			{
				triggerEvents();
			}
		}
	}

	void Awake()
	{
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
		Debug.Log ("All units on team "+team+" died");
		foreach( EventTarget o in events)
		{
			try{
				o.Notice();
			}catch(System.Exception e){
				Debug.LogException(e);
			}
		}
	}
}
