using UnityEngine;
using System.Collections;

public class Guard : Ability, HealthObserver {
	int turnUsed = -1;

	public override string Name {
		get {
			return "Guard";
		}
	}

	public override void Use ()
	{
		Unit unit = GetComponent<Unit>();
		Stats s = new Stats();
		s.strength = unit.ModifiedStats.intelligence/3+1;
		s.hitBonus = 0.2f;
		s.dodgeBonus = -0.2f;
		new DurationBuff(1, s, unit);
		turnUsed = UnitManager.Instance.currTurn;

		FinishUse();
	}

	// Use this for initialization
	void Start () {
		GetComponent<Unit>().RegisterHealthObserver(this);
	}

	public void NotifyHealth (Unit unit, int change)
	{
		if(change <= 0 && turnUsed == UnitManager.instance.currTurn){
			unit.Heal(unit.ModifiedStats.intelligence/3+1);
		}
	}
}
