using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpriteLibrary : MonoBehaviour
{

	static SpriteLibrary instance;

	[SerializeField]
	List<SpriteBook> library;

	static SpriteLibrary Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Resources.Load<SpriteLibrary>("Sprites");
			}
			return instance;
		}
	}

	public static Sprite GetIcon(string name)
	{
		return Instance.GetInstanceIcon(name);
	}

	Sprite GetInstanceIcon(string name)
	{
		foreach (SpriteBook book in library)
			if (book.name == name) return book.sprite;
		Debug.LogWarning(name + " does not exsist in Sprite Library!");
		return library[0].sprite;
	}

	[System.Serializable]
	private class SpriteBook
	{
#pragma warning disable
		public string name;
#pragma warning disable
		public Sprite sprite;
	}
}
