using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollMenu : MonoBehaviour {

	int height;
	Vector3 origin;
	Vector3 bottom;

	public bool inverted = false;
	public RectTransform anchor;
	public RectMask2D mask;
	public Scrollbar scrollbar;

	public int Height
	{
		get
		{
			return height;
		}

		set
		{
			if (value > mask.rectTransform.sizeDelta.y)
			{
				if (scrollbar) scrollbar.gameObject.SetActive(true);
				if (inverted)
					bottom = origin - new Vector3(0f, value - mask.rectTransform.sizeDelta.y, 0f);
				else
					bottom = origin + new Vector3(0f, value - mask.rectTransform.sizeDelta.y, 0f);
			}
			else
			{
				bottom = origin;
				if (scrollbar) scrollbar.gameObject.SetActive(false);
			}
			height = value;
		}
	}

	public void Start()
	{
		
		origin = anchor.localPosition;
		Height = height;
	}

	public void Scroll(float f)
	{
		if(inverted)
			anchor.localPosition = Vector3.Lerp(bottom, origin, 1f-f);
		else
			anchor.localPosition = Vector3.Lerp(bottom, origin, f);

	}
}
