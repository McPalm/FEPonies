using UnityEngine;
using System.Collections;
using System;

public class Backstab : Passive, IAttackModifier, AttackBuff
{

	private Unit u;
	private Stats hitBonus;

	public override string Name
	{
		get
		{
			return "Backstab";
		}
	}

	public int Priority
	{
		get
		{
			return 1;
		}
	}

	public Stats Stats
	{
		get
		{
			return hitBonus;
		}
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		// sneak attack if out retaliations
		if(target.RetaliationsLeft == 0) return true;
		// sneak attack if they cannot attack the tile
		if (target.AttackInfo.Reach.GetTiles(targetLocation).Contains(source)) return false;
		return true;
	}

	// Use this for initialization
	public void Start()
	{
		u = GetComponent<Unit>();
		hitBonus = new Stats();
		hitBonus.hitBonus = 0.2f;
		u.RegisterAttackBuff(this);
	}

	public void Test(DamageData dd)
	{
		if(Applies(dd.target, dd.SourceTile, dd.target.Tile))
		{
			dd.damageMultipler *= 1.2f;
			dd.flatBonus += 1;
			dd.backstab = true;
		}
	}
}
