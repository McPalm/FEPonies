using UnityEngine;
using System.Collections;

public class TargetAlly : MonoBehaviour, ITargetFilter {

	public bool ValidateTarget (Tile target, Unit user)
	{
		if(target.Unit == null){
			return false;
		}

		if(user == null){
			Debug.LogError("Target Ally needs a user!" +
			               "\nTarget Tile=" + target +
			               "\nTarget Unit" + target.Unit);
			return true;
		}
		return (!user.isHostile(target.Unit));

	}
}
