using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UnitRoster : MonoBehaviour, IEnumerable<Unit>
{
	public List<Unit> roster;
    public List<Unit> activeRoster;

    public List<Unit> Roster
    {
        get
        {
            return roster;
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
    }
    //End of Singleton stuff


    public IEnumerator<Unit> GetEnumerator()
	{
		return activeRoster.GetEnumerator();
	}

	/// <summary>
	/// Gets a unit by name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public Unit GetUnit(string name)
	{
		foreach(Unit u in Roster)
        {
            if (u.name==name)
            {
                return u;
            }
        }
        Debug.Log("Not found");
        return null;
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
        Unit spawnUnit = GetUnit(name);
        GameObject go=Instantiate<Unit>(spawnUnit).gameObject;
        go.transform.position = position.position;
        go.name = name;
    }
}
