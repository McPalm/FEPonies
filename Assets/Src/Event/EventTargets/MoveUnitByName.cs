using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// This class should only be used if there is a single unit with this name
/// </summary>
public class MoveUnitByName : EventTarget
{

    public string moveMe;
    public Tile moveTo;
    public bool active = false;
    public List<EventTarget> events;

    private bool callback;

    public override void Notice()
    {
        HashSet<Unit> units = UnitManager.Instance.GetUnits();
        Unit moveThis=null;
        foreach(Unit u in units)
        {
            if(u.name==moveMe)
            {
                moveThis = u;
            }
        }
        if (moveThis != null)
        {
            moveThis.MoveToAndAnimate(moveTo, true);
            active = true;
        }
        else
        {
            Debug.LogError("No Unit with that name or Unit=null in MoveUnitByName");
        }       
    }

    public void Update()
    {
        if (active && StateManager.Instance.Peek() != GameState.unitMoving)
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