using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CharacterSheet : MonoBehaviour {

	public Text nameText;
	public Image portrait;
	public Text statText;
	public Text abilityText;
	public Text itemText;

	private Unit client;

	public void Build(Character c)
	{
		nameText.text = c.Name;
		SetPortrait(c);
		BuildStats(c);
		BuildAbilities(c);
		BuildItems(c);
	}

	private void BuildItems(Character c)
	{
		Backpack b = c.GetComponent<Backpack>();
		if (b != null)
		{
			string val = "";
			foreach (Item i in b)
			{
				val += i.Name;
				if (i is Equipment && b.IsEquipped((Equipment)i)) val += " (equipped)";
				val += "\n";
			}
			itemText.text = val;
		}
		else
		{
			itemText.text = "Error: Missing backpack.";
		}
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
		try
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
				c.level + "\n\n" +  // LEVEL

				currenthp + "/" + mystats.maxHP + "\n" + // HP
				mystats.strength + "\n" + // STR
				mystats.agility + "\n" + // AGI
				mystats.dexterity + "\n" + //DEX
				mystats.intelligence + "\n\n" + //INT

				100 * mystats.Hit + "%\n" + //HIT
				100 * mystats.Dodge + "%\n" + //DODGE
				100 * mystats.Crit + "%\n" + //CRIT
				100 * mystats.CritDodge + "%\n\n" + //CRIT.D

				mystats.defense + "\n" + //ARMOR
				mystats.resistance + "\n" + //RESISTANCE
				(mystats.might + mystats.strength) + "\n"; //DAMAGE TODO, make it work for casters
	}
}
