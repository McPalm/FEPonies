using UnityEngine;
using System.Collections;

public class Assassin : Skill {

	DurationBuff _buff;

	public override string Name {
		get {
			return "Assassin";
		}
	}

	// Use this for initialization
	void Start () {
		Unit u = GetComponent<Unit>();
		_buff = new DurationBuff(-1, new Stats(), u);
		Recalc();
	}

	void Recalc(){
		Stats s = new Stats();
		s.crit = GetComponent<Unit>().ModifiedStats.agility*0.02f + 0.2f;
		s.movement.moveSpeed = 1;
		_buff.Buff = s;
	}

	void OnDestroy(){
		BuffManager.Instance.RemoveBuff(_buff);
	}

	void UnitSelected(){
		Recalc();
	}
}
