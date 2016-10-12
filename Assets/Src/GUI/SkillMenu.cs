using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SkillMenu : ScrollMenu {

	public Text nameText;
	public Image portrait;


	public void Build(Character c)
	{
		nameText.text = c.Name;
		SetPortrait(c);

		Height = 1000;
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
}
