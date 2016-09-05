using UnityEngine;
using System.Collections;
[System.Serializable]
public struct UnitMove {
	public int moveSpeed;
	public MoveType moveType;

	public UnitMove (int moveSpeed, MoveType moveType = MoveType.walking)
	{
		this.moveSpeed = System.Math.Min(10, moveSpeed);
		this.moveType = moveType;
	}

	// overload operator +
	public static UnitMove operator +(UnitMove a, UnitMove b){

		if(b.moveType == MoveType.flying){
			return new UnitMove(a.moveSpeed + b.moveSpeed, MoveType.flying);
		}
		return new UnitMove(a.moveSpeed + b.moveSpeed, a.moveType);
	}
}
