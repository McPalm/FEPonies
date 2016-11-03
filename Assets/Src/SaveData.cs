using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<SaveCharacter> Roster;
    public string Checkpoint;
    public DateTime saveTime;
    public GearPackage train;

    public SaveData()
    {
        Roster = new List<SaveCharacter>();
        Checkpoint = "";
    }
}
