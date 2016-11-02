using UnityEngine;
using System.Collections.Generic;

public class ItemFrame : ScrollMenu {

	[SerializeField]
	ItemInfo template;

	List<ItemInfo> ilist;
	int count = 0;

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
		return "I stab ponies!";
	}

	public string GetDesc(Armor a)
	{
		return "I stop ponies from getting stabbed";
	}
}
