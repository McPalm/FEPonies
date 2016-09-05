using UnityEngine;
using System.Collections.Generic;

public class ParticleSpawner : MonoBehaviour {

	public GameObject particle;
	[Range(1, 10)]
	public int radius = 1;
	[Range(0.1f, 100f)]
	public float particlesPerSecondPerTile = 1f;

	[Range(0.01f, 1f)]
	public float chance = 1f;

	public Vector3 offset;

	private HashSet<Tile> tiles;
	private float _timer;


	// Use this for initialization
	void Start () {
		tiles = TileGrid.Instance.GetTilesAt(transform.position, 1);
	}
	
	// Update is called once per frame
	void Update () {
		_timer-= Time.deltaTime*particlesPerSecondPerTile;
		while(_timer < 0){
			_timer += 1f;
			foreach(Tile t in tiles) SpawnParticle(t);
		}
	}

	void SpawnParticle(Tile t){
		if(Random.Range(0f, 1f) > chance) return;
		Vector3 random = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
		Instantiate(particle, t.transform.position + offset + random, Quaternion.identity);
	}

}
