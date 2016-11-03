using System;
using System.Collections.Generic;

public class WeaponDB
{
	private static WeaponDB instance;

	public static WeaponDB Instance
	{
		get
		{
			if (instance == null) instance = new WeaponDB();
			return instance;
		}
	}

	private HashSet<WeaponInfo> infos;

	WeaponDB()
	{
		infos = new HashSet<WeaponInfo>();
		List<WeaponInfo> list = new List<WeaponInfo>();
		list.Add(new WeaponInfo("Battle Axe", 1, WeaponType.axe, 1, 0, 0));
		list.Add(new WeaponInfo("Crossbow", 1, WeaponType.crossbow, 0, 1, 0));
		list.Add(new WeaponInfo("Dagger", 1, WeaponType.dagger, 1, 2, 0));
		list.Add(new WeaponInfo("Pyromancers Tome", 1, WeaponType.tome, 0, 0, 1));
		list.Add(new WeaponInfo("Short Spear", 1, WeaponType.spear, 2, 1, 0));
		list.Add(new WeaponInfo("Short Sword", 1, WeaponType.sword, 2, 3, 0));
		list.Add(new WeaponInfo("Lightning Rod", 2, WeaponType.tome, 0, 2, 3));
		list.Add(new WeaponInfo("Warhammer", 2, WeaponType.axe, 1, 0, 0));
		list.Add(new WeaponInfo("Javelin", 3, WeaponType.spear, 2, 3, 0));
		list.Add(new WeaponInfo("Long Sword", 2, WeaponType.sword, 3, 2, 0));
		list.Add(new WeaponInfo("Halberd", 4, WeaponType.axe, 2, 1, 0));
		list.Add(new WeaponInfo("Rapier", 5, WeaponType.sword, 1, 2, 0));
		list.Add(new WeaponInfo("Arbalest", 6, WeaponType.crossbow, 0, 1, 0));
		list.Add(new WeaponInfo("Sanguine Tome", 6, WeaponType.tome, 0, 0, 1));
		list.Add(new WeaponInfo("Naginata", 7, WeaponType.spear, 2, 3, 0));
		list.Add(new WeaponInfo("Great Axe", 7, WeaponType.axe, 1, 0, 0));
		list.Add(new WeaponInfo("Greatsword", 7, WeaponType.sword, 2, 1, 0));
		list.Add(new WeaponInfo("Estoc", 7, WeaponType.sword, 1, 2, 0));
		list.Add(new WeaponInfo("Flaming Sword", 8, WeaponType.sword, 2, 1, 2));
		list.Add(new WeaponInfo("Lightning Rapier", 8, WeaponType.sword, 1, 2, 2));
		list.Add(new WeaponInfo("Assassin Blades", 10, WeaponType.dagger, 0, 1, 0));
		list.Add(new WeaponInfo("Burning Axe", 11, WeaponType.axe, 2, 0, 1));
		list.Add(new WeaponInfo("Heartseeker", 15, WeaponType.crossbow, 0, 1, 0));

		foreach(WeaponInfo i in list)
		{
			infos.Add(i);
			infos.Add(new WeaponInfo(i, 1));
			infos.Add(new WeaponInfo(i, 2));
			infos.Add(new WeaponInfo(i, 3));
			infos.Add(new WeaponInfo(i, 4));
			infos.Add(new WeaponInfo(i, 5));
		}
	}

	/// <summary>
	/// get a suitable weapon for the user
	/// </summary>
	/// <param name="wt"></param>
	/// <param name="dex">if you preffer dexterity based weapons over regular ones</param>
	/// <returns></returns>
	public Weapon GetWeapon(int level, WeaponType wt, bool dex = false)
	{
		List<WeaponInfo> list = new List<WeaponInfo>();
		foreach(WeaponInfo w in infos)
		{
			if (w.type == wt && w.level <= level) list.Add(w);
		}
		list.Sort();
		if(dex)
		{
			for(int i = list.Count-1; i >= 0; i--)
			{
				if (dex && list[i].dex > list[i].str) return GetWeapon(list[i].fullName);
				else if (!dex && list[i].dex < list[i].str) return GetWeapon(list[i].fullName);
			}
		}
		return GetWeapon(list[list.Count-1].fullName);
	}

	/// <summary>
	/// Gets a weapon of a certain type and leveld up to wanted item level.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="level">maximum level of the item.</param>
	/// <returns></returns>
	public Weapon GetWeapon(string name, int level)
	{
		List<WeaponInfo> list = new List<WeaponInfo>();
		foreach(WeaponInfo w in infos)
		{
			if (w.name == name && w.level <= level) list.Add(w);
		}
		if (list.Count == 0) return GetWeapon(name);
		list.Sort();
		return GetWeapon(list[list.Count - 1].fullName);
	}

