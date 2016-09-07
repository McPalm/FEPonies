using UnityEngine;
using System.Collections;

public class SpawnUnit : EventTarget {

	public Unit unit;
	public int team = 1;
	public bool changeAI = false;
	public AITypes ai;

	public override void Notice ()
	{
		GameObject go = Instantiate(unit.gameObject) as GameObject;
		Unit iSpawned = go.GetComponent<Unit>();
		iSpawned.name = unit.name;
		Tile targetTile = TileGrid.Instance.GetTileAt(transform.position);
		if(!iSpawned.MoveTo(targetTile)){
			if(targetTile.North == null || !iSpawned.MoveTo(targetTile.North)){
				if(targetTile.South == null || !iSpawned.MoveTo(targetTile.South)){
					if(targetTile.East == null || !iSpawned.MoveTo(targetTile.East)){
						if(targetTile.West == null || !iSpawned.MoveTo(targetTile.West)){
							Debug.LogError("Unable to Spawn unit! This is like a fatal error"); // TODO make sure this cannot happen!
						}	
					}	
				}	
			}
		}

		iSpawned.team = team;
		if(changeAI){
			try{
                AIUtility.AITypeChanger(iSpawned, ai);
                    //UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(iSpawned.gameObject, "Assets/Event/EventTargets/SpawnUnit.cs (32,5)", ai.ToString());
			}catch(UnityException e){
				Debug.LogException(e);
			}
		}
	}
}
