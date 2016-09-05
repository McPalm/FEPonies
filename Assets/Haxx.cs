using UnityEngine;
using System.Collections;

public class Haxx : MonoBehaviour {

	public Unit iAttack;
	public Tile attackTarget;


	// Use this for initialization
	void Start () {
		//StateManager.Instance.State = GameState.playerTurn;
		//
		//GUInterface.Instance.PrintMessage("Player Turn!");

		/*
		DamageType d = new DamageType();
		Debug.Log("expect false: " + d.AntiAir);
		Debug.Log("expect false: " + d.ArmourPiercing);
		Debug.Log("expect false: " + d.MageSlayer);
		Debug.Log("expect true: " + d.Normal);

		d.ArmourPiercing = true;
		d.ArmourPiercing = true;
		d.ArmourPiercing = true;
		d.AntiAir = false;

		Debug.Log("expect false: " + d.AntiAir);
		Debug.Log("expect true: " + d.ArmourPiercing);
		Debug.Log("expect false: " + d.MageSlayer);
		Debug.Log("expect false: " + d.Normal);

		d.AntiAir = true;
		d.ArmourPiercing = false;
		d.MageSlayer = true;

		Debug.Log("expect true: " + d.AntiAir);
		Debug.Log("expect false: " + d.ArmourPiercing);
		Debug.Log("expect true: " + d.MageSlayer);
		Debug.Log("expect false: " + d.Normal);

		d.AntiAir = false;
		d.MageSlayer = false;

		Debug.Log("expect true: " + d.Normal);
		*/

		/*
		foreach(Sprite s in Resources.LoadAll<Sprite>("portraits/")){
			Debug.Log(s.name);
		}
		*/
	}

	void Update () {
		// if were on enemy turn, end enemy turn.
		/*if(StateManager.Instance.State == GameState.allyTurn){
			StateManager.Instance.State = GameState.playerTurn;
		}*/
		//if(Input.anyKeyDown){
		//	Debug.Log("The Any Key?");
		//	iAttack.AnimateAttack(attackTarget);
		//}
		//if(History.Instance.Count > 0){
		//	Debug.Log(History.Instance[History.Instance.Count-1]);
		//}

	}
}

