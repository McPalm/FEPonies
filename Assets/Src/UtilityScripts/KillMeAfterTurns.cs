using UnityEngine;
using System.Collections.Generic;

public class KillMeAfterTurns : MonoBehaviour, TurnObserver {

	public int duration = 2;

	private int _killWhen;

	// Use this for initialization
	void Start () {
		_killWhen = UnitManager.Instance.currTurn + duration;
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	public void Notify (int turn){
		if(_killWhen <= turn) Kill();
	}

	void Kill(){
		Destroy(gameObject);

	}

	void OnDestroy(){
		UnitManager.instance.unRegisterTurnObserver(this);
	}
}
