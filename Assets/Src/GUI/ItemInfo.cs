using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	Image icon;
	[SerializeField]
	Text text;
	[SerializeField]
	Image equipped;
	[SerializeField]
	Image frame;

	System.Action<Item> mouseovercallback;
	Item callbackitem;

	public bool Equipped
	{
		set
		{
			equipped.gameObject.SetActive(value);
		}
	}

	public void Build(Item i)
	{
		icon.sprite = i.icon;
		text.text = i.Name;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		frame.transform.localScale = Vector3.one * 1.1f;
		if (mouseovercallback != null) mouseovercallback(callbackitem);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		frame.transform.localScale = Vector3.one;
	}

	public void MouseoverCallback(Action<Item> a, Item s)
	{
		mouseovercallback = a;
		callbackitem = s;
	}
}
