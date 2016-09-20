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
        float maxJudge = 0;        
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
                        float temp = judgeAttackMove(unit, pa.Unit, tile);
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
        float hitChance = user.GetStatsAt(userPos, target).Hit - target.GetStatsAt(target.Tile, user, userPos).Dodge;
        if (user.AttackInfo.effect.Apply(target.Tile, user, true, userPos)>=target.CurrentHP||hitChance>0.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected float judgeAttackMove(Unit user, Unit target, Tile moveTo)
    {
        float actionValue;
        int damage = user.AttackInfo.effect.Apply(target.Tile, user, true, moveTo);
        float hitChance = user.GetStatsAt(moveTo, target).Hit - target.GetStatsAt(target.Tile, user, moveTo).Dodge;
        float critChance = user.GetStatsAt(moveTo, target).crit - target.GetStatsAt(target.Tile, user, moveTo).critDodge;

        if (hitChance > 1f) hitChance = 1f;
        else if (hitChance < 0f) hitChance = 0f;
        if (critChance > 1f) critChance = 1f;
        else if (critChance < 0f) critChance = 0f;
        if (damage > target.GetStatsAt(target.Tile).maxHP) damage = 20;
        damage = (damage / target.GetStatsAt(target.Tile).maxHP)*20;
        actionValue = damage * (hitChance+critChance);

        if (target.retaliationsLeft > 0&&target.AttackInfo.reach.GetTiles(target.Tile).Contains(moveTo))//If it will retaliate
        {
            damage = target.AttackInfo.effect.Apply(user.Tile, target, true);
            hitChance = target.GetStatsAt(moveTo, target).Hit - user.GetStatsAt(target.Tile, user, moveTo).Dodge;
            critChance = target.GetStatsAt(moveTo, target).crit - user.GetStatsAt(target.Tile, user, moveTo).critDodge;
            if (hitChance > 1f) hitChance = 1f;
            else if (hitChance < 0f) hitChance = 0f;
            if (critChance > 1f) critChance = 1f;
            else if (critChance < 0f) critChance = 0f;
            if (damage > target.GetStatsAt(target.Tile).maxHP) damage = 20;
            damage = (damage / user.GetStatsAt(moveTo).maxHP)*20;
            actionValue -= (damage * (hitChance + critChance))/2;
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

    protected float judgeMove(Unit user, Tile moveTo, Unit target = null)
    {
        float moveValue = 0;
        if (target!=null&&target.AttackInfo.reach.GetTiles(target.Tile).Contains(moveTo))
        {
            moveValue -= 10;
        }
        Stats temp = user.GetStatsAt(moveTo);
        moveValue += (temp.critDodge*20) + temp.defense + (temp.Dodge*20) + (temp.Hit*20) + temp.resistance;
        if (user.Tile == moveTo)
        {
            moveValue += 1;
        }
        return moveValue;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
