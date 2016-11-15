using UnityEngine;
using System.Collections;
using System;

public class Switch : MonoBehaviour, IDefenceModifiers
{
	public TileGate[] _gates;

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	void Start()
	{
		Weapon w = new Weapon();
		w.attackInfo = new AttackInfo(new Norange());
		Backpack b = GetComponent<Unit>().Character.Backpack;

		b.Add(w);
		b.Equip(w);
	}

	public void DefenceTest(DamageData dd)
	{
		dd.damageMultipler = 0f;
		if (!dd.testAttack) OpenClose();
	}

	void OpenClose()
	{
		foreach(TileGate g in _gates)
		{
			g.Open = !g.Open;
		}
			
	}
}
