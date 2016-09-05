using UnityEngine;
using System.Collections.Generic;

public class BuffManager{

	private HashSet<Buff> _buffs = new HashSet<Buff>();

	static private BuffManager instance;
	static public BuffManager Instance{
		get{
			if(instance == null){
				instance = new BuffManager();
			}
			return instance;
		}
	}

	/// <summary>
	/// Add the specified buff.
	/// </summary>
	/// <param name="buff">Buff.</param>
	public void Add(Buff buff){
		_buffs.Add(buff);
	}

	/// <summary>
	/// Gets the buffs.
	/// </summary>
	/// <returns>The stat buffs currently in effect on a certain unit.</returns>
	/// <param name="u">U.</param>
	public Stats GetBuffs(Unit unit){
		Stats rv = new Stats();
		foreach(Buff buff in _buffs){
			if(buff.Affects(unit)){
				rv += buff.Stats;
			}
		}
		return rv;
	}

	public void RemoveBuff(Buff buff){
		_buffs.Remove(buff);
	}

	public void Clear ()
	{
		_buffs.Clear();
	}
}
