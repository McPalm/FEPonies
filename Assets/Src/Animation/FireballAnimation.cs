using UnityEngine;
using System.Collections;
using System;

public class FireballAnimation : IAnimation {

	private Vector3 _startPosition;
	private Vector3 _endPosition;
	private GameObject _particle;
	private float _duration;
	private bool _active = false;
	private bool _exploding = false;
	private Tile _target;
	private Action<Tile> actions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_active && StateManager.Instance.State == GameState.unitAttack){
		_duration += Time.deltaTime*4f;
			if(_duration < 1f){
				// tween
				_particle.transform.position = _endPosition*_duration + _startPosition * (1f-_duration);
			}else if(_duration < 2f &! _exploding){
				// Unit.ApplyEffect();
				actions(_target);
				//explode
				_exploding = true;
				UnityEngine.Object.Destroy(_particle, 1f);
				_particle.GetComponent<ParticleSystem>().Stop();
				_particle = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Explosion")) as GameObject; // add contant
				_particle.transform.position = _endPosition;
				// explosion sfx!
				SFXPlayer.Instance.Explosion();
			}else if(_duration < 2f){
				// keep exploding

			}else if(_duration < 4f){
				// stop exploding
				_particle.GetComponent<ParticleSystem>().Stop();
			}else{
				UnityEngine.Object.Destroy(_particle);
				StateManager.Instance.DebugPop();
				DirtyUpdate.Instance.UnregisterUpdate(Update);
				_active = false;
				_exploding = false;
			}
		}
	}

	public void Animate (Unit source, Tile target, Action<Tile> action, bool hit=true)
	{
		DirtyUpdate.Instance.RegisterUpdate(Update);
		if(!_active){
			actions = action;
			_target = target;
			// enter animation state
			_active = true;
			StateManager.Instance.DebugPush(GameState.unitAttack);
			// spawn trail particle
			_particle = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("FireTrail")) as GameObject; // add contant

			// set up tweening
			_duration = 0f;
			_startPosition = source.transform.position + new Vector3(0f, 0.4f, 0f);
			_endPosition = target.transform.position + new Vector3(0f, 0.4f, 0f);
			_particle.transform.position = _startPosition;
		}else{
			action(target);
		}

	}
	void OnDestroy(){
		UnityEngine.Object.Destroy(_particle);
	}

	public void Cancel()
	{
		// Should not cause trouble
	}
}
