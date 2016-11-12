using UnityEngine;
using System.Collections;

public class SpawnUnit : EventTarget {

	public string rosterCharacter;
	public string databaseCharacter;
	int level = -1;
	public int team = 1;
	public bool changeAI = false;
	public AITypes ai;

	public override void Notice ()
	{
		Character c = null;
		if(rosterCharacter != "")
		{
			c = UnitRoster.Instance.GetCharacter(rosterCharacter);
		}
		else
		{
			c = CharacterDB.Instance.GetCharacter(databaseCharacter);
		}
		if (level > 0) c.Level = level;

		Unit iSpawned = Unit.Create(c);
		iSpawned.name = (rosterCharacter != null) ? databaseCharacter : rosterCharacter;

		Tile targetTile = TileGrid.Instance.GetTileAt(transform.position);
		if(!iSpawned.MoveTo(targetTile)){
			if(targetTile.North == null || !iSpawned.MoveTo(targetTile.North)){
				if(targetTile.South == null || !iSpawned.MoveTo(targetTile.South)){
					if(targetTile.East == null || !iSpawned.MoveTo(targetTile.East)){
						if(targetTile.West == null || !iSpawned.MoveTo(targetTile.West)){
							Debug.LogWarning("Unable to Spawn unit!"); // TODO make sure this cannot happen!
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
