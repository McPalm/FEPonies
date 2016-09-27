using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BottomGUI : MonoBehaviour {

	public void EndTurn()
	{
		if (StateManager.Instance.State == GameState.playerTurn)
			UnitManager.instance.EndTurn(UnitManager.PLAYER_TEAM);
	}

	public void FinishMovement()
	{
		if (StateManager.Instance.State == GameState.unitSelected)
			Unit.SelectedUnit.FinnishMovement();
	}

	public void Update()
	{
		if (Input.GetButtonDown("Skip")) FinishMovement();
	}
}
