using UnityEngine;
using System.Collections;

public enum GameState{
	playerTurn,
	unitSelected,
	aiTurn,
	unitMoving,
	retaliate,
	unitAttack,
	buttonMenu,
	dialogue,
	noState,
	LevelManagerAnimation,
	targetingAbility,
	allyTurn,
	movingCamera,
	paused,
	engageTargetEvent,
	evaluateAttack,
	runningAttackSequence,
	changingSkin,
	fadeToBlack
}
