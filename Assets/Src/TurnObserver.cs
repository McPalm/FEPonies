/// <summary>
/// Turn observer. Interface used to observe turn changes. Register to UnitManager.Instance.RegisterTurnObserver(TurnObserver e)
/// </summary>
public interface TurnObserver
{
	void Notify(int turn);
}