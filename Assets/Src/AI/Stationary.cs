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
				else if (canMurder (unit, o.Unit, unit.Tile)) {
					target = o.Unit;
					break;
				} else {
					int temp = judgeAttackMove (unit, o.Unit, unit.Tile);///TODO check so the tile is correct
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

    protected bool canMurder(Unit user, Unit target, Tile userPos)
    {
        if (user.AttackInfo.effect.Apply(target.Tile, user, true, userPos) >= target.CurrentHP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected int judgeAttackMove(Unit user, Unit target, Tile moveTo)///TODO Make sure this is correct
    {
        int actionValue = user.AttackInfo.effect.Apply(target.Tile, user, true);

        if (target.RetaliationsLeft == 0)
        {
            actionValue += 20;
        }
        else if ((user.AttackInfo.reach) is Melee)
        {
            if (target.AttackInfo.reach is Ranged || target.AttackInfo.reach is IncreasedRange)
            {
                actionValue += 20;
            }
        }
        else if (user.AttackInfo.reach is Ranged)
        {
            if (target.AttackInfo.reach is Melee)
            {
                actionValue += 20;
            }
        }
        else if (user.AttackInfo.reach is RangeAndMelee)
        {
            if (target.AttackInfo.reach is Melee || target.AttackInfo.reach is Ranged || target.AttackInfo.reach is IncreasedRange)
            {
                actionValue += 20;
            }
        }
        else if (user.AttackInfo.reach is IncreasedRange)
        {
            if (target.AttackInfo.reach is Melee || target.AttackInfo.reach is Ranged || target.AttackInfo.reach is RangeAndMelee)
            {
                actionValue += 20;
            }
        }
        return actionValue;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
