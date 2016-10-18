using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SkillMenu : ScrollMenu {

	public Text nameText;
	public Image portrait;
	[SerializeField]
	private MyButton skillButton;

	public Sprite str;
	public Sprite dex;
	public Sprite agi;
	public Sprite inte;
	public Sprite missing;

	List<MyButton> buttons;
	int countButton;

	Character active;

	/// <summary>
	/// Builds up the skill tree
	/// </summary>
	/// <param name="c">subject character</param>
	public void Build(Character c)
	{
		active = c;
		if (buttons == null) buttons = new List<MyButton>();
		Reset();
		nameText.text = c.Name;
		SetPortrait(c);

		Height = 90 * 15;

		BuildSkills(c.Skilltree, c.Level);
	}

	private void BuildSkills(SkillTree skilltree, int level)
	{
		skillButton.transform.localPosition = new Vector3(1000, 1000);
		int i = 0;
		bool locked = true;
		bool available = true;
		foreach(SkillTree.SkillTreeLevel stl in skilltree.skills)
		{
			if (i >= level) available = false;
			if (locked == false) available = false;
			if (stl.Selected == "") locked = false;
			if(stl.option2 == "") // static
			{
				BuildButton(new Vector3(0, -194 + 90 * i, 0), stl.option1, i*10, available &! locked, !available);
			}
			else // options
			{
				if (locked)
				{
					BuildButton(new Vector3(-60, -194 + 90 * i, 0), stl.option1, i * 10, false, (stl.Choise == 2));
					BuildButton(new Vector3(60, -194 + 90 * i, 0), stl.option2, i * 10 + 1, false, (stl.Choise == 1));
				}
				else
				{
					BuildButton(new Vector3(-60, -194 + 90 * i, 0), stl.option1, i * 10, available & !locked, !available);
					BuildButton(new Vector3(60, -194 + 90 * i, 0), stl.option2, i * 10 + 1, available & !locked, !available);
				}
			}
			i++;
		}
	}

	private void BuildButton(Vector3 v3, string n, int callback, bool enabled, bool chain)
	{
		MyButton mb = GetButton();
		mb.transform.localPosition = v3;
		if (n == "Str") mb.icon.sprite = str;
		else if (n == "Dex") mb.icon.sprite = dex;
		else if (n == "Agi") mb.icon.sprite = agi;
		else if (n == "Int") mb.icon.sprite = inte;
		else mb.icon.sprite = missing;
		if (enabled) mb.Register(Select, callback);
		mb.Pulse = enabled;
		mb.chain = chain;
		mb.Active = enabled;
	}

	private void SetPortrait(Character c)
	{
		if (c.MugShot != null)
			portrait.sprite = c.MugShot;
		else try
			{
				Sprite s = c.GetComponent<SpriteRenderer>().sprite;
				portrait.sprite = s;
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

	}

	private MyButton GetButton()
	{
		if(buttons.Count <= countButton)
		{
			// make new button
			GameObject go = Instantiate<GameObject>(skillButton.gameObject);
			MyButton mb = go.GetComponent<MyButton>();
			go.transform.SetParent(anchor.transform);
			go.transform.localScale = Vector3.one;
			buttons.Add(mb);
		}
		countButton++;
		return buttons[countButton - 1];
	}

	private void Reset()
	{
		foreach(MyButton b in buttons)
		{
			b.transform.localPosition = new Vector3(2000, 2000);
			b.Register(null, 0);
		}
		countButton = 0;
	}

	private void Select(int i)
	{
		active.Skilltree.skills[i / 10].Choise = i % 10 + 1;
		active.Skilltree.CalculateStats(active.Level);
		Build(active);
	}
}
