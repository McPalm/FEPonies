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
}