	public Weapon GetWeapon(string name)
	{
		WeaponInfo wi = null;
		foreach (WeaponInfo w in infos)
			if (w.fullName == name) wi = w;
		if (wi == null)
			return null;

		// generate weapon
		WeaponFactory wf = new WeaponFactory(wi.fullName);
		wf.Level = wi.level;
		wf.SetScaling(wi.str, wi.dex, wi.inte);

		switch (wi.name)
		{
			case "Battle Axe":
				wf.HighCrit();
				break;
			case "Crossbow":
				wf.LowScaling();
				wf.SetLongRange();
				break;
			case "Dagger":
				wf.HighCrit();
				wf.ArmorPenetrating();
				wf.LowScaling();
				break;
			case "Pyromancers Tome":
				wf.Magic();
				wf.SetMeleeAndRange();
				break;
			case "Short Spear":
				wf.LowScaling();
				wf.SetMeleeAndRange();
				break;
			case "Short Sword":
				wf.HighHit();
				break;
			case "Lightning Rod":
				wf.LowHit();
				wf.HighCrit();
				wf.Magic();
				wf.SetMeleeAndRange();
				break;
			case "Warhammer":
				wf.LowHit(3);
				wf.ArmorPenetrating();
				wf.HighScaling();
				break;
			case "Javelin":
				wf.LowHit();
				wf.SetMeleeAndRange();
				break;
			case "Halberd":
				wf.HighScaling();
				break;
			case "Rapier":
				wf.HighScaling();
				wf.ArmorPenetrating();
				break;
			case "Arbalest":
				wf.LowScaling();
				wf.ArmorPenetrating();
				wf.SetLongRange();
				break;
			case "Sanguine Tome":
				wf.LowScaling();
				wf.Magic();
				wf.SetMeleeAndRange();
				UnityEngine.Debug.LogWarning("Still no lifesteal on Sanguine Tome."); // TODO
				break;
			case "Naginata":
				wf.LowScaling();
				wf.SetMeleeAndRange();
				UnityEngine.Debug.LogWarning("No AGI bonus on naginatas."); // TODO
				break;
			case "Great Axe":
				wf.HighScaling();
				wf.HighCrit();
				wf.LowHit();
				break;
			case "Greatsword":
				wf.HighScaling();
				wf.HighCrit();
				wf.LowHit();
				break;
			case "Estoc":
				wf.ArmorPenetrating();
				wf.LowHit();
				break;
			case "Flaming Sword":
				wf.HighHit();
				wf.SetHybrid();
				break;
			case "Lightning Rapier":
				wf.HighScaling();
				wf.SetHybrid();
				wf.LowHit();
				wf.HighCrit();
				break;
			case "Lightning Crossbow":
				wf.LowScaling();
				wf.SetHybrid();
				wf.HighCrit();
				wf.SetLongRange();
				break;
			case "Assassin Blades":
				wf.HighCrit();
				wf.LowHit();
				UnityEngine.Debug.LogWarning("No AGI bonus for Assasin Blades yet!"); // TODO
				break;
			case "Burning Axe":
				wf.HighCrit();
				wf.LowHit();
				wf.SetHybrid();
				break;
			case "Heartseeker":
				wf.HighCrit();
				wf.LowHit();
				wf.LowScaling();
				wf.SetLongRange();
				break;
			default:
				UnityEngine.Debug.LogError("WeaponDB: " + wf.Name + " is not implemented.");
				break;
		}

		return wf.GetWeapon();
	}

	/// <summary>
	/// Gets the item level of a specific weapon
	/// </summary>
	/// <param name="weaponName"></param>
	/// <returns>level of the item, 0 if not found.</returns>
	public int GetLevel(string weaponName)
	{
		foreach(WeaponInfo w in infos)
		{
			if (w.fullName == weaponName) return w.level;
		}
		return 0;
	}

	private class WeaponInfo : IComparable<WeaponInfo>
	{
		readonly public int level;
		readonly public WeaponType type;
		readonly public string name;
		readonly public string fullName;
		readonly public int str;
		readonly public int dex;
		readonly public int inte;

		public WeaponInfo(string name, int level, WeaponType type, int str, int dex, int inte)
		{
			this.level = level;
			this.type = type;
			this.name = name;
			this.fullName = name;
			this.str = str;
			this.dex = dex;
			this.inte = inte;
		}

		public WeaponInfo(WeaponInfo w, int magic)
		{
			this.level = w.level + magic*2;
			this.type = w.type;
			this.name = w.name;
			this.fullName = name + "+" + magic;
			this.str = w.str;
			this.dex = w.dex;
			this.inte = w.inte;
		}

		public int CompareTo(WeaponInfo other)
		{
			return this.level - other.level;
		}
	}
}
