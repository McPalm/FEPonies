using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour, IComparable<Tile>
{
	//Pathfinder Variables
	public float G;
	public float H
	{
		get
		{
			return ((Mathf.Abs(transform.position.x - Pathfinder.endTile.transform.position.x)) + (Mathf.Abs(transform.position.y - Pathfinder.endTile.transform.position.y)));
		}
	}
	public float F
	{
		get
		{
			return G + H;
		}
	}
	public int CompareTo(Tile other)
	{
		return (int)(F - other.F);
	}
	public bool Visible = true;

	//End of pathfinder variables
	public SpriteRenderer overlay;
	protected int moveLeft;

	protected Tile east, west, south, north;
	protected Unit unit;

	private HashSet<UnityEngine.Object> _slows = new HashSet<UnityEngine.Object>();

	public bool Slow
	{
		get { return _slows.Count > 0; }
	}

	public virtual int Movecost
	{
		get
		{
			return 1;
		}
	}

	public virtual bool ShootThrough
	{
		get { return true; }
	}
	public Unit Unit
	{
		get
		{
			return unit;
		}
	}

	public bool isOccuppied
	{
		get
		{
			if (unit != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public Tile East
	{
		get
		{
			return east;
		}
	}

	public Tile South
	{
		get
		{
			return south;
		}
	}

	public Tile North
	{
		get
		{
			return north;
		}
	}

	public Tile West
	{
		get
		{
			return west;
		}
	}

	public virtual TileType Type
	{
		get
		{
			return TileType.regular;
		}
	}

	public virtual void GetReachableTiles(UnitMove move, HashSet<Tile> reachTiles, Unit team)
	{
		if (!isOccuppied)
		{
			this.moveLeft = move.moveSpeed;
			reachTiles.Add(this);
		}
		else if (this.Unit.isHostile(team))
		{
			return;
		}

		if (move.moveType == MoveType.flying)
		{
			move.moveSpeed--;
		}
		else
		{
			move.moveSpeed -= Movecost;
		}

		if (move.moveSpeed >= 0)
		{
			if (Slow) move.moveSpeed = 0;
			if (north != null) north.GetReachableTiles((north.Slow) ? new UnitMove(0) : move, reachTiles, team);
			if (south != null) south.GetReachableTiles((south.Slow) ? new UnitMove(0) : move, reachTiles, team);
			if (west != null) west.GetReachableTiles((west.Slow) ? new UnitMove(0) : move, reachTiles, team);
			if (east != null) east.GetReachableTiles((east.Slow) ? new UnitMove(0) : move, reachTiles, team);
		}
		else
		{
			return;
		}
	}

	public virtual HashSet<Tile> GetAttackableTiles(HashSet<Tile> attackTiles)
	{
		throw new System.NotImplementedException();
	}

	public bool enterTile(Unit unit)
	{
		if (this.unit != null)
		{
			return false;
		}
		this.unit = unit;
		return true;
	}

	public Unit leaveTile()
	{
		if (this.unit == null)
		{
			return unit;
		}
		Unit temp = unit;
		unit = null;
		return temp;
	}

	// Use this for initialization
	protected void Awake()
	{
		if (TileGrid.Instance.Add(this))
		{

			east = TileGrid.Instance.GetTileAt((Vector2)transform.position + new Vector2(1, 0));
			if (east != null)
			{
				east.west = this;
			}
			west = TileGrid.Instance.GetTileAt((Vector2)transform.position + new Vector2(-1, 0));
			if (west != null)
			{
				west.east = this;
			}
			north = TileGrid.Instance.GetTileAt((Vector2)transform.position + new Vector2(0, 1));
			if (north != null)
			{
				north.south = this;
			}
			south = TileGrid.Instance.GetTileAt((Vector2)transform.position + new Vector2(0, -1));
			if (south != null)
			{
				south.north = this;
			}

			overlay.enabled = false;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		if (StateManager.Instance.State == GameState.unitSelected)
		{
			if (Input.GetButtonDown("Cancel") && StateManager.Instance.Turn == GameState.playerTurn)
			{
				Pauser.Instance.lockme = true;
				CancelSelection();
			}
			else if (Unit.SelectedUnit == null)
			{
				StateManager.Instance.DebugPop();
				UnColourAll();
			}
		}
	}


	void OnDrawGizmosSelected()
	{
		overlay.enabled = false;
	}

#if UNITY_STANDALONE
	void OnMouseDown()
	{
		Clicked();
	}
#elif UNITY_EDITOR
	void OnMouseDown(){
		Clicked();
	}
#endif

	// when this tile is clicked
	void Clicked()
	{
		if (Input.mousePosition.y < CameraControl.BottomBorder) return; // ignore those under the bottom thingy of the screen.

		if (!Visible) return;
		Unit selectedUnit = Unit.SelectedUnit;


		switch (StateManager.Instance.State)
		{
			case GameState.playerTurn:
				if (isOccuppied && Unit.CanMove)
				{
					// Here we select a unit.
					SelectUnit(selectedUnit);
				}
				else if (isOccuppied && Unit.team != UnitManager.PLAYER_TEAM && !Unit.invisible)//If it is not the player team.
				{
					if (Unit.selectedEnemies.Contains(Unit))
					{
						deSelectEnemy(Unit);
					}
					else
					{
						selectEnemy(Unit);
					}

				}
				break;
			case GameState.unitSelected:
				if (this == Unit.SelectedUnit.Tile)
				{ // if we click where the selected unit is standing
					CancelSelection();
				}
				else if (this == Unit.SelectedUnit.currAction.startTile)
				{ // if we click where the selected unit started its turn
					CancelSelection();
				}
				else if (Unit.SelectedUnit.reachableTiles.Contains(this))
				{
					// if we click a tile the unit can move to
					Unit.SelectedUnit.MoveToAndAnimate(this);
				}
				else if (TileGrid.Instance.AttackTiles.Contains(this))
				{
					//construct a command to attack target and issue
					Action a = new Action(
						TileGrid.Instance.SelectedTile,
						LazyMove(selectedUnit, Unit),
						this
						);
					selectedUnit.PerformAction(a);
				}
				else if (this.isOccuppied && Unit.CanMove)//If we click on another team unit.
				{
					CancelSelection();
					SelectUnit(this.unit);
				}
				else
				{
					// click elsewhere to deselect
					CancelSelection();
				}
				break;
			case GameState.evaluateAttack:
				GUInterface.Instance.Clicked(this);
				break;
		}
	}

	private void selectEnemy(Unit unit)
	{
		Unit.selectedEnemies.Add(unit);
		Tile.colorEnemiesSelected();
		new MyLittleTurnObserver();
	}

	private void deSelectEnemy(Unit unit)
	{
		Unit.selectedEnemies.Remove(unit);
		UnColourAll();
	}

	private static void colorEnemiesSelected()
	{
		// Debug.Log("Going to color some enemies");
		HashSet<Tile> reachAbleTiles = new HashSet<Tile>();

		foreach (Unit o in Unit.selectedEnemies)
		{
			HashSet<Tile> temp = o.GetReachableTiles();
			temp.Add(o.Tile);
			foreach (Tile t in temp)
			{
				reachAbleTiles.UnionWith(o.AttackInfo.Reach.GetTiles(t));
			}
		}

		foreach (Tile o in reachAbleTiles)
		{
			o.ColourMe(Color.red);
		}

		foreach (Unit o in Unit.selectedEnemies)
		{
			o.Tile.ColourMe(new Color(1f, 0.5f, 0f));
		}
	}

	internal void CancelSelection()
	{
		UnColourAll();
		Unit.Deselect();
		StateManager.Instance.DebugPop();
	}

	public override string ToString()
	{
		return string.Format("[RegularTile: name={0}, unit={1}, isOccuppied={2}, Type={3}, X:{4}, Y:{5}]", name, unit, isOccuppied, Type, transform.position.x, transform.position.y);
	}

	public void ColourMe(Color colour)
	{
		overlay.color = colour;
		overlay.enabled = true;
		colouredTiles.Add(this);
	}

	public void UnColourMe()
	{
		overlay.enabled = false;
	}

	static public HashSet<Tile> colouredTiles = new HashSet<Tile>();
	static public void UnColourAll()
	{
		foreach (Tile t in colouredTiles)
		{
			t.UnColourMe();
		}

		colouredTiles = new HashSet<Tile>();
		colorEnemiesSelected();
	}


	/*
	 * inner class to handle cancel moev button press.
	 */
	private class CancelMove : IGUIButtonListener
	{
		private Tile parent;

		public CancelMove(global::Tile parent)
		{
			this.parent = parent;
		}

		public void ButtonPressed()
		{
			parent.CancelSelection();
		}

		public string ButtonLabel
		{
			get
			{
				return "Cancel";
			}
		}
	}
	/*
	 * inner class to handle wait move.
	 */
	private class ConfirmMove : IGUIButtonListener
	{
		public void ButtonPressed()
		{
			Unit.SelectedUnit.FinnishMovement();
		}

		public string ButtonLabel
		{
			get
			{
				return "Wait";
			}
		}
	}

	private class AbilityUse : IGUIButtonListener
	{

		public Ability ability;

		public AbilityUse(Ability ability)
		{
			this.ability = ability;
		}


		public void ButtonPressed()
		{
			ability.Use();
		}

		public string ButtonLabel
		{
			get
			{
				return ability.Name;
			}
		}
	}

	private class MyLittleTurnObserver : TurnObserver
	{

		static private MyLittleTurnObserver instance;

		public MyLittleTurnObserver()
		{
			if (instance == null)
			{
				instance = this;

				UnitManager.Instance.RegisterTurnObserver(this);
			}

		}

		public void Notify(int turn)
		{
			Unit.selectedEnemies = new HashSet<Unit>();
			UnColourAll();
		}
	}

	public void AddSlow(UnityEngine.Object o)
	{
		_slows.Add(o);
	}

	public void RemoveSlow(UnityEngine.Object o)
	{
		_slows.Remove(o);
	}

	public void SelectUnit(Unit u)
	{
		unit.Select();
		unit.currAction = new Action(this);
		TileGrid.Instance.SelectedTile = this;
		HashSet<Tile> reachable = unit.reachableTiles;
		HashSet<Tile> attackable = new HashSet<Tile>();
		foreach (Tile o in reachable)
		{
			attackable.UnionWith(unit.AttackInfo.GetAttackTiles(unit, o));
		}
		attackable.UnionWith(unit.AttackInfo.GetAttackTiles(unit));
		TileGrid.Instance.MoveTiles = reachable;
		TileGrid.Instance.AttackTiles = attackable;

		StateManager.Instance.DebugPush(GameState.unitSelected);
	}

	/// <summary>
	/// This method have several dependencies, use with care.
	/// really, DONT USE THIS. Like, just don't touch it.
	/// </summary>
	/// <param name="client"></param>
	/// <param name="target"></param>
	/// <returns></returns>
	public Tile LazyMove(Unit client, Unit target)
	{
		// possible attack zones
		HashSet<Tile> possibleMoves = new HashSet<Tile>();
		possibleMoves.UnionWith(client.AttackInfo.Reach.GetTiles(target.Tile)); // attack moves

		if (possibleMoves.Contains(client.Tile)) return client.Tile;

		// intersect with tiles I can move to
		possibleMoves.IntersectWith(TileGrid.Instance.MoveTiles);

		// search for closest
		Tile suggestedMove = null;
		float distance = 999f;
		foreach (Tile t in possibleMoves)
		{
			if (t.isOccuppied) continue; // dont considering occupied squares
			float cd = (client.transform.position - t.transform.position + new Vector3(0f, 0.1f, 0f)).magnitude;
			if (cd < distance)
			{
				suggestedMove = t;
				distance = cd;
			}
		}

		return suggestedMove;
	}
}
