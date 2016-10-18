using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class RosterMenu : ScrollMenu, Observable {

	List<Observer> observers;
	private Character character;

	public CharacterButton button;
	public List<Character> characters;
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
		for (int i = 0; i < characters.Count; i++)
		{
			CharacterButton b = Instantiate<CharacterButton>(button);
			buttons.Add(b);
			RectTransform rc = b.GetComponent<RectTransform>();
			rc.SetParent(anchor);
			rc.localScale = Vector3.one;
			rc.localPosition = new Vector3(0, i*-90);

			Height = i * 90 + 95;

			b.Label = characters[i].name;
			b.Register(Click, i);
		}
		button.gameObject.SetActive(false);
		UpdateLevels();
		base.Start();
	}

	void Click(int i)
	{
		character = characters[i];
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
		for (int i = 0; i < characters.Count; i++)
		{
			buttons[i].LevelUp = characters[i].Level > characters[i].Skilltree.PointsSpent;
		}
	}
}
