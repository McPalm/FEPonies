using UnityEngine;
using System.Collections;

public class AutoLerp : MonoBehaviour {

	RectTransform rt;
	Vector3 target;
	bool lerping = false;

	// Update is called once per frame
	void FixedUpdate () {
		if (lerping)
		{
			rt.position = rt.position * 0.80f + target * 0.2f;
			if ((rt.position - target).magnitude < 1f)
			{
				lerping = false;
				rt.position = target;
			}
		}
	}

	void Awake()
	{
		rt = GetComponent<RectTransform>();
		target = rt.position;
	}

	public void Lerp(Vector3 destination)
	{
		target = destination;
		lerping = true;
	}
}
