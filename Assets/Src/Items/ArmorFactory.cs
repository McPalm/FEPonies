using UnityEngine;
using System.Collections;

public class ArmorFactory
{

	int level = 1;
	int weight = 0;
	float resistance = 0.5f;
	string name;
	bool agility = false;
	bool intelligence = false;
	bool dexterity = false;
	bool strenght = false;

	public ArmorFactory(string name = "")
	{
		this.name = name;
	}

	public int Level
	{
		set
		{
			if (value < 1) Debug.LogError("Item level cannot be lower than 1!");
			else level = value;
		}
	}

	public float Resistance
	{
		set
		{
			if (resistance < 0f) Debug.LogError("Resistance cannot be lower than 0");
			else if (resistance > 1f) Debug.LogError("Resistance cannot be higher than 1");
			else resistance = value;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}

		set
		{
			name = value;
		}
	}

	public int Weight
	{
		set
		{
			if (value < 0) Debug.LogError("armour wiehgt cannot be less than 0");
			else weight = value;
		}
	}

	public void AgilityBonus()
	{
		agility = true;
	}

	public void StrenghtBonus()
	{
		strenght = true;
	}

	public void IntelligenceBonus()
	{
		intelligence = true;
	}

	public void DexterityBonus()
	{
		dexterity = true;
	}

	public Armor GetArmour()
	{
		float power = level + 5;
		float advantages = 0f;
		float disadvantages = 0f;
		
		// count advantages/disadvantages
		disadvantages += weight / 20f;

		if (strenght) advantages += 0.25f;
		if (dexterity) advantages += 0.25f;
		if (agility) advantages += 0.25f;
		if (intelligence) advantages += 0.25f;
		
		advantages += Mathf.Max(resistance, 1f - resistance) - 0.5f; // specialization penalty

		power /= 1f + advantages;
		power *= 1f + disadvantages;

		// set base stats
		Armor a = new Armor();
		a.weight = weight;
		a.buff = new Stats();
		a.Name = name;

		a.buff.defense = Mathf.RoundToInt(power * 0.99f* (1f - resistance) + weight * 0.25f);
		a.buff.resistance = Mathf.RoundToInt(power * 1.01f * resistance);

		// apply special attributes
		if (strenght) a.buff.strength = 1 + (int)(power / 4f);
		if (dexterity) a.buff.dexterity = 1 + (int)(power / 4f);
		if (agility) a.buff.agility = 1 + (int)(power / 4f);
		if (intelligence) a.buff.intelligence = 1 + (int)(power / 4f);

		// calculate value
		level += 5;
		a.value = (int)((level * power) * (0.5f + weight * 0.1f));

		return a;
	}
}
