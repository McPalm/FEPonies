using UnityEngine;
using System.Collections.Generic;

public class AbilityFrame : ScrollMenu {

	[SerializeField]
	MyButton template;

	List<MyButton> buttons;
	int count = 0;

	void Awake()
	{
		buttons = new List<MyButton>();
		buttons.Add(template);
		Clear();
	}

	public void Build(Character c)
	{
		Clear();
		int i = 0;
		foreach(string s in c.Skilltree.GetSkills(c.Level))
		{
			MyButton b = GetNext();
			Build(s, b);
			b.transform.localPosition = new Vector3(0f, +65 - i * 50 + 0);
			b.gameObject.SetActive(true);
			i++;
		}
		Height = i * 50;
	}

	public void Build(string s, MyButton b)
	{
		b.Label = s;
		b.Icon = SkillDB.GetIcon(s);
	}

	MyButton GetNext()
	{
		count++;
		if (count > buttons.Count)  // generate new button
		{
			MyButton mb = Instantiate<MyButton>(template);
			mb.transform.SetParent(anchor);
			mb.transform.localScale = Vector3.one;
			count++;
			buttons.Add(mb);
			return mb;
		}

		return buttons[count - 1];
	}

	public void Clear()
	{
		foreach(MyButton b  in buttons)
		{
			b.gameObject.SetActive(false);
			b.transform.localScale = Vector3.one;
		}
		count = 0;
	}
}