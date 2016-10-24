using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BackpackButton : MonoBehaviour {

	public List<Button> buttons;

	List<Text> labels;
	bool open = false;

	// Use this for initialization
	void Start () {
		labels = new List<Text>();
		foreach(Button b in buttons)
		{
			labels.Add(b.gameObject.GetComponentInChildren<Text>(true));
		}
	}

	public void Open()
	{
		if (open && StateManager.Instance.State == GameState.buttonMenu) Close();
		// only open when a unit is selected
		else if(StateManager.Instance.State == GameState.unitSelected)
		{
			StateManager.Instance.Push(GameState.buttonMenu);
			Backpack b = Unit.SelectedUnit.Character.Backpack;
			if(b != null)
			{
				int i = 0;
				foreach(Item t in b)
				{
					buttons[i].gameObject.SetActive(true);
					labels[i].text = t.Name;
					i++;
				}
				open = true;
			}
			else
			{
				Debug.LogError("No backpack! Cannot open backpack!");
			}
		}
		else
		{
			print("Cannot open when no unit is selected!");
		}
	}

	public void Close()
	{
		foreach(Button b in buttons)
		{
			b.gameObject.SetActive(false);
		}
		open = false;
		if (StateManager.Instance.State == GameState.buttonMenu) StateManager.Instance.Pop();
	}

	public void Click(int button)
	{
		if(Unit.SelectedUnit.GetComponent<Backpack>().Use(button))
		{
			Close();
			Unit.SelectedUnit.FinnishMovement();
		}
	}
}
