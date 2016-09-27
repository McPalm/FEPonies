using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class AbilityGUI : MonoBehaviour {


	Unit client;

	public List<Text> abilitytexts;

	List<Ability> _abils;

	void Update()
	{
		switch (StateManager.Instance.State)
		{
			case GameState.unitSelected:
				if(client != Unit.SelectedUnit)
				{
					client = Unit.SelectedUnit;
					Build();
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
		for(int i = 0; i < 5; i++)
		{
			abilitytexts[i].text = (i + 1).ToString();
		}
	}

	private void Build()
	{
		Clear();
		client.GetComponents<Ability>(_abils = new List<Ability>());
		int i = 0;
		foreach(Ability a in _abils)
		{
			if (i == abilitytexts.Count) break;
			abilitytexts[i].text = a.Name;
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
	}
}
