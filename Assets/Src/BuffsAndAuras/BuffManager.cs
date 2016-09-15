using UnityEngine;
using System.Collections.Generic;

public class BuffManager{

	private HashSet<Buff> _buffs = new HashSet<Buff>();
	private HashSet<BuffArea> _areas = new HashSet<BuffArea>();

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

	public void Add(BuffArea area)
	{
		_areas.Add(area);
	}

	/// <summary>
	/// Gets the buffs.
	/// </summary>
	/// <returns>The stat buffs currently in effect on a certain unit.</returns>
	/// <param name="u">U.</param>
	public Stats GetBuffs(Unit unit, Tile tile = null){
		if (tile == null) tile = unit.Tile;
		Stats rv = new Stats();
		foreach(Buff buff in _buffs){
			if(buff.Affects(unit)){
				rv += buff.Stats;
			}
		}
		foreach(BuffArea area in _areas)
		{
			if(area.Includes(unit, tile))
			{
				rv += area.Stats;
			}
		}
		return rv;
	}

	public void RemoveBuff(Buff buff){
		_buffs.Remove(buff);
	}

	public void RemoveArea(BuffArea area)
	{
		_areas.Remove(area);
	}

	public void Clear ()
	{
		_buffs.Clear();
	}
}
