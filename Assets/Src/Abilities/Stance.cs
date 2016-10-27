using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Stance abilities
/// Activating a stance ability deactivates all other stances.
/// </summary>
abstract public class Stance : Ability, SustainedAbility {

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