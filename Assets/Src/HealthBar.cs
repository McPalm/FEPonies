using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Unit represents;
	public Transform healthBar;

	private SpriteRenderer frame;
	private int lastDamage = 99;

	void Start (){
		represents = transform.parent.GetComponent<Unit>();
		frame = GetComponent<SpriteRenderer>();

		if(represents.team == 1)
			healthBar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("HealthBarYellow");
		if(represents.team == 2)
			healthBar.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("HealthBarGreen");
	}

	// Update is called once per frame
	void Update () {
		if (represents.damageTaken != lastDamage)
		{
			lastDamage = represents.damageTaken;

			if (represents.invisible || represents.CurrentHP < 1)
			{
				healthBar.gameObject.SetActive(false);
				frame.enabled = false;
			}
			else
			{
				healthBar.gameObject.SetActive(true);
				frame.enabled = true;

				float scale = Mathf.Max((float)(represents.CurrentHP) / (float)represents.Character.ModifiedStats.maxHP, 0f);
				healthBar.transform.localScale = new Vector3(scale, 1f, 1f);
			}
			FaceRight = represents.FaceRight;
		}
	}
	/// <summary>
	/// Creates a new healthbar attached to a partent GameObject. Will not function if the parent does not have a Unit component! But if the game object have a Unit component, then the healthbar should work autonomiously.
	/// </summary>
	/// <param name="parent">Parent GameObjects transform component.</param>
	static public void NewHealthBar(Transform parent){
		GameObject go = Resources.Load<GameObject>("HealthFrame");
		go = Instantiate(go) as GameObject;
		go.transform.parent = parent;
		go.transform.localPosition = new Vector3(-0.5f, -0.5f, 0f);
	}

	public bool FaceRight{
		set{if (value){
				transform.localPosition = new Vector3(-0.5f, -0.5f, 0f);
				transform.localScale = new Vector3(1, 1, 1);
			}else{
				transform.localPosition = new Vector3(0.5f, -0.5f, 0f);
				transform.localScale = new Vector3(-1, 1, 1);
			}
		}
	}
}
