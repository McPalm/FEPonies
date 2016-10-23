using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class RosterMenu : ScrollMenu, Observable {

	List<Observer> observers;
	private Character character;

	public CharacterButton button;
	List<CharacterButton> buttons;

	public Character Character
	{
		get
		{
			return character;
		}
	}

	void Awake()
	{
		observers = new List<Observer>();
		Pauser.Instance.gameObject.SetActive(false);
	}

	new void Start()
	{
		buttons = new List<CharacterButton>();
		int i = 0;
		foreach(Character c in UnitRoster.Instance)
		{ 
			CharacterButton b = Instantiate<CharacterButton>(button);
			buttons.Add(b);
			RectTransform rc = b.GetComponent<RectTransform>();
			rc.SetParent(anchor);
			rc.localScale = Vector3.one;
			rc.localPosition = new Vector3(0, i*-90);

			Height = i * 90 + 95;

			b.Label = c.Name;
			b.Register(Click, i);

			i++;
		}
		button.gameObject.SetActive(false);
		UpdateLevels();
		base.Start();
	}

	void Click(int i)
	{
		character = UnitRoster.Instance.activeRoster[i];
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

	public void UpdateLevels()
	{
		for (int i = 0; i < UnitRoster.Instance.activeRoster.Count; i++)
		{
			// buttons[i].LevelUp = UnitRoster.Instance.activeRoster[i].Level > UnitRoster.Instance.activeRoster[i].Skilltree.PointsSpent;
		}
	}
}
