/// <summary>
/// IGUI button listener.
/// Used together with the GUInterface.Instanace.ShowButtonMenu() method.
/// </summary>
public interface IGUIButtonListener{

	string ButtonLabel{
		get;
	}

	void ButtonPressed();
}
