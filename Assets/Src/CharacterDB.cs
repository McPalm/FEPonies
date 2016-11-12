using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Contains all starting values for all characters.
/// </summary>
public class CharacterDB : MonoBehaviour {

	static CharacterDB _instance;

	void Awake()
	{
		_instance = this;
	}

    [SerializeField]
    private List<CharacterDBContainer> starterCharacters;

	public static CharacterDB Instance
	{
		get
		{
			if (_instance == null) _instance = Resources.Load<CharacterDB>("CharacterDB");
			return _instance;
		}
	}

	/// <summary>
	/// Gets a character by name from the starter character list.
	/// </summary>
	/// <param name="name">name of the character to search for</param>
	/// <returns>a copy of the character or null if not found</returns>
	public Character GetCharacter(string name)
    {
        foreach(CharacterDBContainer character in starterCharacters)
        {
            if(character.name == name)
            {
                return BuildCharacter(character);
            }
        }
		Debug.LogError("Unable to retrieve " + name + " from the Character Database.");
        return null;
    }

    public List<Character> GetRoster()
    {
        List<Character> starterRoster = new List<Character>();
        foreach (CharacterDBContainer character in starterCharacters)
        {
            starterRoster.Add(BuildCharacter(character));
        }
        return starterRoster;
    }

	Character BuildCharacter(CharacterDBContainer cc)
	{
		Character retVal = new Character(cc.character);
		retVal.Backpack = cc.backpack.GetBackpack();
		retVal.Skilltree = SkillTreeDB.GetSkillTreeClone(cc.skillTree);
        retVal.Sprite = cc.sprite;
        retVal.MugShot = cc.mugShot;
		retVal.Initialize(null);

		return retVal;
	}

	/// <summary>
	/// Gets a library with the unit, keyed with the units dev name.
	/// </summary>
	/// <param name="team">0 is all, 1 is PCs, 2 is enemies</param>
	/// <returns></returns>
	public Dictionary<string, Character> GetDictionary(int team)
	{
		Dictionary<string, Character> ret = new Dictionary<string, Character>();
		foreach (CharacterDBContainer dbz in starterCharacters)
		{
			if(team == 0 || (team == 1 && dbz.PC) || (team == 2 &! dbz.PC))
			{
				ret.Add(dbz.name, BuildCharacter(dbz));
			}
		}
		return ret;
	}

    [Serializable]
    private class CharacterDBContainer
    {
#pragma warning disable 0649
		public string name;
        public Character character;
        public string skillTree;
        public GearPackage backpack;
        public Sprite mugShot;
        public Sprite sprite;
		public bool PC = true;
#pragma warning restore 0649
    }
}