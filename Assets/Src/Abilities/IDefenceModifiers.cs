using System.Collections.Generic;

/// <summary>
/// Implement this on a component and attach on a unit to run every hit it takes through here
/// Uses IComparer to determine priority, higher priority moves goes first.
/// </summary>
public interface IDefenceModifiers
{

	/// <summary>
	/// Higher priority moves goes first
	/// </summary>
	int Priority
	{
		get;
	}

	void Test(DamageData dd);

}