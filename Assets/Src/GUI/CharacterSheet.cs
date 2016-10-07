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

	public void Build(Unit u)
	{
		nameText.text = u.name;
		SetPortrait(u);
		BuildStats(u);
		BuildAbilities(u);
		BuildItems(u);
	}

	private void BuildItems(Unit u)
	{
		Backpack b = u.GetComponent<Backpack>();
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

	private void BuildAbilities(Unit u)
	{
		string output = "";
		foreach(Skill a  in u.GetComponents<Skill>())
		{
			output += a.Name;
			if(a is Passive)
				output += "\n";
			else
				output += " \n";
		}
		abilityText.text = output;
	}

	private void SetPortrait(Unit u)
	{
		Sprite s = u.GetComponent<SpriteRenderer>().sprite;
		portrait.sprite = s;
	}

	private void BuildStats(Unit u)
	{
		Stats mystats = u.ModifiedStats;

		statText.text =
				u.level + "\n\n" +  // LEVEL

				u.CurrentHP + "/" + mystats.maxHP + "\n" + // HP
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
