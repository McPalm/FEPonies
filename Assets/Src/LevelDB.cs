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
    public string getMainMenu()
    {
        return levels[0];
    }

    public string getFirstLevel()
    {
        return levels[1];
    }

    public List<string> getAllLevels()
    {
        List<string> temp = new List<string>(levels);
        return temp;
    }

    public string getNextLevel(string curr)
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
