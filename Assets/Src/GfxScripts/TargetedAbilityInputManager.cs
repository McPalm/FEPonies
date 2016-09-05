using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetedAbilityInputManager : MonoBehaviour {

	public enum AreaOfEffect{
		SingleTile = 0,
		Burst = 1,
		Line = 2
	}

	static TargetedAbilityInputManager instance;
	private TargetedAbility listener;
	private AreaOfEffect _aoe = 0;
	private HashSet<Tile> _targets = new HashSet<Tile>();
	private Tile _orig;

	static public TargetedAbilityInputManager Instance{
		set{}
		get{
			if(instance == null){
				GameObject tempObj = new GameObject("TargetedAbilityManager");
				tempObj.AddComponent<TargetedAbilityInputManager>();
			}
			return instance;
		}
	}

	void Awake()
	{
		if(instance == null){
			instance = this;
			// put initiation code here!			
		}else{
			Destroy(gameObject);
		}
	}

	public void Listen(TargetedAbility listener, AreaOfEffect aoe = 0, Tile orig = null)
	{
		_aoe = aoe;
		_orig = orig;
		Tile.UnColourAll();
		this.listener = listener;
		StateManager.Instance.DebugPush( GameState.targetingAbility);
		_targets = listener.GetAvailableTargets();
		foreach(Tile t in _targets){
			t.ColourMe(Color.green);
		}
	}

	void OnGUI(){
		if(StateManager.Instance.State==GameState.targetingAbility){
			if(GUI.Button(new Rect(Screen.width/2-50, Screen. height-50, 100, 25), "Cancel")){
				StateManager.Instance.DebugPop();
				Unit.Deselect();
				// HACK goes under here...   sorta
				Tile.UnColourAll();
				StateManager.Instance.DebugPop();
			}
		}
	}

	// Update is called once per frame
	void LateUpdate () {
		if(StateManager.Instance.State==GameState.targetingAbility)
		{
			if(Input.GetMouseButtonDown(0))
			{
				Tile clicked=MousePosition.GetTile();
				if(listener.GetAvailableTargets().Contains(clicked))
				{
					StateManager.Instance.DebugPop();
					listener.Notify(clicked);
					Tile.UnColourAll();
				}

			}
			else if(_aoe == AreaOfEffect.Burst){
				Tile.UnColourAll();
				// colour tiles in the Area
				foreach(Tile t in _targets) t.ColourMe(Color.green);
				// colours tiles in the area of effect
				Tile mt = MousePosition.GetTile();
				if(_targets.Contains(mt)){
					foreach(Tile t in Burst(mt)) t.ColourMe(Color.red);
				}
			}else if(_aoe == AreaOfEffect.Line){
				Tile.UnColourAll();
				// colour tiles in the Area
				foreach(Tile t in _targets) t.ColourMe(Color.green);
				// colours tiles in the area of effect
				Tile mt = MousePosition.GetTile();
				if(_targets.Contains(mt)){
					foreach(Tile t in Line3(_orig, mt)) t.ColourMe(Color.red);
				}
			}
		}
	}

	public static IEnumerable<Tile> Burst(Tile t)
	{
		List<Tile> rw = new List<Tile>(5);
		rw.Add(t);
		if(t.North) rw.Add(t.North);
		if(t.East) rw.Add(t.East);
		if(t.South) rw.Add(t.South);
		if(t.West) rw.Add(t.West);

		return rw;
	}

	public static IEnumerable<Tile> Line3(Tile orig, Tile t){
		// change t to on that is exactly 3 away from orig
		Vector3 vt = t.transform.position;

		float dx = Mathf.Abs(orig.transform.position.x - vt.x);
		float dy = Mathf.Abs(orig.transform.position.y - vt.y);
		if(dx+dy < 4 && orig != t){
			if(dx+dy < 3){
				if(dx == 0){
					vt.y += Mathf.Sign(vt.y -orig.transform.position.y)*(3-dx-dy);
				}else if(dx == 1 && dy == 1){
					vt.y += Mathf.Sign(vt.y -orig.transform.position.y);
				}else{
					vt.x += Mathf.Sign(vt.x -orig.transform.position.x)*(3-dx-dy);
				}
			}
			return Line (orig, vt);
		}

		return new List<Tile>();
	}


	/// <summary>
	/// Get a line between orig and t.
	/// Only gives satisfactory results if dx+dy is an odd number.
	/// </summary>
	/// <param name="orig">Original.</param>
	/// <param name="t">T.</param>
	public static IEnumerable<Tile> Line(Tile orig, Vector2 destination){
		HashSet<Tile> rw = new HashSet<Tile>();

		Vector2 origp = orig.transform.position;

		RaycastHit2D[] rays = Physics2D.RaycastAll(origp,
		                                           destination-origp,
		                                           Vector3.Distance(origp, destination));

		foreach(RaycastHit2D ray in rays){
			Tile lt = ray.transform.gameObject.GetComponent<Tile>();
			if(lt == null || lt.Type == TileType.wall) break;
			rw.Add(lt);
		}
		rw.Remove(orig);

		return rw;
	}
}
