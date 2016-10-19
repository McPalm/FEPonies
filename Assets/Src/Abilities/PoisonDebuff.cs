using UnityEngine;
using System.Collections.Generic;

public class PoisonDebuff : Debuff, TurnObserver {

	public int damage = 1;
	public int ticks = 5;

	public DamageType dt = new DamageType(DamageType.POISON);

	// Use this for initialization
	void Start () {
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	public void Notify (int turn)
	{
		GameState gs = StateManager.Instance.Turn;
		if(gs == GameState.aiTurn) Tick ();
		else if(gs == GameState.allyTurn) Tick ();
	}

	void Tick(){
		DamageData dd = new DamageData();
		dd.baseDamage = damage;
		dd.defenceMultiplier = 0f;
		GetComponent<Unit>().Damage(dd);
		ticks--;
		if(ticks == 0) Destroy(this);
		// TODO: apply poison sfx effect?
	}

	public void Stack(int damage, int ticks)
	{
		this.damage = Mathf.Max(damage, this.damage);
		this.ticks = Mathf.Max(ticks, this.ticks);
	}


	void OnDestroy(){
		UnitManager.instance.unRegisterTurnObserver(this);
	}
}
