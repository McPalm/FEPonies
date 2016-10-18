using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterButton : MyButton {

	[SerializeField]
	Image LevelUpTexture;

	/// <summary>
	/// Enable or disable the level up icon
	/// </summary>
	public bool LevelUp
	{
		set
		{
			LevelUpTexture.gameObject.SetActive(value);
		}
		get
		{
			return LevelUpTexture.gameObject.activeSelf;
		}
	}

}
