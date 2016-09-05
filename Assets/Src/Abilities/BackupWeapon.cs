using UnityEngine;
using System.Collections;

public class BackupWeapon : Ability {

	private IReach sheathe;
	private IAnimation sAnim;
	private AttackInfo info;

	public override string Name {
		get {
			return "Swap Weapon";
		}
	}

	public override void Use ()
	{
		// uncolour tiles I can attack
		foreach(Tile t in info.GetAttackTiles(Unit.SelectedUnit)){
			t.UnColourMe();
		}
		// swap
		IReach temp = info.reach;
		info.reach = sheathe;
		sheathe = temp;
		IAnimation t2 = info.attackAnimation;
		info.attackAnimation = sAnim;
		sAnim = t2;
		// colour tiles I can attack
		foreach(Tile t in info.GetAttackTiles(Unit.SelectedUnit)){
			t.ColourMe(Color.red);
		}

	}

	// Use this for initialization
	void Start () {
		// figure out whnever or not we are melee or ranged
		info = GetComponentInChildren<AttackInfo>();
		IReach temp = info.reach;
		// create new complementary IReach to swap with
		if(temp is Melee){
			sheathe = gameObject.AddComponent<Ranged>();
			sAnim = gameObject.AddComponent<Arrow>();
		}else{
			sheathe = gameObject.AddComponent<Melee>();
			sAnim = gameObject.AddComponent<Tackle>();
		}
	}
	

}
