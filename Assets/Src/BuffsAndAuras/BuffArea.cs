using UnityEngine;
using System.Collections;

public class BuffArea : MonoBehaviour {

	[Range(0, 10)]
	public int radius;
	public Stats buff;
	private bool _affectsEnemies;
	private Unit _user;
	/// <summary>
	/// If the buff is active
	/// Can be used to tempararly disable an area buff.
	/// </summary>
	public bool Active = true;

	public void Initialize(int _radius, Stats _buff, bool affectsEnemies = false)
	{
		this.radius = _radius;
		this.buff = _buff;
		this._affectsEnemies = affectsEnemies;
		this._user = GetComponent<Unit>(); // pretty hack, might come back and chew me un the bum.
		BuffManager.Instance.Add(this);
	}
	
	public bool Includes(Unit u, Tile t)
	{
		if (!Active) return false;
		if (_user != null && !_affectsEnemies && _user.isHostile(u)) return false;

		return TileGrid.GetDelta(t, this) <= radius;
	}

	/*
	public bool Affects (Tile t)
	{
		if(!Active) return false;
		if(_user != null && !_affectsEnemies && _user.isHostile(u)) return false;

		// see if under radius
		return TileGrid.GetDelta(u, this) <= radius;
	}
	*/

	public Stats Stats {
		get {
			return buff;
		}
	}

	public void Stop(){
		BuffManager.Instance.RemoveArea(this);
	}

	public void Start(){
		BuffManager.Instance.Add(this);
	}

	void OnDestroy(){
		BuffManager.Instance.RemoveArea(this);
	}
}
