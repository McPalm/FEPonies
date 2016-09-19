using UnityEngine;
using System.Collections.Generic;

public class StartDialogue : EventTarget {

	public string dialogue;
	public List<EventTarget> events;

	public override void Notice ()
	{
		Dialogue.StartDialogue(dialogue, this);
	}

	public void CallBack(){
		foreach(EventTarget et in events){
			try{
				et.Notice();
			}catch(System.Exception e){
				Debug.LogError(e);
				Debug.Log(et);
			}
		}
	}
}
