using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour, TurnObserver
{
	static internal AIManager instance;
	private List<Unit> units = new List<Unit>();
	private Unit target;
	private Action currentCommand;
	private int _framesSinceTurnshift = 0;
	private HashSet<Unit> _secondPass = new HashSet<Unit>();

	static public AIManager Instance {
		get {
			if (instance == null) {
				GameObject tempObj = new GameObject ("AIManager");
				tempObj.AddComponent<AIManager> ();
			}
			return instance;
		}
	}
	
	void Awake ()
	{
		if (instance == null) {
			instance = this;
			UnitManager.Instance.RegisterTurnObserver(this);
		} else {
			Destroy (gameObject);
		}

	}

	// Update is called once per frame
	void Update ()
	{
		if(StateManager.Instance.Turn == GameState.playerTurn) return;

		if (StateManager.Instance.State == GameState.aiTurn) {
			_framesSinceTurnshift++;
			if(_framesSinceTurnshift > 1000){

			}
			// Looking for new actions!
			//if (target == null) 
			//{
			// Unit.SelectedUnit = null;  TODO verify this is still happening
			units.Sort(new Comparison<Unit>(SortByPosition));
			if(Unit.SelectedUnit == null){
				foreach (Unit o in units) {
					if (o.HasActed == false && (o.team==UnitManager.AI_TEAM || o.team==UnitManager.FERAL_TEAM) &! _secondPass.Contains(o)) {
						Action a = o.unitAI.GetAction(o);
						// we gunna do something with this bloke!
						if(a.IsDoingSomething){
							IssueCommand(o, a);
							return;
						}
						else{
							_secondPass.Add(o);
						}
					}
				}
				// second pass, looking again if those who did not act got something to do now.
				foreach (Unit o in _secondPass) {
					if (o.HasActed == false && (o.team==UnitManager.AI_TEAM || o.team==UnitManager.FERAL_TEAM)) {
						Action a = o.unitAI.GetAction(o);
						IssueCommand(o, a);
						break;
					}
				}
			}
		}
		else if(StateManager.Instance.State == GameState.unitSelected && currentCommand != null){
			// command the unit around here!
			if((currentCommand.movement != null) && (currentCommand.movement != Unit.SelectedUnit.Tile)){ // if its not already standing where it supposed to move
				Unit.SelectedUnit.MoveToAndAnimate(currentCommand.movement);
			}else if(currentCommand.attack != null){
				Unit.SelectedUnit.StartAttackSequence(currentCommand.attack.Unit);
				currentCommand = null;
			}else if(currentCommand.ability != null){
				if(currentCommand.ability is TargetedAbility)
				{
					((TargetedAbility)currentCommand.ability).Notify(currentCommand.abilityTarget);
				}
				else
				{
					currentCommand.ability.Use();
				}
			}else{
				FinnishMovement();
			}
		}

		if (StateManager.Instance.State == GameState.allyTurn) {
			// Looking for new actions!
			//if (target == null) 
			//{
			// Unit.SelectedUnit = null;  TODO verify this is still happening
			if(Unit.SelectedUnit == null){
				foreach (Unit o in units) {
					if (o.HasActed == false&&o.team==UnitManager.ALLY_TEAM) {
						// we gunna do something with this bloke!
						IssueCommand(o, o.unitAI.GetAction(o));
						break;
					}
				}
			}
		}
	}

	/// <summary>
	/// Makes the AI manager forget about a unit. Most likely because of unit death.
	/// </summary>
	/// <param name="unit">Unit.</param>
	public void Remove (Unit unit)
	{
		units.Remove (unit);
	}

	public void AddUnit(Unit unit){
		units.Add(unit);
	}

	public void IssueCommand(Unit unit, Action command){
		StateManager.Instance.DebugPush(GameState.unitSelected); // entering unit selected state
		currentCommand = command;
		unit.Select();
		// focus camrea if the unit does something other than idling.
		try{
			if(currentCommand.IsDoingSomething){
				if(!CameraControl.IsOnScreen(unit.transform.position) || (command.movement != null &! CameraControl.IsOnScreen(command.movement.transform.position))){
					if(command.movement != null){
						CameraControl.FocusOn((unit.transform.position + command.movement.transform.position) * 0.5f);
					}else{
						CameraControl.FocusOn(unit.transform.position);
					}
				}
			}
		}catch(System.Exception e){
			Debug.LogException(e);
		}

	}

	private int SortByPosition(Unit a, Unit b)
	{
		float x=a.transform.position.x+a.transform.position.y*1000;
		float y=b.transform.position.y+b.transform.position.y*1000;
		return (int)(x-y);
	}

	private void FinnishMovement(){
		currentCommand = null;
		Unit.SelectedUnit.FinnishMovement();
	}

	public void Notify (int turn)
	{
		_framesSinceTurnshift = 0;
		_secondPass.Clear();
	}

}
