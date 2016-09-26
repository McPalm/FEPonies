using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// the type of weapon
/// </summary>
public enum WeaponType {
    axe, spear, sword
}

/// <summary>
/// class for weapons in the game
/// type - the type of the weapon, see enum WeaponType
/// </summary>
public class Weapon : Equipment
{
	public WeaponType type;
}
