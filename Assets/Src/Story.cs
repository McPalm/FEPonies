using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class Story : MonoBehaviour {


	string checkPoint;
    /// <summary>
	/// Name of the level that is the current checkpoint
	/// </summary>
	public string Checkpoint
	{
		get { return checkPoint; }
        set { checkPoint = value; }
	}

    //Singleton stuff
    static private Story instance;

    static public Story Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    //End of Singleton stuff

    /// <summary>
    /// Save the game and marks a checkpoint
    /// also saves all character in the UnitRoster
    /// </summary>
    public void Save()
    {
        Save("SaveGame");
        Debug.Log("Saved!");
    }
    /// <summary>
    /// Save the game with supplied filename and marks a checkpoint
    /// also saves all character in the UnitRoster
    /// </summary>
    public void Save(string filename)
	{
        //Preparing save data
        SaveData saveData = new SaveData();
        saveData.Checkpoint = checkPoint;
        UnitRoster roster = GetComponent<UnitRoster>();
        saveData.Roster = new List<SaveCharacter>();
        foreach(Unit u in roster.Roster)
        {
            SaveCharacter temp;
            temp.Level = u.Character.Level;
            temp.Name = u.name;
            Backpack tempBackPack = u.GetComponent<Backpack>();
            temp.Backpack = new List<string>();
            if (tempBackPack != null)
            {
                foreach (Item i in tempBackPack)
                {
                    temp.Backpack.Add(i.Name);
                }
            }
            if(roster.activeRoster.Contains(u))
            {
                temp.isActive = true;
            }
            else
            {
                temp.isActive = false;
            }
            saveData.Roster.Add(temp);
        }
        saveData.saveTime = DateTime.Now;
        //TODO add abilities
        //Now let's write it to a file
        BinaryFormatter binary = new BinaryFormatter();
        Debug.Log(Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/"+ filename+ ".sav");
        binary.Serialize(file, saveData);
        file.Close();
    }

    /// <summary>
	/// Load a game from the checkpoint being stored in this class
    /// and stuffs all character info from the savefile into the UnitRoster
	/// </summary>
    public void Load()
    {
        Load("SaveGame");
    }

	/// <summary>
	/// Load a game from provided filename from the checkpoint being stored in this class
    /// and stuffs all character info from the savefile into the UnitRoster
	/// </summary>
	public void Load(string filename)
	{
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/"+filename+".sav", FileMode.Open);
        SaveData saveData= (SaveData)binary.Deserialize(file);
        file.Close();
        checkPoint = saveData.Checkpoint;
        UnitRoster roster = GetComponent<UnitRoster>();
        foreach(SaveCharacter character in saveData.Roster)
        {
            Unit tempUnit = roster.GetUnit(character.Name);
            if (tempUnit != null)
            {
                tempUnit.Character.Level = character.Level;
                if (character.isActive)
                {
                    if(!roster.activeRoster.Contains(tempUnit))
                    {
                        roster.activeRoster.Add(tempUnit);
                    }
                }
                else
                {
                    if (roster.activeRoster.Contains(tempUnit))
                    {
                        roster.activeRoster.Remove(tempUnit);
                    }
                }
                Backpack tempBackPack = tempUnit.GetComponent<Backpack>();
                if (tempBackPack != null)
                {
                    tempBackPack.EmptyBackpack();
                    foreach (string itemName in character.Backpack)
                    {
                        Item tempItem = ItemString.StringToItem(itemName);
                        tempBackPack.Add(tempItem);
                    }
                }
                else
                {
					tempUnit.Character.Backpack = new Backpack();
                }
            }
            else
            {
                Debug.LogError("No characters in loaded file?");
                return;
            }
        }
    }
}
