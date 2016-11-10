using UnityEngine;
using System.Collections.Generic;
using System;

public class Pin : Passive, IAttackModifier
{
	Shove shove;

	public override string Name
	{
		get
		{
			return "Pin";
		}
	}

	public int Priority
	{
		get
		{
			return 0;
		}
	}

	public void Test(DamageData dd)
	{
		if(shove && shove.Active)
			dd.RegisterCallback(OnHit);
	}

	void OnHit(DamageData dd)
	{
		dd.target.gameObject.AddComponent<PinDebuff>();
	}

	void Start()
	{
		shove = GetComponent<Shove>();
	}


	private class PinDebuff : MonoBehaviour, TurnObserver, IDefenceModifiers
	{
		public int Priority
		{
			get
			{
				return 0;
			}
		}

		public void DefenceTest(DamageData dd)
		{
			dd.damageMultipler *= 1.2f;
		}

		public void Notify(int turn)
		{
			UnitManager.Instance.unRegisterTurnObserver(this);
			Destroy(this);
		}

		void Awake()
		{
			UnitManager.Instance.RegisterTurnObserver(this);
		}
	}
}