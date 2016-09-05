using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Defensive : MonoBehaviour, IAIBehaviour {

	public virtual Action GetAction(Unit unit)
	{
		Unit target=null;
		Action retValue=new Action(unit.Tile);
		HashSet<Tile> possibleAttacks = new HashSet<Tile> ();
		HashSet<Tile> possibleMoves = unit.GetReachableTiles ();//Gets all possible moves
		possibleMoves.Add (unit.Tile);
		///Judge Attacks
		foreach (Tile o in possibleMoves) {//Gets all possible attacks
			possibleAttacks.UnionWith (unit.AttackInfo.GetAttackTiles (unit, o));
		}
		int maxJudge = 0;
		possibleAttacks.Remove (unit.Tile);
		if (possibleAttacks.Count > 0) {
			maxJudge = 0;
			foreach (Tile o in possibleAttacks) {
				if (o.Unit.invisible)
				{
					continue;
				}
				else if (unit.CanMurder (o.Unit)) {
					target = o.Unit;
					break;
				} else {
					int temp = unit.AttackInfo.effect.judgeAttack (unit, o.Unit); // TODO got a null reffence here during AI turn. I think it was after a united died from retaliation.
					if (temp > maxJudge) {
						target = o.Unit;
						maxJudge = temp;
					}
				}
			}
			if (target != null) {
				retValue.attack=target.Tile;
				possibleMoves.IntersectWith (unit.AttackInfo.reach.GetTiles (target.Tile));
				HashSet<Tile> safeMoves=new HashSet<Tile>(target.AttackInfo.reach.GetTiles(target.Tile));
				safeMoves.IntersectWith(possibleMoves);
				Tile moveTo = null;
				foreach (Tile o in possibleMoves) {
					moveTo = o;
					if(safeMoves.Count==0||!safeMoves.Contains(o))
					{
						break;
					}
				}
				retValue.movement=moveTo;
			}
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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
