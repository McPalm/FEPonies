using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Contains all starting values for all characters.
/// </summary>
public class CharacterDB : MonoBehaviour {

    [SerializeField]
    private List<CharacterDBContainer> starterCharacters;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
            if(character.character.Name==name)
            {
                Character tempChar = new Character(character.character);
                tempChar.Backpack = character.backpack.GetBackpack();
                tempChar.Skilltree = SkillTreeDB.GetSkillTreeClone(character.skillTree);
                return tempChar;
            }
        }
        return null;
    }

    public List<Character> GetRoster()
    {
        List<Character> starterRoster = new List<Character>();
        foreach (CharacterDBContainer character in starterCharacters)
        {
            Character tempChar = new Character(character.character);
            tempChar.Backpack = character.backpack.GetBackpack();
            tempChar.Skilltree = SkillTreeDB.GetSkillTreeClone(character.skillTree);
            starterRoster.Add(tempChar);
        }
        return starterRoster;
    }

    [Serializable]
    private class CharacterDBContainer
    {
#pragma warning disable 0649
        public Character character;
        public string skillTree;
        public GearPackage backpack;
#pragma warning restore 0649
    }
}