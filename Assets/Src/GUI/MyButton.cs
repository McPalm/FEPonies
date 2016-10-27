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
	[SerializeField]
	private Image chains;
	private bool pulse;

	Action<int> callback;
	int callbackInt;
	private float pulsecount = 0f;

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
			if (value == null) icon.gameObject.SetActive(false);
			else
			{
				icon.gameObject.SetActive(true);
				icon.sprite = value;
			}
		}
	}

	public bool chain
	{
		set
		{
			if (chains)
			{
				if(value) transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
				chains.gameObject.SetActive(value);
			}
		}
		get
		{
			if (chains == null) return false;
			return chains.gameObject.activeSelf;
		}
	}

	public bool Pulse
	{
		get
		{
			return pulse;
		}

		set
		{
			pulse = value;
		}
	}

	public bool HighLight
	{
		set
		{
			GetComponent<Image>().color = (value) ? Color.yellow : Color.white;
		}
	}

	public void Update()
	{
		if(pulse)
		{
			pulsecount += Time.deltaTime;
			transform.localScale = Vector3.one * (1.05f + 0.08f * Mathf.Sin(pulsecount * 2f));
		}
	}

	public void Register(Action<int> action, int i)
	{
		callbackInt = i;
		callback = action;
	}

	public void Click()
	{
		if(callback != null) callback(callbackInt);
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
