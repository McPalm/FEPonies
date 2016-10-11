using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterSheet))]
public class BattleSheet : MonoBehaviour {

	static BattleSheet instance;

	public CharacterSheet sheet;

	Unit client;

	public static BattleSheet Instance
	{
		get
		{
			return instance;
		}
	}

	void Awake()
	{
		instance = this;
	}

	// Update is called once per frame
	void Start () {
		sheet.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Cancel")) Close();
	}

	/// <summary>
	/// Opens a stat window with the input character
	/// Or the last charcter that was open in a window.
	/// </summary>
	/// <param name="u">Unit you want detailed stats of.</param>
	public void Open(Unit u = null)
	{
		if (u != null) client = u;
		sheet.Build(client.Character);
		if (StateManager.Instance.State != GameState.characterSheet) StateManager.Instance.Push(GameState.characterSheet);
		sheet.gameObject.SetActive(true);
	}

	/// <summary>
	/// Close the character sheet
	/// Only usable in GameState.characterSheet!
	/// </summary>
	public void Close()
	{
		if (StateManager.Instance.Peek() == GameState.characterSheet)
		{
			sheet.gameObject.SetActive(false);
			StateManager.Instance.Pop();
		}
		else
		{
			throw new System.Exception("Called CharacterSheet.Close() in the wrong state!");
		}

	}
}
