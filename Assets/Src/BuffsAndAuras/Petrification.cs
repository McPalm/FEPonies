using System;
using UnityEngine;

public class Petrification : Debuff
{
	DurationBuff stone;
	// IReach attackRange;

	void Start()
	{
		Unit temp=GetComponent<Unit>();
		// temp.HasActed=false;
		Stats tempstats=new Stats();
		tempstats.defense=10;
		tempstats.dodgeBonus=-1;
		stone=new DurationBuff(-1, tempstats, temp);
		temp.AddInhibitor(this);
		// attackRange=temp.AttackInfo.reach;
		// temp.AttackInfo.reach=temp.AttackInfo.gameObject.AddComponent<Norange>();
	}

	void OnDestroy()
	{
		Unit temp=GetComponent<Unit>();
		temp.RemoveInhibitor(this);
		// temp.AttackInfo.reach=attackRange;
		stone.Destroy();
	}
}
