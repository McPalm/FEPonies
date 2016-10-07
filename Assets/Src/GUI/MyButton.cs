using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent(typeof(Button))]
public class MyButton : MonoBehaviour {

	public Text label;

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

	public void Register(Action<int> action, int i)
	{
		callbackInt = i;
		callback = action;
	}

	public void Click()
	{
		callback(callbackInt);
	}
}
