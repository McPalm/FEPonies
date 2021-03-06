﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class UnitRoster : MonoBehaviour, IEnumerable<Character>
{
	private Dictionary<string, Character> roster;
    public List<Character> activeRoster;
    [SerializeField]
    private CharacterDB StarterDatabase;
    public Train train;

    public Dictionary<string, Character> Roster
    {
        get
        {
            return roster;
        }
        set
        {
            roster = value;
        }
    }

    //Singleton stuff
    static private UnitRoster instance;

    static public UnitRoster Instance
    {
        get
        {
//            if (instance == null)
//            {
//               new GameObject("UnitRoster").AddComponent<UnitRoster>();
//            }
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
        roster = StarterDatabase.GetDictionary(0);

		activeRoster = new List<Character>();
		foreach(KeyValuePair<string, Character> c in roster)
		{
			activeRoster.Add(c.Value);
		}
        train = new Train();
    }
    //End of Singleton stuff


    public IEnumerator<Character> GetEnumerator()
	{
		return activeRoster.GetEnumerator();
	}

	/// <summary>
	/// Gets a character by name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public Character GetCharacter(string name)
	{
		if (roster.ContainsKey(name)) return roster[name];
        Debug.LogWarning(name + " not found in Roster");
        return null;
	}

    public Character getBaseCharacter(string name)
    {
        return StarterDatabase.GetCharacter(name);
    }

	IEnumerator IEnumerable.GetEnumerator()
	{
		return activeRoster.GetEnumerator();
	}

    /// <summary>
    /// Spawns the named unit in the indicated position
    /// </summary>
    /// <param name="name">Name of the unit</param>
    /// <param name="position">Transform of the unit</param>
    public void SpawnUnit(string name, Transform position)
    {
		/*
        Unit spawnUnit = GetUnit(name);
        GameObject go=Instantiate<Unit>(spawnUnit).gameObject;
        go.transform.position = position.position;
        go.name = name;
		*/
    }
}
