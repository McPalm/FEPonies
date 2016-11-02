using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CharacterSheet : MonoBehaviour {

	public Text nameText;
	public Image portrait;
	public Text statText;
	[SerializeField]
	ItemFrame itemFrame;
	[SerializeField]
	AbilityFrame abilityFrame;
	[SerializeField]
	Text descriptionTitle;
	[SerializeField]
	Text descriptionText;
	[SerializeField]
	Image descriptionPicture;

	private Unit client;

	public void Build(Character c)
	{
		nameText.text = c.Name;
		SetPortrait(c);
		BuildStats(c);
		abilityFrame.Build(c, SetDescription);
		itemFrame.Build(c.Backpack, SetDescription);
		SetDescription("", "", null);
	}

	private void SetPortrait(Character c)
	{
		if (c.MugShot != null)
			portrait.sprite = c.MugShot;
		else try
		{
			Sprite s = c.Sprite;
			portrait.sprite = s;
		}
		catch(Exception e)
		{
			Debug.LogError(e);
		}
		
	}

	private void BuildStats(Character c)
	{
		Stats mystats = c.ModifiedStats;

		int currenthp = mystats.maxHP;
		Unit u = null; // TODO, get max HP in combat scenes. c.GetComponent<Unit>();
		if(u != null)
		{
			currenthp = u.CurrentHP;
		}

		statText.text =
				c.Level + "\n\n" +  // LEVEL

				currenthp + "/" + mystats.maxHP + "\n" + // HP
				mystats.strength + "\n" + // STR
				mystats.agility + "\n" + // AGI
				mystats.dexterity + "\n" + //DEX
				mystats.intelligence + "\n\n" + //INT

				100 * mystats.Hit + "%\n" + //HIT
				100 * mystats.Crit + "%\n" + //CRIT
				100 * mystats.Dodge + "%\n" + //DODGE
				100 * mystats.CritDodge + "%\n" + //CRIT.D
				mystats.defense + "\n" + //ARMOR
				mystats.resistance; //RESISTANCE
	}

	public void SetDescription(string title, string body, Sprite image)
	{
		descriptionTitle.text = title;
		descriptionText.text = body;
		descriptionPicture.gameObject.SetActive(image != null);
		descriptionPicture.sprite = image;
	}
}
