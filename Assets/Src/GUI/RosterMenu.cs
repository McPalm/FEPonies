using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class RosterMenu : ScrollMenu, Observable {

	List<Observer> observers;
	private Unit unit;

	public CharacterButton button;
	public List<Unit> units;

	public Unit Unit
	{
		get
		{
			return unit;
		}
	}

	void Awake()
	{
		observers = new List<Observer>();
	}

	new void Start()
	{
		for (int i = 0; i < units.Count; i++)
		{
			CharacterButton b = Instantiate<CharacterButton>(button);
			RectTransform rc = b.GetComponent<RectTransform>();
			rc.SetParent(anchor);
			rc.localScale = Vector3.one;
			rc.localPosition = new Vector3(0, i*-90);

			Height = i * 90 + 95;

			b.Label = units[i].name;

			b.Register(Click, i);
		}
		button.gameObject.SetActive(false);
		base.Start();
	}

	void Click(int i)
	{
		unit = units[i];
		notifyObservers();
	}

	public void registerObserver(Observer obs)
	{
		observers.Add(obs);
	}

	public void unregisterObserver(Observer obs)
	{
		observers.Remove(obs);
	}

	public void notifyObservers()
	{
		foreach (Observer o in observers)
			o.Notify();
	}
}
