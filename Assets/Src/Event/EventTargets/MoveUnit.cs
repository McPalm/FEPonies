using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MoveUnit : EventTarget{

	public Unit moveMe;
	public Tile moveTo;
    public bool active=false;
    public List<EventTarget> events;

    private bool callback;

    public override void Notice()
	{
		moveMe.MoveToAndAnimate(moveTo, true);
        active = true;
    }

    public void Update()
    {
        if(active&&StateManager.Instance.Peek()!=GameState.unitMoving)
        {
            CallBack();
            active = false; 
        }
    }

    public void CallBack()
    {
        foreach (EventTarget et in events)
        {
            try
            {
                et.Notice();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                Debug.Log(et);
            }
        }
    }
}
