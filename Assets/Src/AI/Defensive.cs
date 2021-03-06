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
					float temp = a.judgeAbility(unit, o, out tempTarget);
                    temp += judgeMove(unit, o);
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
 
		// float hitChance = user.GetStatsAt(userPos, target).HitVersus(target.GetStatsAt(target.Tile, user, userPos));
		DamageData dd = user.TestAttack(target, userPos);
		if (dd.FinalDamage >= target.CurrentHP && dd.hitChance>0.5f)
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

		// gather data
		Stats attackStats = user.GetStatsAt(moveTo, target);
		Stats defenceStats = target.GetStatsAt(moveTo, user);
		DamageData dd = user.TestAttack(target, moveTo);
        float damage = dd.FinalDamage;

		// calculate average damage as a function of targets max health
        if (damage > defenceStats.maxHP) damage = 20;
        damage = (damage / defenceStats.maxHP)*20;
        actionValue = damage * (dd.hitChance + dd.critChance);

        if (target.RetaliationsLeft > 0&&target.AttackInfo.Reach.GetTiles(target.Tile).Contains(moveTo))//If it will retaliate
        {
			// repeat pervious calculation, but on the retaliation
			DamageData rd = target.TestAttack(user);

			damage = rd.FinalDamage;
            damage = (damage / attackStats.maxHP)*20;
            actionValue -= (damage * (rd.hitChance + rd.critChance))/2;
        }

        actionValue += judgeMove(user, moveTo, target);
		return actionValue;
    }

    protected float judgeMove(Unit user, Tile moveTo, Unit target = null)
    {
        float moveValue = 0;
        if (target!=null&&target.AttackInfo.Reach.GetTiles(target.Tile).Contains(moveTo))
        {
            moveValue -= 10;
        }
        Stats temp = user.GetStatsAt(moveTo);
        moveValue += (temp.critDodgeBonus*20) + temp.defense + (temp.Dodge*20) + (temp.Hit*20) + temp.resistance;
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
