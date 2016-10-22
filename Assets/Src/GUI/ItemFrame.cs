using UnityEngine;
using System.Collections.Generic;

public class ItemFrame :  ScrollMenu {

	[SerializeField]
	ItemInfo template;

	List<ItemInfo> ilist;
	int count = 0;
	
	void Awake()
	{
		ilist = new List<ItemInfo>();
		ilist.Add(template);
		Clear();
	}
	
	public void Build(Backpack b)
	{
		Clear();
		int n = 0;
		foreach(Item i in b)
		{
			ItemInfo ii = GetNext();
			ii.Build(i);
			ii.transform.localPosition = new Vector3(0f, + 65 - n * 50 + 0);
			ii.gameObject.SetActive(true);
			ii.Equipped = (i is Equipment && b.IsEquipped((Equipment)i));
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
		
		return ilist[count-1];
	}

	public void Clear()
	{
		Height = 0;
		foreach(ItemInfo ii in ilist)
		{
			ii.gameObject.SetActive(false);
			ii.transform.localScale = Vector3.one;
		}
		count = 0;
	}
}
