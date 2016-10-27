using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class AbilityGUI : MonoBehaviour {


	Unit client;

	public List<MyButton> buttons;

	List<Ability> _abils;

	void Update()
	{
		switch (StateManager.Instance.State)
		{
			case GameState.unitSelected:
				if(client != Unit.SelectedUnit)
				{
					Build(Unit.SelectedUnit);
				}
				if (Input.GetButtonDown("a1")) Use(0);
				else if (Input.GetButtonDown("a2")) Use(1);
				else if (Input.GetButtonDown("a3")) Use(2);
				else if (Input.GetButtonDown("a4")) Use(3);
				else if (Input.GetButtonDown("a5")) Use(4);
				break;
			case GameState.playerTurn:
				Clear();
				break;
		}
	}

	private void Clear()
	{
		client = null;
		for(int i = 0; i < 5; i++)
		{
			buttons[i].Label  = (i + 1).ToString();
			buttons[i].chain = false;
			buttons[i].HighLight = false;
			buttons[i].Icon = null;
		}
	}

	private void Build(Unit client)
	{
		Clear();
		this.client = client;
		client.GetComponents<Ability>(_abils = new List<Ability>());
		int i = 0;
		foreach(Ability a in _abils)
		{
			if (i == buttons.Count) break;
			if(a is SustainedAbility)
			{
				if ((a as SustainedAbility).Active) buttons[i].HighLight = true;
			}
			buttons[i].Label = a.Name;
			i++;
			
		}
	}

	public void Use(int button)
	{
		if(StateManager.Instance.State == GameState.unitSelected)
		{
			if (button < _abils.Count)
			{
				_abils[button].Use();
			}
		}
		if(StateManager.Instance.State == GameState.unitSelected)
		{
			Build(client);
		}
	}

	void Start()
	{
		Clear();
	}
}
