using UnityEngine;
using System.Collections.Generic;

public class ItemFrame : ScrollMenu {

	[SerializeField]
	ItemInfo template;

	List<ItemInfo> ilist;
	int count = 0;
	Backpack client;

	System.Action<string, string, Sprite> setDescription;

	void Awake()
	{
		ilist = new List<ItemInfo>();
		ilist.Add(template);
		Clear();
	}

	public void Build(Backpack b, System.Action<string, string, Sprite> desc)
	{
		setDescription = desc;
		client = b;
		Clear();
		int n = 0;
		foreach (Item i in b)
		{
			ItemInfo ii = GetNext();
			ii.Build(i);
			ii.transform.localPosition = new Vector3(0f, +65 - n * 50 + 0);
			ii.gameObject.SetActive(true);
			ii.Equipped = (i is Equipment && b.IsEquipped((Equipment)i));
			ii.MouseoverCallback(MouseOver, i);
			n++;
		}
		Height = n * 50;
	}

	ItemInfo GetNext()
	{
		count++;
		if (count > ilist.Count)  // generate new button
		{
			ItemInfo ii = Instantiate<ItemInfo>(template);
			ii.transform.SetParent(anchor);
			ii.transform.localScale = Vector3.one;
			count++;
			ilist.Add(ii);
			return ii;
		}

		return ilist[count - 1];
	}

	public void Clear()
	{
		Height = 0;
		foreach (ItemInfo ii in ilist)
		{
			ii.gameObject.SetActive(false);
			ii.transform.localScale = Vector3.one;
		}
		count = 0;
	}

	public void MouseOver(Item i)
	{
		string body = "missing description";
		if (i is Weapon) body = GetDesc(i as Weapon);
		if (i is Armor) body = GetDesc(i as Armor);

		setDescription(i.Name, body, i.icon);
	}

	public string GetDesc (Weapon w)
	{
		WeaponDamage wd = w.attackInfo.Effect as WeaponDamage;

		string s = "Base Damage: " + wd.BaseDamage;
		if (wd.StrScale > 0) s += "\n +" + Mathf.RoundToInt(wd.StrScale * 100) + "% STR";
		if (wd.DexScale > 0) s += "\n +" + Mathf.RoundToInt(wd.DexScale * 100) + "% DEX";
		if (wd.IntScale > 0) s += "\n +" + Mathf.RoundToInt(wd.IntScale * 100) + "% INT";
		s += "\nTotal damage: " + wd.GetDamage(client.Owner.ModifiedStats);
		if (wd.DefenceMulitiplier == 1f && wd.ResistanceMultiplier == 0f) s += "\n vs Defence.";
		else if (wd.DefenceMulitiplier == 0f && wd.ResistanceMultiplier == 1f) s += "\n vs Resistance.";
		else if (wd.DefenceMulitiplier > 0f && wd.ResistanceMultiplier == 0f) s += "\n vs " + GUIUtility.FloatToPercent(wd.DefenceMulitiplier) +  " Defence.";
		else if (wd.DefenceMulitiplier == 0f && wd.ResistanceMultiplier > 0f) s += "\n vs " + GUIUtility.FloatToPercent(wd.ResistanceMultiplier) + " Resistance.";
		else s += "\n vs "  + GUIUtility.FloatToPercent(wd.DefenceMulitiplier) + " Defence & " + GUIUtility.FloatToPercent(wd.ResistanceMultiplier) + " Resistance.";


		s += "\n" + GUIUtility.StatsToString(w.buff);
		return s;
	}

	public string GetDesc(Armor a)
	{
		int maxload = client.Owner.ModifiedStats.CarryingCapacity;
		string s = "Weight: " + a.weight  + "/" + maxload;
		if (a.weight > maxload * 2) s += "(Too heavy)";
		else if(a.weight > maxload)  s += " (-1 speed, -" + GUIUtility.FloatToPercent((a.weight - maxload) * 0.05f) + " Dodge)"; // (equippedArmor.weight - owner.ModifiedStats.CarryingCapacity) * 0.05f
		else if (a.weight < maxload) s += " (+" + GUIUtility.FloatToPercent((1f - (a.weight/ maxload)) * 0.1f ) + " Dodge)"; //( 1f - (equippedArmor.weight / owner.ModifiedStats.CarryingCapacity) ) * 0.01f;
		s += "\n";
		s += GUIUtility.StatsToString(a.buff);
		return s;
	}
}
