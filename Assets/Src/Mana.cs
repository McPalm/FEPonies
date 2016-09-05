using UnityEngine;
using System.Collections.Generic;

public class Mana : MonoBehaviour{

	public int maxMana = 0;
	private int _manaSpent = 0;
	
	public Mana (int maxMana)
	{
		this.maxMana = maxMana;
	}

	void Start(){
		ManaBar.Create(transform);
	}
	
	public int MaxMana{
		set{
			maxMana = Mathf.Max(0, value);
		}
		get{return maxMana;}
	}
	
	public int ManaRemaining{
		get{return maxMana - _manaSpent;}
		set{_manaSpent = Mathf.Min (maxMana, Mathf.Max(0, maxMana - value));}
	}
	
	public bool CanCast(int manacost){
		return manacost + _manaSpent <= maxMana;
	}

	void UnitDeath(){
		_manaSpent = maxMana;
	}
}
