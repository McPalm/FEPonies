using System;
using System.Collections.Generic;

[Serializable]
public struct SaveCharacter
{
    //TODO add abilities
    public Character character;
    public GearPackage backpack;
    public SkillTree skills;
    public bool isActive;
}
