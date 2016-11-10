using UnityEngine;
using System.Collections.Generic;
using System;

public class Shove : Ability, SustainedAbility, IAttackModifier
{
	bool _active = true;

	public bool Active
	{
		get
		{
			return _active;
		}
	}

	public override string Name
	{
		get
		{
			return "Shove";
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
		if(_active && UnitManager.Instance.IsItMyTurn(dd.source))
		{
			dd.RegisterCallback(OnHit);
		}
	}

	public override void Use()
	{
		_active = !_active;
	}

	public void OnHit(DamageData dd)
	{
		Tile moveTo = dd.target.Tile;
		new Mydebuff(dd.target.Character);
		//if we can enter the tile
		if(!(moveTo is WaterTile) &! dd.source.Character.Flight)
		{
			// and we can push the target
			if(Push.SmartStaticApply(moveTo, dd.SourceTile))
			{
				dd.source.AttackInfo.AttackAnimation.Cancel();
				dd.source.MoveTo(moveTo);
			}
		}
	}

	class Mydebuff : Buff, TurnObserver
	{
		Stats _stats;
		int endTurn = 0;
		GameState endPhase = 0;
		Character target;

		public Stats Stats
		{
			get
			{
				return _stats;
			}
		}

		public Mydebuff(Character target)
		{
			this.target = target;
			_stats = new Stats();
			_stats.movement.moveSpeed = -1;
			target.AddBuff(this);
			endTurn = UnitManager.Instance.currTurn + 1;
			endPhase = StateManager.Instance.Turn;
			UnitManager.Instance.RegisterTurnObserver(this);
		}

		public void Notify(int turn)
		{
			if(turn == endTurn && StateManager.Instance.Turn == endPhase)
			{
				target.RemoveBuff(this);
				UnitManager.Instance.unRegisterTurnObserver(this);
			}
		}
	}
}