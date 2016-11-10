using UnityEngine;
using System.Collections;
using System;

public class Lightning : MonoBehaviour, IAnimation{

	private float _time = 0f;
	private bool _active = false;
	private GameObject _sprite;
	private bool _hit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_active && StateManager.Instance.State == GameState.unitAttack){
			_time += Time.deltaTime;
			if(_time > 0.3f){
				// end it
				_active = false;

				StateManager.Instance.DebugPop();
				Destroy(_sprite);
			}
		}
	}

	public void Animate (Unit source, Tile target, Action<Tile> t, bool hit=true)
	{
		_hit=hit;
		if(!_active){
			StateManager.Instance.DebugPush(GameState.unitAttack);
			_active = true;
			try{
				_sprite = Instantiate(Resources.Load<GameObject>("Lightning")) as GameObject;
				_sprite.transform.position = target.transform.position;
			}catch(System.Exception e){
				Debug.LogException(e);
			}
			_time = 0f;
			if (_hit==true)
			{
				SFXPlayer.Instance.Lightning();
			}
			else
			{
				SFXPlayer.Instance.Whoosh();
			}
		}
		// Unit.ApplyEffect();
		t(target);
	}

	void OnDestroy(){
		Destroy(_sprite);
	}

	public void Cancel()
	{
		// blorp
	}
}
