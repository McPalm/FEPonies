using UnityEngine;
using System.Collections.Generic;

public class EnterTurnEvent : MonoBehaviour, TurnObserver {

	public int turn;
	public bool continous = false;
	public int interval = 1;

	public List<EventTarget> events;

	public GameState phase = GameState.playerTurn;
	// Update is called once per frame

	void Awake(){
		UnitManager.Instance.RegisterTurnObserver(this);
		if(interval < 1) interval = 1;
	}

	public void Notify(int turn){
		if(this.turn == turn && StateManager.Instance.Turn == phase){
			NotifySubscribers();
			if(continous){
				this.turn += interval;
			}else {
				UnitManager.Instance.unRegisterTurnObserver(this);
			}
		}
	}

	private void NotifySubscribers() {
		foreach(EventTarget e in events){
			try{
				e.Notice();
			}catch(System.Exception err){
				Debug.LogException(err);
			}
		}
	}
}
