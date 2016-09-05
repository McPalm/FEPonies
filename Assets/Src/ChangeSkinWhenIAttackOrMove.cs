using UnityEngine;
using System.Collections.Generic;

public class ChangeSkinWhenIAttackOrMove : MonoBehaviour {

	public Sprite newSprite;
	public float changeDuration = 0.5f;

	private bool _changing = false;

	// called when the unit is moving
	public void StartAnimatedMove(){
		ChangeSkin();
	}

	void Update(){
		if(_changing && StateManager.Instance.State == GameState.changingSkin){
			changeDuration -= Time.deltaTime;
			if(changeDuration < 0f){
				StateManager.Instance.DebugPop();
				_changing = false;
				Destroy(this);
			}
		}
	}

	// called when the unit is starts its attack sequence
	public void StartingAttackSequence(){
		ChangeSkin();
	}

	private void ChangeSkin(){
		try{
			_changing = true;
			StateManager.Instance.DebugPush(GameState.changingSkin);
			this.GetComponent<SpriteRenderer>().sprite = newSprite;
		}catch(System.Exception e){
			Debug.LogException(e);
		}
	}
}
