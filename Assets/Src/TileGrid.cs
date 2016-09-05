using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGrid : MonoBehaviour, IEnumerable{

	static private TileGrid instance;
	public int maxX = 0;
	public int minX = 0;
	public int maxY = 0;
	public int minY = 0;

	/// <summary>
	/// The maximum size of the playfield, has no effect after stage initializtion.
	/// </summary>
	public int MaxGridSize = 64;
	private int maxSize = 64;
	private Tile[,] tiles;
	private HashSet<Tile> tilesSet;

	/// <summary>
	/// Singleton implementation
	/// </summary>
	/// <value>The instance.</value>
	static public TileGrid Instance{
		get{
			if(instance == null){
				GameObject tempObj = new GameObject("TileGrid");
				tempObj.AddComponent<TileGrid>();
			}
			return instance;
		}
	}

	void Awake(){
		maxSize = MaxGridSize;

		if(instance == null){
			instance = this;
			// put initiation code here!
			tiles = new Tile[maxSize, maxSize];
			tilesSet=new HashSet<Tile>();

		}else{
			Destroy(gameObject);
		}
	}

	void Start(){
		GenerateGrid();
	}

	/// <summary>
	/// Add the specified tile to the tileGrid. Uses a hardcoded max size at the moment. Might be improved upon later?
	/// </summary>
	/// <param name="tile">Tile.</param>
	public bool Add (Tile tile)
	{
		int x = 0;
		int y = 0;
		try {
			x = (int)(tile.transform.position.x);
			y = (int)(tile.transform.position.y);

			// Debug.Log("Adding tile #" + x + ", " + y + ";");
			if(tiles[x+maxSize/2, y+maxSize/2] != null){
				Debug.LogError("Double tiles @ (" + x + "," + y + ")");
				return false;
			}

			tiles[x+maxSize/2, y+maxSize/2] = tile;
		} catch (System.IndexOutOfRangeException e) {
			Debug.LogError (e);
			return false;
		}

		maxX = Mathf.Max(maxX, x);
		minX = Mathf.Min(minX, x);
		maxY = Mathf.Max(maxY, y);
		minY = Mathf.Min(minY, y);
		return tilesSet.Add(tile);
	}

	/// <summary>
	/// Gets the tile at x and y.
	/// </summary>
	/// <returns>The <see cref="RegularTile"/>.</returns>
	/// <param name="x">The x coordinate of wanted tile.</param>
	/// <param name="y">The y coordinate of wanted tile.</param>
	public Tile GetTileAt(int x, int y){
		try{
			if(tiles[x+maxSize/2, y+maxSize/2] == null){
//				Debug.Log("there is no tile to get @" + x + ", " + y);
			}
			return tiles[x+maxSize/2, y+maxSize/2];
		}catch(System.IndexOutOfRangeException e){
			Debug.LogException(e);
			Debug.LogError("coordinates: " + x + "," + y + ";");
			return null;
		}
	}
	/// <summary>
	/// Gets the tile at coordinate.
	/// </summary>
	/// <returns>The <see cref="RegularTile"/>.</returns>
	/// <param name="vector">The coordinates of wanted tile.</param>
	public Tile GetTileAt(Vector2 vector){
		return GetTileAt(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
	}

	public HashSet<Tile> GetOccuppiedTiles(Unit unit)
	{
		HashSet<Unit> units=UnitManager.Instance.GetUnitsByFriendliness(unit);
		HashSet<Tile> retValue=new HashSet<Tile>();
		foreach(Unit u in units)
		{
			retValue.Add(u.Tile);
		}
		return retValue;
	}

	public HashSet<Tile> GetFreeTiles()
	{
		HashSet<Tile> retValue=new HashSet<Tile>();
		foreach(Tile t in this)
		{
			if(t.isOccuppied)
			{

			}
			else if(t is WallTile)
			{

			}
			else if(t is WaterTile)
			{

			}
			else
			{
				retValue.Add(t);
			}
		}
		return retValue;
	}

	public IEnumerator GetEnumerator()
	{
		return tilesSet.GetEnumerator();
	}

	public override string ToString ()
	{
		return string.Format ("[TileGrid: MaxSize={0}, tiles={1}]", maxSize, tiles);
	}

	private void GenerateGrid(){ // possibly make it fancier later, not adding grids close to walls or something.
		GameObject cross = Resources.Load<GameObject>("Cross");
		for(int y = minY-1; y < maxY+1; y++){
			for(int x = minX-1; x < maxX+1; x++){
				Instantiate(cross, new Vector3(x, y, 0), Quaternion.identity);
			}
		}
	}
	/// <summary>
	/// Gets the delta between two objects.
	/// </summary>
	/// <returns>The distance between the objects.</returns>
	static public int GetDelta(Component a, Component b){
		float dx = a.transform.position.x - b.transform.position.x;
		float dy = a.transform.position.y - b.transform.position.y;
		return Mathf.RoundToInt( Mathf.Abs(dx) + Mathf.Abs(dy) );
	}

	public HashSet<Tile> GetTilesAt(Vector2 postion, int radius){
		HashSet<Tile> tiles = new HashSet<Tile>();
		RecursiveAdd(tiles, GetTileAt(postion), radius);
		return tiles;
	}

	private void RecursiveAdd(HashSet<Tile> ret, Tile t, int range){
		if(t == null) return;
		if(t == null) return;
		ret.Add(t);
		range--;
		if(range < 0) return;
		RecursiveAdd(ret, t.North, range);
		RecursiveAdd(ret, t.South, range);
		RecursiveAdd(ret, t.West, range);
		RecursiveAdd(ret, t.East, range);

	}
}
