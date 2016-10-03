using UnityEngine;
using System.Collections;
using System;

public class LightningReflexes : Passive {
	public override string Name
	{
		get
		{
			return "Lightning Reflexes;";
		}
	}

	// Use this for initialization
	void Start () {
		GetComponent<Unit>().Retaliations += 2;
	}
}
