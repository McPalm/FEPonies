using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = "Particles";
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
	}
}
