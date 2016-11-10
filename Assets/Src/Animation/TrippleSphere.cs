using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// This animation summons three that goes out from a target while spinning..  and then goes after the enemy target.
/// </summary>
public class TrippleSphere : MonoBehaviour, IAnimation {

	public Sprite missile;
	[Range(1, 100)]
	public int MissileCount = 3;
	public float SpawnDuration = 0.5f;
	[Range(0.1f, 2f)]
	public float rangeFromUser = 0.7f;
	[Range(0f, 10f)]
	public float rotationsPerSecond = 0f;
	[Range(0.1f, 2f)]
	public float firstStageDuration = 0.8f;
	[Range(0.1f, 2f)]
	public float secondStageDuration = 0.3f;
	[Range(0.1f, 2f)]
	public float lastStageDuration = 0.3f;

	private bool _active = false;
	private System.Action<Tile> _action;
	private float _timeSinceActivated = 0f;
	private Tile _target;
	private Vector2 _start;


	private List<GameObject> _missiles = new List<GameObject>();
	private List<Missile> _activeMissiles = new List<Missile>();
	private int _toSpawn = 0;


	void Start(){
		for(int i = 0; i < MissileCount; i++){
			GameObject o = new GameObject("Missile");

			o.SetActive(false);
			SpriteRenderer s = o.AddComponent<SpriteRenderer>();
			s.sortingLayerName = "Particles";
			s.sprite = missile;

			_missiles.Add(o);
		}
	}

	void Update(){
		// move towards target
		if(_active && StateManager.Instance.State == GameState.unitAttack){
			_timeSinceActivated += Time.deltaTime;
			if(_toSpawn > 0 && MissileCount - _toSpawn < (_timeSinceActivated*MissileCount/SpawnDuration)){
				SpawnMissile();
				_toSpawn--;
			}

			for(int i = _activeMissiles.Count-1; i >= 0; i--){
				TweenMissile(_activeMissiles[i]);
			}

			if(_activeMissiles.Count == 0 && _toSpawn == 0) Finish();
		}
	}

	public void Animate (Unit source, Tile target, System.Action<Tile> tile, bool hit)
	{
		if(!_active){
			_action = tile;
			_target = target;
			// enter animation state
			_active = true;
			StateManager.Instance.DebugPush(GameState.unitAttack);

			_start = source.transform.position;
			_toSpawn = MissileCount;
			_timeSinceActivated = 0f;

		}else{
			tile(target);
		}
	}

	private void SpawnMissile(){
		Missile m = new Missile();
		m.start = _start  + new Vector2(0f, 0.25f);
		m.midPoint =  _start +  generateMidpoint() + new Vector2(0f, 0.25f);
		m.endPoint = _target.transform.position  + new Vector3(0f, 0.25f, 0f);
		m.stageDuration = 0f;
		m.totalDuration = 0f;
		m.stage = 0;
		m.gameObject = GetNextMissile();
		m.gameObject.SetActive(true);
		_activeMissiles.Add(m);
	}

	private int nextMissile = 0;
	private GameObject GetNextMissile(){
		nextMissile++;
		if(nextMissile >= _missiles.Count) nextMissile = 0;
		return _missiles[nextMissile];
	}

	private Vector2 lastMidPoint = new Vector2(1f, 1f);
	private Vector2 generateMidpoint(){
		Vector2 point = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * rangeFromUser;

		if(Mathf.Sign(point.x) == Mathf.Sign(lastMidPoint.x) && Mathf.Sign(point.y) == Mathf.Sign(lastMidPoint.y))
			point = new Vector2(-point.x, -point.y);
		lastMidPoint = point;

		return point;
	}

	private void TweenMissile(Missile m){
		m.stageDuration += Time.deltaTime;
		m.totalDuration += Time.deltaTime;

		if(m.stage == 0){
			if(m.stageDuration > firstStageDuration){
				m.stage++;
				m.stageDuration = 0f;
			}

			// tween towards first end point
			float distance = m.stageDuration / firstStageDuration;
			distance =1f - (1f - distance)*(1f - distance);

			m.gameObject.transform.position = m.start*(1f-distance) + m.midPoint*distance;

		}if(m.stage == 1){
			if(m.stageDuration > secondStageDuration){
				m.stage++;
				m.stageDuration = 0f;
			}

			// hover in air, aim towards target
			m.gameObject.transform.position = m.midPoint;

		}if(m.stage == 2){
			if(m.stageDuration > lastStageDuration){
				m.stage++;
				m.stageDuration = 0f;
			} 
			float distance = m.stageDuration/lastStageDuration;
			// tween towards target
			m.gameObject.transform.position = m.midPoint*(1f-distance) + m.endPoint*distance;
		}if(m.stage > 2){
			//I'm done
			m.gameObject.SetActive(false);
			SFXPlayer.Instance.AttackSound();
			_activeMissiles.Remove(m);
		}

		m.gameObject.transform.localRotation = Quaternion.AngleAxis(m.stageDuration*360f*rotationsPerSecond, Vector3.forward);
	}

	private void Finish(){
		StateManager.Instance.DebugPop();
		_action(_target);
		_active = false;
	}

	public void Cancel()
	{
		// wow this is a big class
	}

	private class Missile{
		// contains information regarding one of the missiles that are tweened!
		public Vector2 start;
		public Vector2 midPoint;
		public Vector2 endPoint;
		public float stageDuration;
		public float totalDuration;
		public int stage = 0;
		public GameObject gameObject;
	}


}
