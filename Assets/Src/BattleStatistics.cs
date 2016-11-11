using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

// a class that keeps track of what happens during combat and keeps statistics
public class BattleStatistics : MonoBehaviour, Observer {

	List<Qbert> qberts;


	// Use this for initialization
	void Start () {
		SceneManager.sceneLoaded += SceneLoaded;
		qberts = new List<Qbert>();
	}

	void SceneLoaded(Scene scene, LoadSceneMode m)
	{
		History.Instance.registerObserver(this);
		foreach(Qbert q in qberts)
		{
			print(q);
		}
		qberts.Clear();
	}

	public void Notify()
	{
		Action a = History.Instance.Peek();
		foreach(DamageData d in a.damageDatas)
		{
			if (d.killingBlow) KillingBlow(d.source);
			TallyDamage(d.source, d.target, d.FinalDamage);
		}
		if (a.damageDatas != null && a.damageDatas.Count > 0)
			TallyFights(a.damageDatas[0].source, a.damageDatas[0].target);
	}

	void TallyFights(Unit source, Unit target)
	{
		Qbert q = GetStats(source);
		Qbert qt = GetStats(target);
		if (q != null) q.attack++;
		if (qt != null) qt.defend++;
	}


	void TallyDamage(Unit source, Unit target, int damage)
	{
		Qbert q = GetStats(source);
		Qbert qt = GetStats(target);
		if(q != null) q.damageDone += damage;
		if(qt != null) qt.damageSuffered += damage;
	}

	void KillingBlow(Unit u)
	{
		Qbert q = GetStats(u);
		if(q != null) q.killingBlows++;
	}

	Qbert GetStats(Unit u)
	{
		if (u.team == UnitManager.PLAYER_TEAM)
		{
			foreach (Qbert q in qberts)
			{
				if (q.unit == u) return q;
			}
			qberts.Add(new Qbert(u));
			return qberts[qberts.Count - 1];
		}
		return null;
	}

	class Qbert
	{
		public Unit unit;
		public string name;
		public int killingBlows = 0;
		public int damageDone = 0;
		public int damageSuffered = 0;
		public int attack = 0;
		public int defend = 0;

		public int Combats
		{
			get
			{
				return attack + defend;
			}

		}

		public Qbert(Unit unit)
		{
			this.unit = unit;
			name = unit.Character.Name;
		}

		public override string ToString()
		{
			return string.Format("{0} has been involved in {1} fights, dealing a total of {2} damage and sustained {3}. In this time {0} scored {4} killing blows.", name, Combats, damageDone, damageSuffered, killingBlows);
		}
	}
}