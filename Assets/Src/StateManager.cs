using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : Stack<GameState>{
	static private StateManager instance;
	private bool debugging = false;

	static public StateManager Instance{
		private set{}
		get{
			if (instance == null)
			{
				instance = new StateManager();
			}
			return instance;
		}
	}

	private GameState lastTurnState;
	private GameState lastState;

	private Stack<GameState> stateStack;


	/// <summary>
	/// Gets or sets the state.
	/// </summary>
	/// <value>The state.</value>
	public GameState State{
		get{
			if(Count == 0){
				return GameState.noState;
			}else{
				return Peek();
			}
		}
		set{
			if(Count > 0){
				Pop();
			}
			Push(value);			

			if(debugging) Debug.Log("Entering " + value);
			// a bit haxxy right now. But functionally as intended?
			if(value == GameState.aiTurn || value == GameState.playerTurn || value == GameState.allyTurn){
				lastTurnState = value;
			}
		}
	}

	/// <summary>
	/// Gets the current turn, regardles of what exact state the game is currently in.
	/// </summary>
	/// <value>GameState.playerTurn or GameState.aiTurn</value>
	public GameState Turn{
		get{return lastTurnState;}
	}

	public void DebugPush(GameState state){
		if(debugging) Debug.Log("Entering state: " + state);
		Push(state);
	}

	/// <summary>
	/// Pop the current gamestate from the stack. Does not return the state, use State before popping to find out current state.
	/// </summary>
	public void DebugPop(){
		Pop();
		if(debugging) Debug.Log("Entering state: " + State);
	}

	public void Clean(){
		while(Count != 0){
			Pop();
		}
	}
}
