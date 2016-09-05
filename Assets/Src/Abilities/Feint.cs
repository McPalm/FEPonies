using UnityEngine;
using System.Collections.Generic;

public class Feint : Passive, Buff {

	Unit c;
	Stats s;

	public override string Name {
		get {
			return "Feint";
		}
	}

	// Use this for initialization
	void Start () {
		c = GetComponent<Unit>();
		RefreshBonus();
		BuffManager.Instance.Add(this);
	}

	void LevelUp(){
		RefreshBonus();
	}
	
	void RefreshBonus(){
		int i = c.ModifiedStats.intelligence;
		s = new Stats();
		s.dodgeBonus = i * 0.01f;
		s.hitBonus = i * 0.02f;
	}

	public bool Affects (Unit u)
	{
		return c == u;
	}

	public Stats Stats {
		get{
			return s;
		}
	}
}
