using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Contains all starting values for all characters.
/// </summary>
public class CharacterDB : MonoBehaviour {

    [SerializeField]
    private List<Character> starterCharacters;

    public List<Character> StarterCharacters
    {
        get
        {
            Debug.Log("gettingcharacters");
            return new List<Character>(starterCharacters);
        }
    }
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
        foreach(Character ch in starterCharacters)
        {
            if(ch.Name==name)
            {
                Character temp = new Character(ch);
                return temp;
            }
        }
        return null;
    }
}
