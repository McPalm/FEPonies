using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Random sprite.
/// Sometimes replace the sprite on the spriterendered with another random sprite from a list
/// </summary>
public class RandomSprite : MonoBehaviour {

	public List<Sprite> pool;


	// Use this for initialization
	void Start () {
		int num = Random.Range(0, pool.Count+1);

		if(num < pool.Count){
			GetComponent<SpriteRenderer>().sprite = pool[num];
		}
	}

}
