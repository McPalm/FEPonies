/// <summary>
/// Use this interface on abilites that are sustainer or have a duration
/// Used by the GUI to know whenever ot not to display ab ability being active.
/// </summary>
public interface SustainedAbility {

	bool Active { get; }

}