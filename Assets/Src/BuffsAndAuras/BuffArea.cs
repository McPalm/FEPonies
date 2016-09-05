using UnityEngine;
using System.Collections;

public class BuffArea : MonoBehaviour, Buff {

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
		if(_user == null){
			Debug.LogWarning("AreaBuff found no user!");

		}
		BuffManager.Instance.Add(this);
	}
	

	public bool Affects (Unit u)
	{
		if(!Active) return false;
		if(_user != null && !_affectsEnemies && _user.isHostile(u)) return false;

		// see if under radius
		return TileGrid.GetDelta(u, this) <= radius;
	}

	public Stats Stats {
		get {
			return buff;
		}
	}

	public void Stop(){
		BuffManager.Instance.RemoveBuff(this);
	}

	public void Start(){
		BuffManager.Instance.Add(this);
	}

	void OnDestroy(){
		BuffManager.Instance.RemoveBuff(this);
	}
}
