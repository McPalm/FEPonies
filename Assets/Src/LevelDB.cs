using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelDB
{
    static private LevelDB instance=new LevelDB();

    public static LevelDB Instance
    {
        get
        {
            return instance;
        }
    }

    private string[] levels={
        "mainMenu",
        "RoadAmbush",
        "BarbarianDefence",
		"CastleBreakIn"
        };

    private string[] battlelevels =
    {
        "RoadAmbush",
        "BarbarianDefence",
		"CastleBreakIn"
	};

    public string GetMainMenu()
    {
        return levels[0];
    }

    public string GetFirstLevel()
    {
        return levels[1];
    }

    public List<string> GetAllLevels()
    {
        List<string> temp = new List<string>(levels);
        return temp;
    }

    /// <summary>
    /// Gets the next level. Level order is in levels
    /// </summary>
    /// <param name="curr">name of the current level</param>
    /// <returns></returns>
    public string GetNextLevel(string curr)
    {
        for(int n=0;n<levels.Length;n++)
        {
            if (n==levels.Length-1)
            {
                Debug.Log("Last Level");
                return ("-1");
            }
            if (levels[n]==curr)
            {
                return levels[n + 1];
            }
        }
        Debug.Log("Level not Found");
        return ("-2");
    }

    public bool isBattleLevel(string levelName)
    {
        foreach(string name in battlelevels)
        {
            if(name==levelName)
            {
                return true;
            }
        }
        return false;
    }
}
