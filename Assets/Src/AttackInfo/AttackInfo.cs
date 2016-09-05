using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackInfo : MonoBehaviour {




	public IReach reach;
	public ITargetFilter filter;
	public IEffect effect;
	public IAnimation attackAnimation;
	
	void Awake(){
		foreach(MonoBehaviour m in GetComponents<MonoBehaviour>())
		{

			if(m is IReach){
				reach = (IReach)m;
			}else if(m is ITargetFilter){
				filter = (ITargetFilter)m;
			}else if(m is IEffect){
				effect = (IEffect)m;
			}else if(m is IAnimation){
				attackAnimation = (IAnimation)m;
			}
		}
		if(attackAnimation == null){
			attackAnimation = gameObject.AddComponent<Tackle>();
		}
	}

	public List<Tile> GetAttackTiles(Unit unit)
	{
		List<Tile> retVal;
		retVal=reach.GetTiles(unit.Tile);
		for(int i=retVal.Count-1;i>=0;i--)
		{
			if(!filter.ValidateTarget(retVal[i],unit))
			{
				retVal.RemoveAt(i);
			}
		}
		return retVal;
	}
	
	/// <summary>
	/// Gets the attack tiles.
	/// Use this if checking other than the units position
	/// </summary>
	/// <returns>The attack tiles.</returns>
	/// <param name="unit">Unit.</param>
	/// <param name="position">Position.</param>
	public List<Tile> GetAttackTiles(Unit unit, Tile position)
	{
		List<Tile> retVal;
		retVal=reach.GetTiles(position);
		for(int i=retVal.Count-1;i>=0;i--)
		{
			if(!filter.ValidateTarget(retVal[i],unit))
			{
				retVal.RemoveAt(i);
			}
		}
		return retVal;
	}

	public bool CanAttack(Unit user, Unit target){
		if(target) return GetAttackTiles(user).Contains(target.Tile);
		return false;
	}
}
