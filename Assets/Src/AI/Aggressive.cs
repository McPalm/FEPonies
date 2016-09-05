using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aggressive : Defensive {
	private float minDistance=999f;
	public override Action GetAction(Unit unit)
	{
		Action retValue=base.GetAction(unit);
		if(retValue.movement!=null)
		{
			return retValue;
		}
		HashSet<Tile> possibleMoves=unit.GetReachableTiles();
		HashSet<Unit> targets=UnitManager.Instance.GetUnitsByHostility(unit);
		float temp=0f;
		minDistance=999f;
		Unit target=null;
		foreach(Unit o in targets)
		{
			if(o.invisible)
			{
				continue;
			}
			float
				dx=unit.transform.position.x-o.transform.position.x,
				dy=unit.transform.position.y-o.transform.position.y;
			temp=dx*dx+dy*dy;
			if( temp<minDistance)
			{
				minDistance=temp;
				target=o;
			}
		}

		if(target == null){ // if we find no target to chase down
			return retValue;
		}
		minDistance=999f;
		temp=0f;
		foreach (Tile o in possibleMoves)
		{
			float
				dx =target.transform.position.x-o.transform.position.x,
				dy =target.transform.position.y-o.transform.position.y;

			temp=dx*dx+dy*dy;
			if (temp<minDistance)
			{
				minDistance=temp;
				retValue.movement=o;
			}
		}
		return retValue;
	}
}