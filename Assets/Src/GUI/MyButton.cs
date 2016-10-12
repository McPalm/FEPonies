using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler {

	public Text label;
	public Image icon;
	public bool Active = true;

	Action<int> callback;
	int callbackInt;

	public String Label
	{
		get
		{
			return label.text;
		}

		set
		{
			label.text = value;
		}
	}

	public Sprite Icon
	{
		get
		{
			return icon.sprite;
		}

		set
		{
			icon.sprite = value;
		}
	}

	public void Register(Action<int> action, int i)
	{
		callbackInt = i;
		callback = action;
	}

	public void Click()
	{
		callback(callbackInt);
	}

	public void OnPointerEnter(PointerEventData p)
	{
		if(Active)
			transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (Active)
			transform.localScale = Vector3.one;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (Active)
			transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		transform.localScale = Vector3.one;
	}
}
