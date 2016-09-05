/// <summary>
/// History.
/// The history class contains a history of all actions made on the level since startup. It can be used to get a complete replay..   huzzah?
/// Inherits from List...  So add new action through Add()
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class History : Observable {
	private static History instance;
	private List<Action> history;
	protected HashSet<Observer> observerCollection;
	protected HashSet<Observer> toRemove;
	

	public static History Instance{
		get{
			if(instance == null){
				instance = new History();
			}
			return instance;
		}
	}

	private History()
	{
		history=new List<Action>();
		observerCollection=new HashSet<Observer>();
		toRemove= new HashSet<Observer>();
	}

	public void Add(Action obj)
	{
		history.Add(obj);
		notifyObservers();
	}

	public Action Peek()
	{
		return history[history.Count-1];
	}

	public int Count()
	{
		return history.Count;
	}

	public void registerObserver(Observer obs)
	{
		observerCollection.Add(obs);
	}

	public void unregisterObserver(Observer obs)
	{
		toRemove.Add(obs);
	}

	public void notifyObservers()
	{
		cleanUpToRemove();
		foreach(Observer o in observerCollection)
		{
			try{
			o.Notify();
			}catch(System.Exception e){
				Debug.LogException(e);
			}
		}
	}

	private void cleanUpToRemove()
	{
		foreach(Observer o in toRemove)
		{
			observerCollection.Remove(o);
		}
		toRemove.Clear();
	}

}
