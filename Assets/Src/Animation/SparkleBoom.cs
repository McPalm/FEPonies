using UnityEngine;
using System;

public class SparkleBoom : MonoBehaviour, IAnimation {

	private bool _active = false;
	private float _timer = 0f;

	Action<Tile> action;
	Tile _target;
	bool _hit;

	public void Animate (Unit source, Tile target, Action<Tile> act, bool hit=true)
	{
		_hit=hit;
		if(!_active){
			action = act;
			_target = target;
			// set state
			_active = true;
			StateManager.Instance.DebugPush(GameState.unitAttack);
			// spawn particle
			Particle.SparkleBoom(target.transform.position);

			// start timer
			_timer = 0f;

		}
	}

	// Update is called once per frame
	void Update () {
		if(_active && StateManager.Instance.State == GameState.unitAttack){
			_timer += Time.deltaTime;
			if(_timer > 1.5f){
				_active = false;
				StateManager.Instance.DebugPop();
				if (_hit==true)
				{

					SFXPlayer.Instance.AttackSound();
				}
				else
				{
					SFXPlayer.Instance.Whoosh();
				}
				action(_target);
			}

		}
	}

	public void Cancel()
	{
		// this animation is old.
	}
}
