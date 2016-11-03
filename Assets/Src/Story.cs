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
        foreach(Character u in roster.Roster)
        {
            SaveCharacter temp;
            temp = new SaveCharacter();
            temp.character = new Character(u);
            GearPackage tempBackPack = new GearPackage(u.Backpack);
            temp.backpack = tempBackPack;
            if(roster.activeRoster.Contains(u))
            {
                temp.isActive = true;
            }
            else
            {
                temp.isActive = false;
            }
            temp.skills = new SkillTree( u.Skilltree);
            saveData.Roster.Add(temp);
        }
        GearPackage train = new GearPackage(roster.train);
        saveData.train = train;
        saveData.saveTime = DateTime.Now;
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
        roster.activeRoster.Clear();
        List<Character> savedRoster = new List<Character>();
        foreach(SaveCharacter character in saveData.Roster)
        {
            Character tempUnit = character.character;
            Character baseChar = roster.getBaseCharacter(character.character.Name);
            if (tempUnit != null)
            {
                tempUnit = character.character;
                if (character.isActive)
                {
                    roster.activeRoster.Add(tempUnit);
                }
                Backpack tempBackPack = character.backpack.GetBackpack();
                tempUnit.Backpack = tempBackPack;
                tempUnit.Skilltree = new SkillTree(character.skills);
                tempUnit.Sprite = baseChar.Sprite;
                tempUnit.MugShot = baseChar.MugShot;
            }
            else
            {
                Debug.LogError("No characters in loaded file?");
                return;
            }
            savedRoster.Add(tempUnit);
        }
        roster.train = saveData.train.GetTrain();

    }
}
