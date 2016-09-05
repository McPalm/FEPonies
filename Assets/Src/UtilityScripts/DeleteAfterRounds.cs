using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Delete after rounds.
/// Attach this script to a gameobject that should be removed after a number of rounds
/// </summary>
public class DeleteAfterRounds : MonoBehaviour, TurnObserver {

	[Range(0, 20)]
	public int duration = 2;

	// Use this for initialization
	void Start () {
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	public void Notify (int turn)
	{
		if(StateManager.Instance.Turn == GameState.playerTurn) duration--;
		if(duration < 1) Destroy(gameObject);
	}

	void OnDestroy(){
		UnitManager.instance.unRegisterTurnObserver(this);
	}
}
