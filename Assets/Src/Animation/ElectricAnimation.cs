using UnityEngine;
using System.Collections.Generic;
using System;

public class ElectricAnimation : MonoBehaviour, IAnimation {

	public GameObject Projectile;
	public GameObject HitEffects;
	[Range(0.1f, 10f)]
	public float TweenSpeed = 3f;
	[Range(0.3f, 2f)]
	public float ShockDuration = 1f;
	[Range(0.5f, 20f)]
	public float HitEffectPerSeconds = 6f;
	[Range(1, 40)]
	public int MaxHitEffects = 5;


	private GameObject _myProjectile;
	private GameObject[] _myHitEffects;
	private int _nextHitEffect = 0;

	private System.Action<Tile> _hitCall;
	private Tile _target;
	private bool _active;
	private bool _hit;

	private Vector2 _startPosition;
	private Vector2 _endPosition;

	private float _tweenProgression = 0f;
	private int _step = 0;
	private float _magnitude;
	private float _shockTimer = 0f;
	private int _hitEffectsSpawned = 0;


	// Use this for initialization
	void Start () {
		_myProjectile = Instantiate(Projectile) as GameObject;
		_myProjectile.SetActive(false);

		_myHitEffects = new GameObject[MaxHitEffects];

		for(int i = 0; i < MaxHitEffects; i++){
			_myHitEffects[i] = Instantiate(HitEffects) as GameObject;
			_myHitEffects[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(_active && StateManager.Instance.State == GameState.unitAttack){
			_tweenProgression += TweenSpeed*Time.deltaTime/_magnitude;
			if(_tweenProgression > 1f && _step == 1){
				// if we reach the goal, pop animaiton state, apply effect, destroy arrow
				//Unit.ApplyEffect();

				if(_hit==true)
				{
					_myProjectile.SetActive(false);

					_step = 3;
					_shockTimer = 0f;
					_hitEffectsSpawned = 0;
					SFXPlayer.Instance.Shock();
				}
				else
				{
					_hitCall(_target);
					SFXPlayer.Instance.Whoosh();
					_step = 2;
				}


				//}else if(_tweenProgression > 1f){
				

			}else if(_tweenProgression > 2f && _step == 2){ // miss
				_myProjectile.SetActive(false);
				_active = false;
				StateManager.Instance.DebugPop();
			}else if(_step == 3){ // hit
				_shockTimer += Time.deltaTime;
				if(_shockTimer > ShockDuration){ // end
					foreach(GameObject go in _myHitEffects) go.SetActive(false);
					StateManager.Instance.DebugPop();
					SFXPlayer.Instance.AttackSound();
					_hitCall(_target);
					_active = false;

				}else{
					if(_shockTimer*HitEffectPerSeconds > _hitEffectsSpawned) SpawnHitEffect();
				}
			}else{
				_myProjectile.transform.position = _endPosition*_tweenProgression + _startPosition * (1f-_tweenProgression);
			}
		}
	}

	public void Animate (Unit source, Tile target, System.Action<Tile> tile, bool hit=true)
	{
		_hit=hit;
		if(!_active){
			SFXPlayer.Instance.Lightning();
			_hitCall = tile;
			_target = target;
			// enter animation state
			StateManager.Instance.DebugPush(GameState.unitAttack);
			_active = true;
			
			// set it on tweening towards target
			_startPosition = source.transform.position + new Vector3(0f, 0.25f);
			if(hit) _endPosition = target.transform.position + new Vector3(0f, 0.25f);
			else _endPosition = target.transform.position + new Vector3(0f +  UnityEngine.Random.Range(-0.5f, 0.5f), 0.25f  + UnityEngine.Random.Range(-0.5f, 0.5f));
			_magnitude = (_startPosition-_endPosition).magnitude;
			_magnitude = Mathf.Max(_magnitude, 0.3f);
			_tweenProgression = 0f;
			_myProjectile.transform.position = _startPosition;
			_myProjectile.SetActive(true);
			_step = 1;

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
			_myProjectile.transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);
		}else{
			tile(target);
			Debug.LogWarning("Tried to do arrow attack twice!");
		}
	}

	public void SpawnHitEffect(){
		_nextHitEffect++;
		if(_nextHitEffect >= _myHitEffects.Length) _nextHitEffect = 0;

		_myHitEffects[_nextHitEffect].SetActive(true);
		_myHitEffects[_nextHitEffect].transform.position = _endPosition + new Vector2(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.1f, 0.5f));
		_myHitEffects[_nextHitEffect].transform.localRotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.forward);

		_hitEffectsSpawned++;
	}

	public void Cancel()
	{
		// should not need to do anything.
	}
}
