using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackInfo
{

	private IReach reach;
	private ITargetFilter filter;
	private IEffect effect;
	private IAnimation attackAnimation;

	public IReach Reach
	{
		get
		{
			return reach;
		}

		set
		{
			reach = value;
		}
	}

	public ITargetFilter Filter
	{
		get
		{
			return filter;
		}

		set
		{
			filter = value;
		}
	}

	public IEffect Effect
	{
		get
		{
			return effect;
		}

		set
		{
			effect = value;
		}
	}

	public IAnimation AttackAnimation
	{
		get
		{
			return attackAnimation;
		}

		set
		{
			attackAnimation = value;
		}
	}

	public AttackInfo(IReach reach = null, IEffect effect = null, ITargetFilter filter = null, IAnimation animation = null)
	{
		this.Reach = reach;
		this.Effect = effect;
		this.Filter = filter;
		AttackAnimation = animation;

		if (reach == null)
			this.Reach = new Melee();
		if (effect == null)
			this.Effect = new Damage();
		if (filter == null)
			this.Filter = new TargetEnemy();
		if (animation == null)
			AttackAnimation = new Tackle();
	}

	public List<Tile> GetAttackTiles(Unit unit)
	{
		List<Tile> retVal = Reach.GetTiles(unit.Tile);
		for(int i=retVal.Count-1;i>=0;i--)
		{
			if(!Filter.ValidateTarget(retVal[i],unit))
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
		retVal=Reach.GetTiles(position);
		for(int i=retVal.Count-1;i>=0;i--)
		{
			if(!Filter.ValidateTarget(retVal[i],unit))
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

	public bool CanAttack(Unit user, Tile position, Tile targetPosition)
	{
		return GetAttackTiles(user, position).Contains(targetPosition);
	}
}
