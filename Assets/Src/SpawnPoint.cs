using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	/// <summary>
	/// Name of the character that is supposed to spawn here (from the UnitRoster)
	/// Leave empty if the player can chose
	/// </summary>
	public string unitName;

    void Start()
    {
        UnitRoster.Instance.SpawnUnit(unitName, transform);
    }
}
