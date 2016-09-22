using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattlePreview : MonoBehaviour {

	Unit target;
	Unit user;
	bool willRetaliate;

	public Text text;
	public Image box;
	public Text retaliationText;
	public Image retaliationBox;

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
			if (TileGrid.Instance.AttackTiles != null && TileGrid.Instance.AttackTiles.Contains(underMouseTile))
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
			int dmg = 1;
			float acc = 1f;
			float crit = 0.05f;

			Stats attacker = user.ModifiedStats;
			Stats defender = target.ModifiedStats;

			// attack stats
			dmg = user.AttackInfo.effect.Apply(target.Tile, user, true);
			acc = attacker.HitVersus(defender);
			crit = attacker.CritVersus(defender);

			acc = Mathf.RoundToInt(acc * 100);
			crit = Mathf.RoundToInt(crit * 100);

			text.text = (dmg + "\n" + acc + "\n" + crit);

			willRetaliate = target.RetaliationsLeft > 0;

			// defence stats
			if (willRetaliate)
			{
				dmg = target.AttackInfo.effect.Apply(user.Tile, target, true);
				acc = attacker.HitVersus(attacker);
				crit = attacker.CritVersus(attacker);

				acc = Mathf.RoundToInt(acc * 100);
				crit = Mathf.RoundToInt(crit * 100);

				retaliationText.text = (dmg + "\n" + acc + "\n" + crit);
			}

			Vector2 location = Camera.main.WorldToScreenPoint(target.transform.position);
			Vector2 sourceDirection = (user.transform.position - target.transform.position).normalized;

			box.rectTransform.position = location + sourceDirection*60f;
			retaliationBox.rectTransform.position = location + sourceDirection * 160f;

			box.gameObject.SetActive(true);
			retaliationBox.gameObject.SetActive(willRetaliate);
		}
	}

	void Inactive()
	{
		text.text = "0";
		box.gameObject.SetActive(false);
		retaliationBox.gameObject.SetActive(false);
	}
}
