using UnityEngine;
using System.Collections.Generic;

public class Backflip : Passive {


	private bool tween;
	private Vector3 tweento;
	private Vector3 tweenfrom;
	private float tweenduration = 0f;

	public override string Name {
		get {
			return "Backflip";
		}
	}

	void Update(){
		if(tween){ // && StateManager.Instance.State == GameState.unitAttack
			tweenduration += Time.deltaTime*6f;

			if(tweenduration > 1){
				transform.position = tweento;
				tween = false;
				//StateManager.Instance.DebugPop();
			}else transform.position = tweenfrom * (1-tweenduration) + tweento * tweenduration;
		}
	}

	void OnDodgeAttack(Unit attacker){
//		Debug.Log("I dodged!");

		if(TileGrid.GetDelta(this, attacker) == 1){
//			Debug.Log("And were in melee!");
			Unit user = GetComponent<Unit>();
			Tile moveTo = null;
			if(user.Tile.North && user.Tile.North.Unit == attacker) moveTo = user.Tile.South;
			else if(user.Tile.South && user.Tile.South.Unit == attacker) moveTo = user.Tile.North;
			else if(user.Tile.West && user.Tile.West.Unit == attacker) moveTo = user.Tile.East;
			else if(user.Tile.East && user.Tile.East.Unit == attacker) moveTo = user.Tile.West;
			Vector3 pos = transform.position;
			if(moveTo && moveTo.Type != TileType.wall && user.MoveTo(moveTo)){
//				Debug.Log("We has dodged!");
				tweento = transform.position;
				transform.position = pos;
				tweenfrom = pos;
				tween = true;
				tweenduration = 0f;
				//StateManager.Instance.DebugPush(GameState.unitAttack);
			}
		}
	}

}
