using UnityEngine;
using System.Collections.Generic;
using System;

public class Thievery : Passive {
	public override string Name
	{
		get
		{
			return "Thievery";
		}
	}

	void Start()
	{
		gameObject.AddComponent<Backstab>();
	}

}