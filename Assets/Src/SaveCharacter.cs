using System;
using System.Collections.Generic;

[Serializable]
public struct SaveCharacter
{
	public string name;
    public Character character;
    public GearPackage backpack;
    public SkillTree skills;
    public bool isActive;
}
