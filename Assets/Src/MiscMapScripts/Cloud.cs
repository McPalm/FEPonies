using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {


	public int distance = 10;
	public float minSpeed;
	public float maxSpeed;
	public float yVariance = 3f;
	public Sprite[] cloudSprites;

	float _speed = 0f;
	Vector3 _startPoint;
	static int lastCloud = 0;

	float RespawnLimit
	{
		get { return _startPoint.x - distance; }
	}

	bool IShouldRespawn
	{
		get { return transform.position.x < RespawnLimit; }
	}

	// Use this for initialization
	void Start () {
		_startPoint = transform.position;
		Spawn();
		transform.position = transform.position - new Vector3(Random.Range(0f, distance), 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + new Vector3(-_speed * Time.deltaTime, 0f, 0f);
		if (IShouldRespawn) Spawn();
	}

	void Spawn()
	{
		_speed = Random.Range(minSpeed, maxSpeed);
		transform.position = _startPoint +  new Vector3(0f, Random.Range(0f, yVariance));
		ShuffleSprite();
	}

	void ShuffleSprite()
	{
		lastCloud += Random.Range(0, 3);
		lastCloud %= cloudSprites.Length;
		GetComponent<SpriteRenderer>().sprite = cloudSprites[lastCloud];
	}
}
