using UnityEngine;
using UnityEngine.UI;
using System;

public class MouseoverStats : MonoBehaviour {

	private Tile tileUnderMouse;

	public Image box;
	public Text text;
	public Image attackIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (StateManager.Instance.State == GameState.playerTurn)
		{
			Vector2 mpos = Input.mousePosition;
			Tile t = TileGrid.Instance.GetTileAt(Camera.main.ScreenToWorldPoint(mpos));
			if (t != tileUnderMouse)
			{
				// we got a new tile!
				tileUnderMouse = t;
				if (t != null && t.isOccuppied)
				{
					BuildStats();
					MoveToTile();
				}
				else
				{
					HideMe();
				}

			}
		}
		else
		{
			HideMe();
		}
	}

	private void MoveToTile()
	{
		// figure out tiles location on the screen.
		Vector2 origin = Camera.main.WorldToScreenPoint(tileUnderMouse.transform.position);

		// ofset towards the centre of the screen.
		Vector2 middle = GetComponent<RectTransform>().position;
		origin += new Vector2((origin.x > middle.x) ? -100f : 100f, 0);

		print(origin.y);
		print(box.rectTransform.sizeDelta.y / 2);
		print(GetComponent<RectTransform>().position.y * 2);


		// offset from borders
		float sizeY = box.rectTransform.sizeDelta.y * box.rectTransform.localScale.y;
		if (origin.y - sizeY/2 < 0)
		{
			print("Below?");
			origin = new Vector2(origin.x, sizeY / 2);
		}
		else if (origin.y + sizeY / 2 > middle.y * 2)
		{
			print("Above?");
			origin = new Vector2(origin.x, middle.y * 2 - sizeY / 2);
		}
		//print(box.rectTransform.sizeDelta);


		// move there
		box.rectTransform.position = origin;
	}

	private void HideMe()
	{
		box.gameObject.SetActive(false);
	}

	private void BuildStats()
	{
		Unit client = tileUnderMouse.Unit;
		Stats s = client.GetStatsAt(client.Tile);
		int hpleft = client.CurrentHP;

		text.text = "HP " + client.CurrentHP +  "/" + s.maxHP + "\n" +
			"DMG " + s.might + s.strength + "\n" +
			"Def " + s.defense + "\n" +
			"Res " + s.resistance + "\n";

		box.gameObject.SetActive(true);
	}
}
