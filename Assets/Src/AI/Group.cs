using UnityEngine;
using System.Collections.Generic;

// this makes the AI shift a bit more naturally, making groups of enemies work together.
// only works if put on a Unit, prefferably an enemy unit...
// If the unit with this script moves or gets attacked, this and every other unit with this script within Range shifts into agressive AI.

public class Group : MonoBehaviour {

	public float range = 3;

	private bool _sleeping = true;

	static private HashSet<Group> _group = new HashSet<Group>();

	void Awake(){
		_group.Add(this);
	}
	
	void OnDestroy(){
		_group.Remove(this);
	}

	// called by unit when it starts moving
	void StartAnimatedMove(){
		if(_sleeping) ActivateAllInRange();
	}

	// called by unit when it engages in combat
	void StartingAttackSequence(){
		if(_sleeping) ActivateAllInRange();
	}

	void ActivateAllInRange(){
		_sleeping = false;
		GetComponent<Unit>().ChangeAI(gameObject.AddComponent<Aggressive>());
		Particle.ExlamationPoint(transform.position + new Vector3(-0.2f, +0.7f));
		foreach(Group g in _group){
			float
				dx = Mathf.Abs(g.transform.position.x - transform.position.x),
				dy = Mathf.Abs(g.transform.position.y - transform.position.y),
				distance = dx+dy;
			if(distance < range+0.1f) g.Activate();
		}
	}

	void Activate(){
		if(_sleeping){
			_sleeping = false;
			GetComponent<Unit>().ChangeAI(gameObject.AddComponent<Aggressive>());
			Particle.ExlamationPoint(transform.position + new Vector3(-0.2f, +0.7f));
			ActivateAllInRange();
		}
	}
}
