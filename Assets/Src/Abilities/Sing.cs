using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Sing : Ability, AIAbility, SustainedAbility, IDefenceModifiers {

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
				_nextParticle = UnityEngine.Random.Range(0.1f, 4f);
			}
		}

	}

	public override void Use ()
	{
		if(_isSinging){
			_isSinging = false;
		}else{
			_isSinging = true;
			HandOutBuff();
			Note();
			_nextParticle = 0.1f;
			FinishUse();
		}
	}

	private void HandOutBuff()
	{
		foreach (Unit u in UnitManager.Instance.GetUnitsByFriendliness(GetComponent<Unit>()))
		{
			if (u.GetComponent<SingBuff>() == null)
			{
				SingBuff sb = u.gameObject.AddComponent<SingBuff>();
				sb.Initialize(this);
			}
		}
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

	public bool Active
	{
		get
		{
			return _isSinging;
		}
	}

	public int Priority
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public void NotifyHealth (DamageData dd)
	{
		if(dd.FinalDamage > 0){
			if(_isSinging) Particle.Clave(transform.position);
			_isSinging = false;
			// SFXPlayer.Instance.Scratch();
		}
	}

	private void Note(){
		Particle.NoteParticle(  (transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.4f), UnityEngine.Random.Range(0.3f, 0.7f), 0f))  );
	}

	public void Test(DamageData dd)
	{
		dd.RegisterCallback(NotifyHealth);
	}
}
