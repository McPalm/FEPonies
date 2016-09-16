using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Defensive : MonoBehaviour, IAIBehaviour {

	public virtual Action GetAction(Unit unit)
	{
		Unit target=null;
        Tile targetMove = null;
		Action retValue=new Action(unit.Tile);
		HashSet<Tile> possibleAttacks;
		HashSet<Tile> possibleMoves = unit.GetReachableTiles ();//Gets all possible moves
		possibleMoves.Add (unit.Tile);
        ///Judge Attacks and moves
        int maxJudge = 0;        
        foreach (Tile tile in possibleMoves) //Searches all moves
        {
			possibleAttacks= new HashSet<Tile>(unit.AttackInfo.GetAttackTiles (unit, tile)); //Gets all possible attacks from that position

            if (possibleAttacks.Count > 0)
            {
			    foreach (Tile pa in possibleAttacks)
                {
                    if (pa.Unit.invisible)
                    {
                        continue;
                    }
                    else if (canMurder(unit, pa.Unit, tile))
                    {
                        target = pa.Unit;
						targetMove = tile;
						maxJudge = 1000;
                        break;
                    }
                    else
                    {
                        int temp = judgeAttackMove(unit, pa.Unit, tile);
                        if (temp > maxJudge)
                        {
                            target = pa.Unit;
                            targetMove = tile;
                            maxJudge = temp;
                        }
                    }
				}
                
			}
		}
        if (target != null)
        {
            retValue.attack = target.Tile;
            retValue.movement = targetMove;
        }
        List<Ability> tempAbilities = new List<Ability>(GetComponents<Ability>());
		List<AIAbility> possibleAbilities = new List<AIAbility>();
		foreach(Ability q in tempAbilities)
		{
			if(q is AIAbility)
			{
				possibleAbilities.Add((AIAbility)q);
			}
		}
		possibleMoves = unit.GetReachableTiles ();//Gets all possible moves
		possibleMoves.Add (unit.Tile);
		Ability currAbility=null;
		Tile currAbilityTarget=null;
		Tile revMovement=null;
		Tile tempTarget=null;
		foreach( AIAbility a in possibleAbilities)
		{
			try
			{
				if (!a.willCast())
				{
					continue;
				}
				foreach (Tile o in possibleMoves) {//Gets all possible attacks
					int temp = a.judgeAbility(GetComponent<Unit>(), o, out tempTarget);
					if (temp > maxJudge) {
						currAbility=a.getAbility;
						maxJudge = temp;
						currAbilityTarget=tempTarget;
						revMovement=o;
					}
				}
			}
			catch(Exception e)
			{
				Debug.LogException(e);
			}

			///TODO text för movement
		}
		if(currAbility!=null)
		{
			retValue.attack=null;
			retValue.ability=currAbility;
			retValue.abilityTarget=currAbilityTarget;
			retValue.movement=revMovement; 

		}
		return retValue;
	}

    protected bool canMurder(Unit user, Unit target, Tile userPos)
    {
        if (user.AttackInfo.effect.Apply(target.Tile, user, true, userPos)>=target.CurrentHP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected int judgeAttackMove(Unit user, Unit target, Tile moveTo)
    {
        int actionValue = user.AttackInfo.effect.Apply(target.Tile, user, true, moveTo);

        if (target.retaliationsLeft == 0)
        {
            actionValue += 20;
        }

		/*
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
        }*/
		actionValue += judgeMove(user, moveTo, target);
		return actionValue;
    }

    protected int judgeMove(Unit user, Tile moveTo, Unit target = null)
    {
        int moveValue = 0;
        if (target!=null&&!target.AttackInfo.GetAttackTiles(target).Contains(moveTo))
        {
            moveValue += 100;
        }
        Stats temp = user.GetStatsAt(moveTo);
        moveValue += (int)(temp.critDodge*20) + temp.defense + (int)(temp.Dodge*20) + (int)(temp.Hit*20) + temp.resistance;
        return moveValue;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
