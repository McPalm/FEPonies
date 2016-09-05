using UnityEngine;
using System.Collections;
/// <summary>
/// Inspire.
/// Passive skill that heals neraby allies when dealing damage equal to one third of the damage dealt.
/// </summary>
public class Inspire : Passive {

	public override string Name {
		get {
			return "Inspire";
		}
	}
	
	public void OnDamageDealt(object value){


		int i = (int)value;
		i = (i+1)/3;
		if(i < 0){
			Unit h = GetComponent<Unit>();
			Tile t = h.Tile;
			if(t.North && t.North.isOccuppied && !t.North.Unit.isHostile(h)) t.North.Unit.Heal(i);
			if(t.East && t.East.isOccuppied && !t.East.Unit.isHostile(h)) t.East.Unit.Heal(i);
			if(t.South && t.South.isOccuppied && !t.South.Unit.isHostile(h)) t.South.Unit.Heal(i);
			if(t.West && t.West.isOccuppied && !t.West.Unit.isHostile(h)) t.West.Unit.Heal(i);
		}
	}
}
