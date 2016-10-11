using UnityEngine;
using System.Collections;

public class DurationBuff : TurnObserver, Buff {

	public bool Affects (Unit u){
		return u.Equals(target);
	}

	public Stats Stats {
		get {
			return buff;
		}
	}

	public Stats Buff {
		get {
			return this.buff;
		}
		set {
			buff = value;
		}
	}

	int endTurn;
	Stats buff;
	Unit target;

	/// <summary>
	/// Initializes a new instance of the <see cref="Buff"/> class.
	/// </summary>
	/// <param name="duration">Duration. number of player turns it persists. Buffs with a duration of 1 expires at the start of the next player phase. -1 is infinite duration</param>
	/// <param name="b">The buff</param>
	/// <param name="t">Unit who will benefit from the buff</param>
	public DurationBuff(int duration, Stats b, Unit t)
	{
		endTurn=UnitManager.Instance.currTurn + duration;
		//BuffManager.Instance.Add(this);
		t.Character.AddBuff(this);
		buff=b;
		target=t;
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	public void Notify(int turn)
	{
		if (turn==endTurn && StateManager.Instance.Turn == GameState.playerTurn)
		{
			Destroy();
		}
	}

	public void Destroy(){
		UnitManager.instance.unRegisterTurnObserver(this);
		target.Character.RemoveBuff(this);
		//BuffManager.Instance.RemoveBuff(this);
	}
}
