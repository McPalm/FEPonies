using System.Collections.Generic;
using System;

public class ArmorDB
{
	static ArmorDB instance;
	HashSet<ArmorStats> adb;

	public static ArmorDB Instance
	{
		get
		{
			if (instance == null) instance = new ArmorDB();
			return instance;
		}
	}

	ArmorDB()
	{
		List<ArmorStats> list = new List<ArmorStats>();
		list.Add(new ArmorStats("Plain Robes", 1, 0, 1f));
		list.Add(new ArmorStats("Leather Armor", 1, 2, 0.5f));
		list.Add(new ArmorStats("Ring Mail", 1, 5, 0.4f));
		list.Add(new ArmorStats("Cuirass", 1, 8, 0.3f));
		list.Add(new ArmorStats("Splint Mail", 1, 10, 0.25f));
		list.Add(new ArmorStats("Thieves Cape", 3, 1, 0.5f));
		list.Add(new ArmorStats("Voluminous Robes", 3, 1, 0.8f));
		list.Add(new ArmorStats("Ring Vest", 4, 3, 0.5f));
		list.Add(new ArmorStats("Full Plate", 4, 12, 0.25f));
		list.Add(new ArmorStats("Magicans Robes", 5, 0, 1f));
		list.Add(new ArmorStats("Brass Cuirass", 6, 8, 0.7f));
		list.Add(new ArmorStats("Dapper Vest", 8, 2, 0.75f));
		list.Add(new ArmorStats("Mithral Ring Mail", 10, 4, 0.5f));
		list.Add(new ArmorStats("Wyrm Scale", 10, 8, 0.5f));
		list.Add(new ArmorStats("Wyrm Plate", 10, 12, 0.5f));
		list.Add(new ArmorStats("Stone Plate", 10, 20, 0.35f));

		adb = new HashSet<ArmorStats>();
		foreach(ArmorStats a in list)
		{
			adb.Add(a);
			adb.Add(new ArmorStats(a.name, a.level + 2, a.weight, a.resistance, 1));
			adb.Add(new ArmorStats(a.name, a.level + 4, a.weight, a.resistance, 2));
			adb.Add(new ArmorStats(a.name, a.level + 6, a.weight, a.resistance, 3));
			adb.Add(new ArmorStats(a.name, a.level + 8, a.weight, a.resistance, 4));
			adb.Add(new ArmorStats(a.name, a.level + 10, a.weight, a.resistance, 5));
		}
	}

	public Armor GetArmor(int level, int weight)
	{
		List<ArmorStats> list = new List<ArmorStats>();
		foreach(ArmorStats astats in adb)
		{
			if (astats.level <= level && astats.weight <= weight) list.Add(astats);
			list.Sort();
		}
		string name = list[list.Count-1].fullName;
		return GetArmor(name);
	}

	/// <summary>
	/// Gets a specific armor and applies +'s on it to match the level
	/// </summary>
	/// <param name="name">name without +'es</param>
	/// <param name="level">max level of it item</param>
	/// <returns></returns>
	public Armor GetArmor(string name, int level)
	{
		List<ArmorStats> list = new List<ArmorStats>();
		foreach(ArmorStats astats in adb)
		{
			if (astats.name == name && astats.level <= level) list.Add(astats);
		}
		if (list.Count == 0) return GetArmor(name);
		list.Sort();
		return GetArmor(list[list.Count - 1].fullName);
	}

	/// <summary>
	/// Get an armour by name
	/// </summary>
	/// <param name="name">name of armour, include +1, +2 etc if needed</param>
	/// <returns>If no such armour exsist, null will be returned</returns>
	public Armor GetArmor(string name)
	{
		ArmorStats a = null;
		foreach(ArmorStats b in adb)
		{
			if (b.fullName == name) a = b;
		}
		if (a == null) return null;

		ArmorFactory af = new ArmorFactory(a.fullName);
		af.Weight = a.weight;
		af.Level = a.level;
		af.Resistance = a.resistance;
		// Special attributes
		switch(a.name)
		{
			case "Thieves Cape":
				af.AgilityBonus();
				break;
			case "Mithral Ring Mail":
			case "Wyrm Scale":
				af.DexterityBonus();
				break;
			case "Magican Robes":
			case "Dapper Vest":
				af.IntelligenceBonus();
				break;
			case "Brass Cuirass":
			case "Wyrm Plate":
				af.StrenghtBonus();
				break;
		}

		return af.GetArmour();
	}

	/// <summary>
	/// Gets the item level of a specific armor.
	/// </summary>
	/// <param name="armorName"></param>
	/// <returns>level of the item, 0 if not found.</returns>
	public int GetLevel(string armorName)
	{
		foreach (ArmorStats w in adb)
		{
			if (w.fullName == armorName) return w.level;
		}
		return 0;
	}

	private class ArmorStats : IComparable<ArmorStats>
	{
		public readonly int level;
		public readonly int weight;
		public readonly string name;
		public readonly int magic;
		public readonly float resistance;
		public readonly string fullName;

		public ArmorStats(string name, int level, int weight, float resistance, int magic = 0)
		{
			this.level = level;
			this.weight = weight;
			this.name = name;
			this.resistance = resistance;
			this.magic = magic;
			if (magic > 0)
				fullName = name + "+" + magic;
			else
				fullName = name;
		}

		public int CompareTo(ArmorStats other)
		{
			return level * 2 + weight - other.level * 2 - other.weight;
		}
	}
}
