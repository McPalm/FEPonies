using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour, Observable {
	static internal UnitManager instance;
	HashSet<Unit> units;

	private HashSet<TurnObserver> turnEvents = new HashSet<TurnObserver>();
	private HashSet<TurnObserver> toRemoveTurnEvents = new HashSet<TurnObserver>();

	public const int PLAYER_TEAM = 0;
	public const int AI_TEAM = 1;
	public const int ALLY_TEAM = 2;
	public const int FERAL_TEAM = 3;
	private int turn = 0;

	/// <summary>
	/// Gets the current turn.
	/// That is, how many turns it has been since the start of the map.
	/// Current turn increases at the beginning of the Player_Team phase.
	/// </summary>
	/// <value>The curr turn.</value>
	public int currTurn{
		get{
			return turn;
		}
	}

	static public UnitManager Instance{
		set{}
		get{
			if(instance == null){
				GameObject tempObj = new GameObject("UnitManager");
				tempObj.AddComponent<UnitManager>();
			}
			return instance;
		}
	}

	public void Add(Unit unit)
	{
		units.Add(unit);
		notifyObservers();
	}

	/// <summary>
	/// Refresh the specified teams has acted status.
	/// </summary>
	/// <param name="team">0 = players, 1 = AI</param>
	public void Refresh(int team)
	{
		foreach(Unit o in units)
		{
			if(o.team==team)
			{
				o.HasActed=false;
                o.retaliationsMade = 0;
			}
		}

	}

	public HashSet<Unit> GetUnits()
	{
		return units;
	}

	public HashSet<Unit> GetUnitsByTeam(int team)
	{
		HashSet<Unit> temp=new HashSet<Unit>();
		foreach(Unit o in units)
		{
			if(o.team==team)
			{
				temp.Add(o);
			}
		}
		return temp;
	}

	public HashSet<Unit> GetUnitsByHostility(Unit unit)
	{
		HashSet<Unit> temp=new HashSet<Unit>();
		foreach(Unit o in units)
		{
			if(o.isHostile(unit))
			{
				temp.Add(o);
			}
		}
		return temp;
	}

	public HashSet<Unit> GetUnitsByFriendliness(Unit unit)
	{
		HashSet<Unit> temp=new HashSet<Unit>();
		foreach(Unit o in units)
		{
			if(!o.isHostile(unit))
			{
				temp.Add(o);
			}
		}
		return temp;
	}

	public HashSet<Unit> GetNearbyAllies(Unit unit, int range = 3){
		HashSet<Unit> retVal = new HashSet<Unit>();
		foreach(Unit u in GetUnitsByFriendliness(unit)){
			if(u == unit) continue;
			int dx = (int)System.Math.Abs( u.transform.position.x - unit.transform.position.x);
			int dy = (int)System.Math.Abs( u.transform.position.y - unit.transform.position.y);
			if(dx+dy > range) continue;
			retVal.Add(u);
		}
		return retVal;
	}

	public void EndTurn(int team)
	{
		Refresh(team);
		if(team == PLAYER_TEAM)
		{
			StateManager.Instance.State=GameState.aiTurn;
			GUInterface.Instance.PrintMessage("Enemy Turn!");
		}
		else if(team == AI_TEAM)
		{
			StateManager.Instance.State=GameState.allyTurn;
			GUInterface.Instance.PrintMessage("Ally Turn!");
			Refresh(FERAL_TEAM);
		}
		else if(team == ALLY_TEAM)
		{
			turn++;
			StateManager.Instance.State=GameState.playerTurn;
			GUInterface.Instance.PrintMessage("Player Turn!");
		}
		cleanUpTurnObserver();
		foreach(TurnObserver ete in turnEvents){
			try{
				ete.Notify(turn);
			}catch(System.Exception e){
				Debug.LogException(e);
			}
		}
	}

	// Use this for initialization
	void Awake()
	{
		if(instance == null){
			instance = this;
			units= new HashSet<Unit>();
			observerCollection=new HashSet<Observer>();
			toRemove= new HashSet<Observer>();
			// put initiation code here!			
		}else{
			Destroy(gameObject);
		}
	}


	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(StateManager.Instance.State== GameState.noState){ // kick of the gamu!
			EndTurn(ALLY_TEAM);
		}
		if (StateManager.Instance.State==GameState.playerTurn)
		{
			if(Input.GetButtonDown("EndTurn")&& (StateManager.Instance.State==GameState.playerTurn || StateManager.Instance.State == GameState.aiTurn ))// Check if ending turn
			{
				EndTurn(PLAYER_TEAM);
			}

			HashSet<Unit> temp=GetUnitsByTeam(PLAYER_TEAM);
			bool shouldEnd=true;
			foreach(Unit o in temp)
			{
				if(o.HasActed==false)
				{
					shouldEnd=false;
				}
			}
			if (shouldEnd)
			{
				EndTurn(PLAYER_TEAM);
			}
		}

		if (StateManager.Instance.State==GameState.aiTurn)
		{
			HashSet<Unit> temp=GetUnitsByTeam(AI_TEAM);
			temp.UnionWith(GetUnitsByTeam(FERAL_TEAM));
			bool shouldEnd=true;
			foreach(Unit o in temp)
			{
				if(o.HasActed==false)
				{
					shouldEnd=false;
				}
			}
			if (shouldEnd)
			{
				EndTurn(AI_TEAM);
			}
		}

		if (StateManager.Instance.State==GameState.allyTurn)
		{
			HashSet<Unit> temp=GetUnitsByTeam(ALLY_TEAM);
			bool shouldEnd=true;
			foreach(Unit o in temp)
			{
				if(o.HasActed==false)
				{
					shouldEnd=false;
				}
			}
			if (shouldEnd)
			{
				EndTurn(ALLY_TEAM);
			}
		}

		if(Input.GetButtonDown("EndTurn")&&StateManager.Instance.State==GameState.unitSelected)//Confirm movement
		{
			Unit.SelectedUnit.FinnishMovement();
		}
	}

	/*
	public void OnGUI(){
		// make a button that calls EndTurn(0) when pressed.
		if(StateManager.Instance.State == GameState.playerTurn){
			if (GUI.Button (new Rect (Screen.width-100, Screen.height-75, 80,60), "End Turn")) {
				EndTurn (0);
			}
		}
	}
	*/

	public void Remove(Unit unit){
		units.Remove(unit);
		if(AIManager.instance != null) AIManager.instance.Remove(unit);
		notifyObservers();
	}

	protected HashSet<Observer> observerCollection;
	protected HashSet<Observer> toRemove;

	public void registerObserver(Observer obs)
	{
		observerCollection.Add(obs);
	}

	public void unregisterObserver(Observer obs)
	{
		toRemove.Add(obs);
	}


	private bool notifying = false, notify = false;
	public void notifyObservers()
	{
		if(!notifying){	
			notifying = true;
			notify = false;
			cleanUpToRemove();
			// should not do this during cleanup!   how!!!?  unity hoooowww!?
			foreach(Observer o in observerCollection)
			{	
				try{
					o.Notify();
				}catch(System.Exception e){
					Debug.LogException(e);
				}
			}
			notifying = false;
			if(notify){
				notifyObservers();
			}
		}else{
			notify = true;
		}

	}

	private void cleanUpToRemove()
	{
		foreach(Observer o in toRemove)
		{
			observerCollection.Remove(o);
		}
		toRemove.Clear();
	}

	public void RegisterTurnObserver(TurnObserver e){
		turnEvents.Add(e);
	}

	public void unRegisterTurnObserver(TurnObserver e)
	{
		toRemoveTurnEvents.Add(e);
	}

	private void cleanUpTurnObserver()
	{
		foreach(TurnObserver o in toRemoveTurnEvents)
		{
			turnEvents.Remove(o);
		}
		toRemove.Clear();
	}

	public bool IsItMyTurn(Unit u){
		if(u.team == PLAYER_TEAM)
			return(StateManager.Instance.Turn == GameState.playerTurn);
		if(u.team == AI_TEAM || u.team == FERAL_TEAM)
			return(StateManager.Instance.Turn == GameState.aiTurn);
		if(u.team == ALLY_TEAM)
			return(StateManager.Instance.Turn == GameState.allyTurn);
		return false;
	}
}