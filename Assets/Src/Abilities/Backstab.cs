using UnityEngine;
using System.Collections;

public class Backstab : Skill, Buff
{

	private Unit u;
	private bool validTarget = false;
	private Stats inactive;
	private Stats active;

	public override string Name
	{
		get
		{
			return "Backstab";
		}
	}

	public Stats Stats
	{
		get{return (validTarget) ? active : inactive;}
	}

	public bool Affects(Unit u)
	{
		if (u != this.u) return false;
		return true;
	}

	// Use this for initialization
	public void Start()
	{
		u = GetComponent<Unit>();

		inactive = new Stats();
		active = new Stats();
		active.strength = 4 + u.level / 2;

		BuffManager.Instance.Add(this);
	}

	public void StartingAttackSequence(Unit u)
	{
		validTarget = u.retaliationsLeft == 0;
	}

	public void FinishedAttackSequence(Unit u)
	{
		validTarget = false;
	}
}
