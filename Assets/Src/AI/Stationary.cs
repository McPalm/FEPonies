using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stationary : MonoBehaviour, IAIBehaviour {

	public Action GetAction(Unit unit)
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
				else if (unit.CanMurder (o.Unit)) {
					target = o.Unit;
					break;
				} else {
					int temp = unit.AttackInfo.effect.judgeAttack (unit, o.Unit);
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
