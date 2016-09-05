using UnityEngine;
using System.Collections;

public class MousePosition{

	/// <summary>
	/// Gets the current mouse position on the scene.
	/// </summary>
	public static Vector3 Get(){
		Vector3 poz = Input.mousePosition;
		//poz = new Vector3(poz.x/Camera.main.pixelWidth, poz.y/Camera.main.pixelHeight, 0);
		return Camera.main.ScreenToWorldPoint(poz);
	}

	public static Tile GetTile()
	{
		Vector3 pos=Get();
		return TileGrid.Instance.GetTileAt(pos);
	}
}