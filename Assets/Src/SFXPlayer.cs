using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFXPlayer : MonoBehaviour{

	static public SFXPlayer Instance;

	public List<AudioClip> clips;

	void Awake(){
		Instance = this;
	}

	void Start(){

	}

	/// <summary>
	/// Plays a random attack sound
	/// </summary>
	public void AttackSound(){
		GetComponent<AudioSource>().clip = clips[Random.Range(0, 2)];
		GetComponent<AudioSource>().Play();
	}
	/// <summary>
	/// Plays the standard death sfx.
	/// </summary>
	public void DeathSound(){
		GetComponent<AudioSource>().clip = clips[2];
		GetComponent<AudioSource>().Play();
	}

	public void Lightning(){
		GetComponent<AudioSource>().clip = clips[3];
		GetComponent<AudioSource>().Play();
	}

	public void Explosion(){
		GetComponent<AudioSource>().clip = clips[4];
		GetComponent<AudioSource>().Play();
	}

	public void Scratch(){
		GetComponent<AudioSource>().clip = clips[5];
		GetComponent<AudioSource>().Play();
	}

	public void Whoosh(){
		GetComponent<AudioSource>().clip = clips[6];
		GetComponent<AudioSource>().Play();
	}

	public void Shock(){
		GetComponent<AudioSource>().clip = clips[7];
		GetComponent<AudioSource>().Play();
	}
}