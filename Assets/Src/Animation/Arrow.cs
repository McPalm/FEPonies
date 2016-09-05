using UnityEngine;
using System.Collections;
using System;

public class Arrow : MonoBehaviour, IAnimation {

	private GameObject _arrow;
	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private float _tweenSpeed;
	private bool _active = false;
	private float _tweenProgression = 0f;
	private bool step1 = true;
	private Tile _target;
	private bool _hit;

	Action<Tile> _callback;

	// Update is called once per frame
	void Update () {
		// move towards target
		if(_active && StateManager.Instance.State == GameState.unitAttack){
			_tweenProgression += _tweenSpeed*Time.deltaTime;
			if(_tweenProgression > 1f && step1){
				// if we reach the goal, pop animaiton state, apply effect, destroy arrow
				//Unit.ApplyEffect();
				StateManager.Instance.DebugPop();
				if(_hit==true)
				{
					SFXPlayer.Instance.AttackSound();
				}
				else
				{
					SFXPlayer.Instance.Whoosh();
				}
				_callback(_target);
				step1 = false;
			//}else if(_tweenProgression > 1f){

				_active = false;
				step1 = true;
				Destroy(_arrow);
			}else{
				_arrow.transform.position = _endPosition*_tweenProgression + _startPosition * (1f-_tweenProgression);
			}
		}
	}

	public void Animate (Unit source, Tile target, Action<Tile> tile, bool hit=true)
	{
		_hit=hit;
		if(!_active){
			_callback = tile;
			_target = target;
			// enter animation state
			StateManager.Instance.DebugPush(GameState.unitAttack);
			_active = true;
			// create new arrow
			_arrow = Instantiate(Resources.Load("arrow")) as GameObject;

			// set it on tweening towards target
			_startPosition = source.transform.position + new Vector3(0f, 0.25f, 0f);
			_endPosition = target.transform.position + new Vector3(0f, 0.25f, 0f);
			_tweenSpeed = 3f; // TODO
			_tweenProgression = 0f;
			_arrow.transform.position = _startPosition;

			// rotate it
			float dx = _startPosition.x-_endPosition.x;
			float dy = _startPosition.y-_endPosition.y;
			float rot;
			if(dx != 0f){
				rot = Mathf.Atan(dy/dx) * 57.2957795f + 90f;
				if(dx < 0){
					rot += 180f;
				}
			}else if(dy < 0){
				rot = 0f;
			}else{
				rot = 180f;
			}
			_arrow.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
		}else{
			tile(target);
				Debug.LogWarning("Tried to do arrow attack twice!");
		}
	}

	void OnDestroy(){
		Destroy(_arrow);
	}
}
