﻿using UnityEngine;
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

	public float yAdjust = 1f;

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
			if (StateManager.Instance.State == GameState.unitSelected)
			{


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
			int hp = 1;
			float acc = 1f;
			float crit = 0.05f;

			// get attack tile interaction thingy
			Tile moveto = target.Tile.LazyMove(user, target);

			Stats attacker = user.GetStatsAt(moveto, target);
			Stats defender = target.GetStatsAt(target.Tile, user, moveto);

			// attack stats
			DamageData dd = new DamageData();
			dd.target = target;
			dd.source = user;
			dd.testAttack = true;
			dd.baseDamage = attacker.might + attacker.strength;
			dd.SourceTile = moveto;

			user.AttackInfo.effect.Apply(dd);

			dmg = dd.FinalDamage;
			hp = defender.maxHP - target.damageTaken;
			acc = attacker.HitVersus(defender);
			crit = attacker.CritVersus(defender);

			acc = Mathf.RoundToInt(acc * 100);
			crit = Mathf.RoundToInt(crit * 100);

			text.text = (dmg + "\n" + acc + "\n" + crit);

			willRetaliate = target.RetaliationsLeft > 0 && target.AttackInfo.reach.GetTiles(target.Tile).Contains(moveto);

			// defence stats
			if (willRetaliate)
			{
				dd = new DamageData();
				dd.target = target;
				dd.source = user;
				dd.testAttack = true;
				dd.baseDamage = attacker.might + attacker.strength;
				dd.SourceTile = moveto;

				target.AttackInfo.effect.Apply(dd);
				dmg = dmg = dd.FinalDamage;
				acc = attacker.HitVersus(attacker);
				crit = attacker.CritVersus(attacker);
				hp = attacker.maxHP - user.damageTaken;

				acc = Mathf.RoundToInt(acc * 100);
				crit = Mathf.RoundToInt(crit * 100);

				retaliationText.text = (dmg + "\n" + acc + "\n" + crit);
			}

			Vector2 location = Camera.main.WorldToScreenPoint(target.transform.position);
			Vector2 sourceDirection = (user.transform.position - target.transform.position).normalized;

			box.rectTransform.position = location + sourceDirection*60f;
			retaliationBox.rectTransform.position = location + sourceDirection * 160f * (1f + Mathf.Abs(sourceDirection.y) * yAdjust);

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
