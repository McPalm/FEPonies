using UnityEngine;
using System.Collections.Generic;

public class MainMenuRandomSprite : MonoBehaviour {

	public List<Sprite> sprites;

	public SpriteRenderer sr1;
	public SpriteRenderer sr2;

	// Use this for initialization
	void Awake () {

		int max = sprites.Count;

		int a = Random.Range(0, max);
		int b = a + Random.Range(0, max-1);

		sr1.sprite = sprites[a];
		sr2.sprite = sprites[b];
	}



}