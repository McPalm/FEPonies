using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sing : Ability, HealthObserver, AIAbility {

	private BuffArea _areaBuff;
	private bool _isSinging = false;
	private float _nextParticle = 5f;

	public override string Name {
		get {
			if( _isSinging){
				return "Stop Singing";
			}
			return "Boost Morale!";
		}
	}

	void Update(){
		if(_isSinging){
			_nextParticle -= Time.deltaTime;
			if(_nextParticle < 0f){
				Note();
				_nextParticle = Random.Range(0.1f, 4f);
			}
		}

	}

	public override void Use ()
	{
		if(_isSinging){
			_isSinging = false;
			_areaBuff.Stop();
		}else{
			_isSinging = true;
			if(_areaBuff == null){
				_areaBuff = gameObject.AddComponent<BuffArea>();
				_areaBuff.Initialize(3, new Stats());
				CalulateBuff();
				GetComponent<Unit>().RegisterHealthObserver(this);
			}else{
				_areaBuff.Start();
			}
			Note();
			_nextParticle = 0.1f;
			FinishUse();
		}
	}

	void CalulateBuff(){
		int i = GetComponent<Unit>().ModifiedStats.intelligence;
		Stats s = new Stats();
		s.strength = (i+7)/5;
		s.hitBonus = i*0.01f;
		_areaBuff.buff = s;
	}

	void LevelUp(){
		CalulateBuff();
	}

	public int judgeAbility(Unit user, Tile move, out Tile target)
	{
		target=null;
		int value=50;
		HashSet<Unit> enemyUnits=UnitManager.Instance.GetUnitsByHostility(user);
		foreach(Unit u in enemyUnits)
		{
			if(IncreasedRange.StaticGetTiles(u.Tile).Contains(move))
			{
				value+=3;
			}
		}
		return value;
	}


	public bool willCast()
	{
		if(_isSinging)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public Ability getAbility
	{
		get
		{
			return this;
		}
	}

	public void NotifyHealth (Unit unit, int change)
	{
		if(change < 0){
			_areaBuff.Stop();
			if(_isSinging) Particle.Clave(transform.position);
			_isSinging = false;
			// SFXPlayer.Instance.Scratch();
		}
	}

	private void Note(){
		Particle.NoteParticle(  (transform.position + new Vector3(Random.Range(-0.1f, 0.4f), Random.Range(0.3f, 0.7f), 0f))  );
	}
}
