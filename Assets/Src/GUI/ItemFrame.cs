using UnityEngine;
using System.Collections.Generic;

public class ItemFrame :  ScrollMenu {

	[SerializeField]
	ItemInfo template;
	
	void Awake()
	{
		template.gameObject.SetActive(false);
	}
	
	public void Build(Backpack b)
	{
		int n = 0;
		foreach(Item i in b)
		{
			ItemInfo ii = Instantiate<ItemInfo>(template);
			ii.transform.SetParent(anchor);
			ii.Build(i);
			ii.transform.localPosition = new Vector3(0f, + 65 - n * 50 + 0);
			ii.transform.localScale = Vector3.one;
			ii.gameObject.SetActive(true);
			ii.Equipped = (i is Equipment && b.IsEquipped((Equipment)i));
			n++;
		}
		Height = n * 50;
	}
}
