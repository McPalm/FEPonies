using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelDB
{
    private string[] levels={
        "mainMenu",
        "RoadAmbush",
        "BarbarianDefence"
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
}
