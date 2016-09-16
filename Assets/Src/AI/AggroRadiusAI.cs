//------------------------------------------------------------------------------
// <auto-generated>
//     Denna kod har genererats av ett verktyg.
//     Körtidsversion:4.0.30319.34014
//
//     Ändringar i denna fil kan orsaka fel och kommer att förloras om
//     koden återgenereras.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

public class AggroRadiusAI : Aggressive
{
	private bool isAggro=false;
	public float aggroRadius=0;

	public override Action GetAction(Unit unit)
	{
		///Check if someone have aggroed
		if (!isAggro)
		{
			if(unit.damageTaken>0)
			{
				isAggro=true;
			}
			if(!isAggro)
			{
				HashSet<Unit> targets=UnitManager.Instance.GetUnitsByHostility(unit);
				float temp=0f;
				foreach(Unit o in targets)
				{
					if(o.invisible)
					{
						continue;
					}
					float
						dx=unit.transform.position.x-o.transform.position.x,
						dy=unit.transform.position.y-o.transform.position.y;
					temp=dx+dy;
					if(temp<=aggroRadius)
					{
						isAggro=true;
						break;
					}
				}
			}
		}

		//Do two different things depending on if it is aggroed
		if(isAggro)
		{
			return base.GetAction(unit);
		}
		else
		{
			Unit target=null;
			Action retValue=new Action(unit.Tile);
			HashSet<Tile> possibleAttacks = new HashSet<Tile>(unit.AttackInfo.GetAttackTiles(unit));
			possibleAttacks.Remove (unit.Tile);
			if (possibleAttacks.Count > 0) {
				int max = 0;
				foreach (Tile o in possibleAttacks) {
					if(o.Unit.invisible)
					{
						continue;
					}
					else if (canMurder(unit, o.Unit, unit.Tile)) {
						target = o.Unit;
						break;
					} else {
						int temp = judgeAttackMove (unit, o.Unit, unit.Tile);///TODO check if the tile is correct
						if (temp > max) {
							target = o.Unit;
							max = temp;
						}
					}
				}
				if (target != null) {
					retValue.attack=target.Tile;
				}
			} 
			return retValue;
		}
	}

}