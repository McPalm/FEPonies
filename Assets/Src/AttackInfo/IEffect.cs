using UnityEngine;
using System.Collections;

public interface IEffect{



	/// <summary>
	/// 
	/// </summary>
	/// <param name="attackData">Attack Data</param>
	/// <returns>A heristic for the attack for AI usage</returns>
	int Apply(DamageData attackData);
}
