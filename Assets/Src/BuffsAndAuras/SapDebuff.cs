using UnityEngine;
using System.Collections.Generic;

public class SapDebuff : Debuff, TurnObserver{

	DurationBuff db;

	private float _time = 1f;

	private int turnsLeft = 2;

	// Use this for initialization
	void Start () {

		UnitManager.instance.RegisterTurnObserver(this);

		Stats debuff = new Stats();
		debuff.hitBonus = -0.4f;
		debuff.dodgeBonus = -0.4f;
		debuff.movement.moveSpeed = -4;
		db = new DurationBuff(-1, debuff, GetComponent<Unit>());
		Particle.Star(transform.position + new Vector3(0.1f, 0.3f, 0f));
		Particle.Star(transform.position + new Vector3(-0.1f, 0.3f, 0f));
	}

	void Update(){
		_time -= Time.deltaTime;

		if(_time < 0f){
			Particle.Star(transform.position + new Vector3(Random.Range(-0.15f, 0.15f), 0.42f, 0f));
			_time += Random.Range(0.1f, 1f);
			if(_time > 0.3f) _time += Random.Range(0f, 5f);
		}
	}

	void OnDestroy(){
		db.Destroy();
		UnitManager.instance.unRegisterTurnObserver(this);
	}

	public void Notify (int turn)
	{
		if(StateManager.Instance.State == GameState.playerTurn) turnsLeft--;
		if(turnsLeft == 0) Destroy(this);
	}
}
