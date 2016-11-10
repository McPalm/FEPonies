using UnityEngine;
using System.Collections.Generic;
using System;

public class DirtyUpdate : MonoBehaviour {

	static DirtyUpdate _instance;

	List<System.Action> _update;
	List<System.Action> _removeUpdate;

	static public DirtyUpdate Instance
	{
		get
		{
			if(_instance == null)
			{
				new GameObject().AddComponent<DirtyUpdate>();
			}
			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		_instance = this;
		name = "DirtyUpdate";
		_update = new List<System.Action>();
		_removeUpdate = new List<System.Action>();
	}

	public void RegisterUpdate(System.Action a)
	{
		_update.Add(a);
	}

	public void UnregisterUpdate(System.Action a)
	{
		_removeUpdate.Add(a);
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (System.Action a in _update)
		{
			a();
		}
		foreach(System.Action a  in _removeUpdate)
		{
			_update.Remove(a);
		}
		_removeUpdate.Clear();
	}

    void OnDestroy()
    {
        _instance = null;
    }
}