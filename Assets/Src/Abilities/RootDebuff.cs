﻿using UnityEngine;
using System.Collections.Generic;

public class RootDebuff : Debuff, TurnObserver, Buff {

	public GameObject root;

	public int duration = 2;

	private GameObject instaRoot;
	private Character target;
	private Unit owner;

	private Stats mod;

	// Use this for initialization
	void Start () {
		mod = new Stats();
		mod.movement = new UnitMove(-99);
		mod.dodgeBonus = -0.3f;
		mod.hitBonus = -0.1f;
		UnitManager.Instance.RegisterTurnObserver(this);
		target = GetComponent<Character>();

		instaRoot = Instantiate(Resources.Load("Vine"), transform.position, Quaternion.identity) as GameObject;
		instaRoot.transform.parent = this.transform;

		target.AddBuff(this);
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	public void initialize(int duration, Unit owner){
		this.duration = duration;
		this.owner = owner;
	}

	public void Notify (int turn)
	{
		GameState gs = StateManager.Instance.Turn;
		if(gs == GameState.allyTurn) Tick ();
	}

	public void Update(){
		if(owner && owner.IsAlive){
		}else{
			Destroy(this);
		}
	}

	
	void Tick(){
		duration--;
		if(duration == 0) Destroy(this);
	}
	
	public void Stack(int duration)
	{
		this.duration = Mathf.Max(duration, this.duration);
	}
	
	
	void OnDestroy(){
		UnitManager.instance.unRegisterTurnObserver(this);
		target.RemoveBuff(this);
		Destroy(instaRoot);
	}

	public Stats Stats{
		get{return mod;}
	}

	// called by unit on death
	void UnitDeath(){
		Destroy(this);
	}
}
