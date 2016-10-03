using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UnitRoster : MonoBehaviour, IEnumerable<Unit>
{
	List<Unit> roster;

	public IEnumerator<Unit> GetEnumerator()
	{
		return roster.GetEnumerator();
	}

	/// <summary>
	/// Gets a unit by name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public Unit GetUnit(string name)
	{
		foreach(Unit u in roster)
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
		return roster.GetEnumerator();
	}
}
