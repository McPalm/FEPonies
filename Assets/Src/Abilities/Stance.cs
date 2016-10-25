using UnityEngine;
using System.Collections.Generic;
using System;

abstract public class Stance : Ability {

	public override void Use()
	{
		if (Active) Active = false;
		else
		{
			foreach (Stance s in GetComponents<Stance>())
			{
				s.Active = false;
			}
			Active = true;
		}
	}

	abstract public bool Active
	{
		get;
		protected set;
	}
}