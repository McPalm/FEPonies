using UnityEngine;
using System.Collections;
using System;

public class Tackle : IAnimation {

	private float _tweenPosition;
	private bool _moving = false;
	private Vector3 _endPosition;
	private Vector3 _startPosition;
	private float _tweenSpeed;
	private bool _hit;

	private Unit source;
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Tackle Update!");
		if(_moving && StateManager.Instance.State == GameState.unitAttack){
			_tweenPosition += _tweenSpeed*Time.deltaTime;
			if(_tweenPosition >= 1f){
				StateManager.Instance.DebugPop();
				source.transform.position = _endPosition;
				_moving = false;
				DirtyUpdate.Instance.UnregisterUpdate(Update);
			}else{
				source.transform.position = _endPosition*_tweenPosition + _startPosition*(1f-_tweenPosition);
			}
		}
	}

	public void Animate (Unit source, Tile target, Action<Tile> actn, bool hit=true)
	{
		DirtyUpdate.Instance.RegisterUpdate(Update);
		_hit=hit;
		if(!_moving){
			this.source = source;
			_tweenPosition = 0f;
			StateManager.Instance.DebugPush(GameState.unitAttack);
			_endPosition = source.transform.position;
			_startPosition = (target.transform.position *2 + _endPosition) / 3;
			_tweenSpeed = 5f;
			_moving = true;
			if (_hit==true)
			{
				SFXPlayer.Instance.AttackSound();
			}
			else
			{
				SFXPlayer.Instance.Whoosh();
			}
		}
		actn(target);
	}
}
