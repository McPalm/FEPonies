using UnityEngine;
using System.Collections.Generic;

public class Bastion : AbilityWithManacost, TurnObserver {

	private BuffArea _buffAura;
	public int _duration;

	void Start(){
		_buffAura = gameObject.AddComponent<BuffArea>();
		_buffAura.radius = 2;
		_buffAura.Active = false;
		UnitManager.Instance.RegisterTurnObserver(this);
	}

	public override string Name {
		get {
			return "Bastion";
		}
	}

	public override void Use ()
	{
		_buffAura.Active = true;
		recaculateBuff();
		_duration = 2;
		// get units within 2 and particle them up
		Unit u = GetComponent<Unit>();
		foreach(Tile t in TileGrid.Instance.GetTilesAt(transform.position, 2)){
			if(t.isOccuppied &&  !t.Unit.isHostile(u)) Particle.BastionShield(t.transform.position);
		}



		FinishUse();
	}

	public override int ManaCost {
		get {
			return 1;
		}
	}

	public void Notify (int turn)
	{
		if(StateManager.Instance.Turn == GameState.playerTurn){
			_duration--;
			if(_duration == 0){
				_buffAura.Active = false;
				// get units with 2 and paricle them up
				Unit u = GetComponent<Unit>();
				foreach(Tile t in TileGrid.Instance.GetTilesAt(transform.position, 2)){
					if(t.isOccuppied &&  !t.Unit.isHostile(u)) Particle.BrokenShield(t.transform.position);
				}
			}
		}
	}

	private void recaculateBuff(){
		int intel = GetComponent<Unit>().ModifiedStats.intelligence;
		Stats s = new Stats();
		s.defense = (intel+2)/5+2;
		s.resistance = (intel+2)/5+2;
		_buffAura.Initialize(2, s, false);

	}
}
