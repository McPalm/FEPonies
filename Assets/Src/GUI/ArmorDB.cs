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
		list.Add(new ArmorStats("Plain Robes", 1, 0));
		list.Add(new ArmorStats("Leather Armor", 1, 2));
		list.Add(new ArmorStats("Ring Mail", 1, 5));
		list.Add(new ArmorStats("Cuirass", 1, 8));
		list.Add(new ArmorStats("Splint Mail", 1, 10));
		list.Add(new ArmorStats("Thieves Cape", 3, 1));
		list.Add(new ArmorStats("Voluminous Robes", 3, 1));
		list.Add(new ArmorStats("Ring Vest", 4, 3));
		list.Add(new ArmorStats("Full Plate", 4, 12));
		list.Add(new ArmorStats("Magicans Robes", 5, 0));
		list.Add(new ArmorStats("Brass Cuirass", 6, 8));
		list.Add(new ArmorStats("Dapper Vest", 8, 2));
		list.Add(new ArmorStats("Mithral Ring Mail", 10, 4));
		list.Add(new ArmorStats("Wyrm Scale", 10, 8));
		list.Add(new ArmorStats("Wyrm Plate", 10, 12));
		list.Add(new ArmorStats("Stone Plate", 10, 20));

		adb = new HashSet<ArmorStats>();
		foreach(ArmorStats a in list)
		{
			adb.Add(a);
			adb.Add(new ArmorStats(a.name, a.level + 2, a.weight, 1));
			adb.Add(new ArmorStats(a.name, a.level + 4, a.weight, 2));
			adb.Add(new ArmorStats(a.name, a.level + 6, a.weight, 3));
			adb.Add(new ArmorStats(a.name, a.level + 8, a.weight, 4));
			adb.Add(new ArmorStats(a.name, a.level + 10, a.weight, 5));
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
			case "Brass Curiass":
			case "Wyrm Plate":
				af.StrenghtBonus();
				break;
		}

		return af.GetArmour();
	}

	private class ArmorStats : IComparable<ArmorStats>
	{
		public readonly int level;
		public readonly int weight;
		public readonly string name;
		public readonly int magic;
		public readonly string fullName;

		public ArmorStats(string name, int level, int weight, int magic = 0)
		{
			this.level = level;
			this.weight = weight;
			this.name = name;
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
