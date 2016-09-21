using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattlePreview : MonoBehaviour {

	Unit target;
	Unit user;

	public Text text;
	public Image box;

	void Start()
	{
		Inactive();
	}

	// Update is called once per frame
	void Update ()
	{
		// figure out if target under mouse has changed
		Tile underMouseTile = TileGrid.Instance.GetTileAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		if (underMouseTile == null) return;
		if (user != Unit.SelectedUnit || target != underMouseTile.Unit)
		{
			user = Unit.SelectedUnit;
			target = underMouseTile.Unit;

			// figure out if target under mouse is a valid target
			if (TileGrid.Instance.AttackTiles.Contains(underMouseTile))
			{
				// we have a valid target!
				// build an attack output
				UpdateStats();

			}
			else
			{
				Inactive();
			}
			
		}
	}

	void UpdateStats()
	{
		if (target == null || user == null) Inactive();
		else
		{
			box.gameObject.SetActive(true);
			int dmg = 1;
			float acc = 1f;
			float crit = 0.05f;

			Stats attacker = user.ModifiedStats;
			Stats defender = target.ModifiedStats;

			dmg = user.AttackInfo.effect.Apply(target.Tile, user, true);
			acc = attacker.HitVersus(defender);
			crit = attacker.CritVersus(defender);

			acc = Mathf.RoundToInt(acc * 100);
			crit = Mathf.RoundToInt(crit * 100);

			text.text = (dmg + "\n" + acc + "\n" + crit);

			print(box.rectTransform.position);
			box.rectTransform.position = Input.mousePosition;
		}
	}

	void Inactive()
	{
		text.text = "0";
		box.gameObject.SetActive(false);
		print("No Bananas!");
	}
}
