using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour {

	static ToolTip _instance;

	[SerializeField]
	Text text;

	// Use this for initialization
	void Awake () {
		_instance = this;
		Hide();
	}

	static public void Set(string text)
	{
		_instance.gameObject.SetActive(true);
		_instance.text.text = text;
		_instance.transform.position = Input.mousePosition + new Vector3(40f, 80f);
	}

	static public void Hide()
	{
		_instance.gameObject.SetActive(false);
	}
}