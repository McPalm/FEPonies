using UnityEngine;
using System.Collections.Generic;

public class IntermissionDialogue : MonoBehaviour {

	public bool skipSetup = false;
	public List<string> dialogues;

	private int _nextDialogue = -1;
	private bool _enabled = true;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(_enabled){
			if(StateManager.Instance.State == GameState.noState && _nextDialogue < dialogues.Count){
				// count upwards
				_nextDialogue++;
				// start dialogue
				if(_nextDialogue < dialogues.Count){
					string title = dialogues[_nextDialogue];
					if(title == "NextSlide")PowerPointSlide.instance.Advance();
					else if(title == "FadeToBlack")SceneFadeInOut.FadeToBlack();
					else if(title == "FadeToClear")SceneFadeInOut.FadeToClear();
					else Dialogue.StartDialogue(dialogues[_nextDialogue], null);
				}
			}else if(_nextDialogue == dialogues.Count){
				if(skipSetup){
					LevelManager.Instance.StartBattle();
					_enabled = false;
				}else{
					gameObject.AddComponent<IntermissionMenu>();
					_enabled = false;
				}
			}
		}
	}
}
