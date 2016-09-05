using UnityEngine;
using System.Collections;

public class HeartSpawner : MonoBehaviour {

	public Particle particle;

	public float minFrequence = 3f;
	public float maxFrequency = 9f;

	private float _lastParticle = 0f;
	// Use this for initialization
	void Start(){
		if(particle == null){
			particle = Resources.Load<GameObject>("Heart").GetComponent<Particle>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		_lastParticle -= Time.deltaTime;
		if(_lastParticle < 0f){
			Instantiate(particle.gameObject, this.transform.position + new Vector3(0f, 0.4f, 0f), Quaternion.identity);
			_lastParticle = Random.Range(minFrequence, maxFrequency);
		}
	}
}
