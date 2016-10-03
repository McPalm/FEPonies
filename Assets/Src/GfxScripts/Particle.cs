using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	public bool sinWave = true;

	public bool increasingSinWave = false;

	private Vector3 start;
	private Vector3 end;
	private float timer = 0f;
	private float scrambleX;


	static public void HealParticle(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("GreenCross"), spawnPosition, Quaternion.identity);
		Instantiate(Resources.Load<GameObject>("GreenCross"), spawnPosition + new Vector3(0.3f, -0.1f, 0f), Quaternion.identity);
		Instantiate(Resources.Load<GameObject>("GreenCross"), spawnPosition + new Vector3(-0.3f, -0.1f, 0f), Quaternion.identity);
	}

	static public void PoisonParticle(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("PoisonSkull"), spawnPosition, Quaternion.identity);
	}

	static public void NoteParticle(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("Note"), spawnPosition, Quaternion.identity);
	}

	static public void Clave(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("clave-de-sol-hi"), spawnPosition, Quaternion.identity);
	}

	static public void LevelUp(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("LevelUp"), spawnPosition, Quaternion.identity);
	}

	static public void Crit(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("Crit"), spawnPosition, Quaternion.identity);
	}

	static public void Dodge(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("Dodge"), spawnPosition, Quaternion.identity);
	}

	static public void Mana(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("Mana"), spawnPosition, Quaternion.identity);
	}

	static public void ExlamationPoint(Vector3 spawnPosition){
		Destroy(Instantiate(Resources.Load<GameObject>("Exlamationpoint"), spawnPosition, Quaternion.identity), 0.7f);
	}

	static public void BastionShield(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("BastionShield"), spawnPosition, Quaternion.identity);
	}

	static public void BrokenShield(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("BrokenShield"), spawnPosition, Quaternion.identity);
	}

	static public void GravityParticle(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("GravityParticle"), spawnPosition + new Vector3(0, 1, 0), Quaternion.identity);
	}

	static public void Star(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("Star"), spawnPosition, Quaternion.identity);
	}
	static public void Backstab(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("Backstab"), spawnPosition, Quaternion.identity);
	}
	static public void Block(Vector3 spawnPosition)
	{
		Instantiate(Resources.Load<GameObject>("Block"), spawnPosition, Quaternion.identity);
	}

	// Use this for initialization
	void Start () {
		start = transform.position;
		end = start + new Vector3(0, 1, 0);
		scrambleX = Random.Range(0f, Mathf.PI*2);
	}
	
	// Update is called once per frame
	void Update () {

		float offsetX = (sinWave) ? Mathf.Sin(timer*Mathf.PI*4+scrambleX) : 0f;
		if(increasingSinWave) offsetX *= timer;

		transform.position = start * (1f-timer) + end * timer + new Vector3(offsetX/6f, 0, 0);
		if(timer > 1f){
			Destroy(gameObject);
		}
		timer += Time.deltaTime;
	}

	static public void SparkleBoom(Vector3 spawnPosition){
		Instantiate(Resources.Load<GameObject>("TwilightSplotion"), spawnPosition, Quaternion.identity);
	}
}
