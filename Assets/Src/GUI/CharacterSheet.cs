using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CharacterSheet : MonoBehaviour {

	public Text nameText;
	public Image portrait;
	public Text statText;
	public Text abilityText;
	[SerializeField]
	ItemFrame itemFrame;

	private Unit client;

	public void Build(Character c)
	{
		nameText.text = c.Name;
		SetPortrait(c);
		BuildStats(c);
		BuildAbilities(c);
		itemFrame.Build(c.GetComponent<Backpack>());
	}

	private void BuildAbilities(Character c)
	{
		string output = "";
		foreach(Skill a in c.GetComponents<Skill>())
		{
			output += a.Name;
			if(a is Passive)
				output += "\n";
			else
				output += " \n";
		}
		abilityText.text = output;
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
		catch(Exception e)
		{
			Debug.LogError(e);
		}
		
	}

	private void BuildStats(Character c)
	{
		Stats mystats = c.ModifiedStats;

		int currenthp = mystats.maxHP;
		Unit u = c.GetComponent<Unit>();
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
}
