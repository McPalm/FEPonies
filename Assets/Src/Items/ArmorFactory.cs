using UnityEngine;
using System.Collections;

public class ArmorFactory
{

	int level = 1;
	int weight = 0;
	float resistance = 0.5f;
	string name;

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
			else if(value > 3) Debug.LogError("armour wiehgt cannot more than 3");
			else weight = value;
		}
	}

	public Armor GetArmour()
	{
		float power = level + 5;
		float advantages = 0f;
		float disadvantages = 0f;

		disadvantages += weight / 4f;

		Armor a = new Armor();
		a.weight = weight;
		a.buff = new Stats();
		a.Name = name;

		power /= 1f + advantages;
		power *= 1f + disadvantages;
		a.buff.defense = Mathf.RoundToInt(power * (1f - resistance) / 1.5f) + weight;
		a.buff.defense = Mathf.RoundToInt(power * resistance / 1.5f);



		level += 5;
		a.value = (int)((level * level) * (0.5f + weight));
		return a;
	}
}
