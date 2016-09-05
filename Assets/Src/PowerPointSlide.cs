using UnityEngine;
using System.Collections.Generic;

public class PowerPointSlide : MonoBehaviour {

	static public PowerPointSlide instance;
	
	public List<Sprite> slides;

	void Awake(){
		instance = this;
	}

	public void Advance(){
		if(slides.Count != 0){
			gameObject.GetComponent<SpriteRenderer>().sprite = slides[0];
			slides.RemoveAt(0);
		}else{
			Debug.LogWarning("Cant advance when out of slides!");
		}
	}


}