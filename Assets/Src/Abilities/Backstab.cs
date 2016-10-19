using UnityEngine;
using System.Collections;

public class Backstab : Skill, AttackBuff
{

	private Unit u;
	private Stats damageBonus;

	public override string Name
	{
		get
		{
			return "Backstab";
		}
	}

	public Stats Stats
	{
		
		get{return damageBonus; }
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
		damageBonus = new Stats();
		damageBonus.might = 4 + u.Character.Level / 2;
		damageBonus.hitBonus = 0.2f;
		u.RegisterAttackBuff(this);
	}
}
