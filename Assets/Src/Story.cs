using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour {


	string checkPoint;

	/// <summary>
	/// Name of the level that is the current checkpoint
	/// </summary>
	public string Checkpoint
	{
		get { return checkPoint; }
	}

	/// <summary>
	/// Save the game and marks a checkpoint
	/// </summary>
	public void Save()
	{
		throw new System.NotImplementedException();
	}

	/// <summary>
	/// Load a game from the checlpoint being stored in this class
	/// </summary>
	public void Load()
	{
		throw new System.NotImplementedException();
	}
}
