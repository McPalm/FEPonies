using System;
using System.Collections.Generic;

[Serializable]
public struct SaveCharacter
{
    //TODO add abilities
    public int Level;
    public string Name;
    public bool isActive;
    public List<string> Backpack;
}
