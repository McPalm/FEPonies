//------------------------------------------------------------------------------
// <auto-generated>
//     Denna kod har genererats av ett verktyg.
//     Körtidsversion:4.0.30319.34014
//
//     Ändringar i denna fil kan orsaka fel och kommer att förloras om
//     koden återgenereras.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class Charge : Passive, AttackBuff, TurnObserver
{
	int distance = 0;
	Tile startPosition;
	Stats _stats;

	public override string Name
	{
		get
		{
			return "Charge!";
		}
	}

	public Stats Stats
	{
		get
		{
			return _stats;
		}
	}

	void Start()
	{
		startPosition = TileGrid.Instance.GetTileAt(transform.position);
		UnitManager.Instance.RegisterTurnObserver(this);
		GetComponent<Unit>().RegisterAttackBuff(this);
	}

	public bool Applies(Unit target, Tile source, Tile targetLocation)
	{
		distance = TileGrid.GetDelta(startPosition, target);
		_stats.critBonus = (float)distance / 20f;
		if (distance > 3) _stats.might = 2;
		else _stats.might = 0;

		return true;
	}

	public void Notify(int turn)
	{
		startPosition = TileGrid.Instance.GetTileAt(transform.position);
	}
}