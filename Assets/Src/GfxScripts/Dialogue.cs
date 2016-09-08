using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {

	// window positon constants
	private int HEIGHT;
	private const int PADDING = 5;
	private int WIDTH;
	private const int FRAME = 40;

	public Texture box;

	private List<Window> windows;
	private int currentWindow = 0;
	private StartDialogue callback;
	private float _timeSinceLastDialogue = 0f;

	private Texture2D leftPortrait = null;
	private Texture2D rightPortrait = null ; //Resources.Load<Texture2D>("Portraits/DawnHappy");

	public static Dialogue StartDialogue(string dialogue, StartDialogue callback){
		StateManager.Instance.DebugPush(GameState.dialogue);
		GameObject go = new GameObject("Dialogue Box");
		Dialogue d = go.AddComponent<Dialogue>();
		d.Parse(dialogue);
		d.callback = callback;
		d.box = Resources.Load<Texture>("TextBox");
		d.HEIGHT = d.box.height;
		d.WIDTH = d.box.width;

		return d;
	}

	void OnGUI(){
		if(StateManager.Instance.Contains(GameState.dialogue)){

			// portraits
			if(leftPortrait) GUI.DrawTexture(new Rect(Screen.width/2-WIDTH/2-100, Screen.height-PADDING-HEIGHT-leftPortrait.height+20, leftPortrait.width, leftPortrait.height), leftPortrait);
			if (rightPortrait) GUI.DrawTexture(new Rect(Screen.width/2+WIDTH/2+100, Screen.height-PADDING-HEIGHT-rightPortrait.height+20, -rightPortrait.width, rightPortrait.height), rightPortrait);

			// name labels
			if(windows[currentWindow].name.Length != 0) GUI.Box(new Rect(Screen.width/2-WIDTH/2, Screen.height-PADDING-HEIGHT-24, 200, 25), windows[currentWindow].name);
			if(windows[currentWindow].rightname.Length != 0) GUI.Box(new Rect(Screen.width/2+WIDTH/2-200, Screen.height-PADDING-HEIGHT-24, 200, 25), windows[currentWindow].rightname);

			// text box
			GUI.DrawTexture(new Rect(Screen.width/2-WIDTH/2, Screen.height-PADDING-HEIGHT, WIDTH, HEIGHT), box);
			// text
			GUI.Label(new Rect(Screen.width/2-WIDTH/2+FRAME, Screen.height-PADDING-HEIGHT+FRAME, WIDTH-FRAME*2, HEIGHT-FRAME*2), windows[currentWindow].body, StyleDB.Instance.style);
		}
	}

	void Update(){
		if(StateManager.Instance.State == GameState.dialogue && Input.GetMouseButtonDown(0) && _timeSinceLastDialogue > 0.15f){
			currentWindow++;


			_timeSinceLastDialogue = 0f;
			if(currentWindow == windows.Count){
				StateManager.Instance.DebugPop();
				Destroy(gameObject);
				if(callback != null) callback.CallBack();
			}else{
				if(windows[currentWindow].left) leftPortrait = windows[currentWindow].left;
				if(windows[currentWindow].right) rightPortrait = windows[currentWindow].right;
			}
		}
		if(StateManager.Instance.State == GameState.dialogue){
			_timeSinceLastDialogue += Time.deltaTime;
		}
	}


	private void Parse(string dialogue){
		// create new windows list
		windows = new List<Window>();
		// find dialogue in text document
		TextAsset ta = Resources.Load<TextAsset>("Dialogues");
		for(int i = 0; i < ta.text.Length; i++){
			string title = "";
			if(ta.text[i] == '§'){
				title = getWord(ta.text, i+1);
			}
			if(title.Length != 0)
            {
                if (title.ToUpper().Equals(dialogue.ToUpper())){
					ParseParagraph(ta.text, i);
				}
			}
		}
		// default state
		if(windows.Count == 0){
			windows.Add(new Window("<Missing text>"));
		}else{
			leftPortrait = windows[0].left;
			rightPortrait = windows[0].right;
		}
	}

	private string getWord(string s, int start){
		for(int i = start; i < s.Length; i++){
			if(s[i] == '\n'|| s[i] == '\r')
            {
				return s.Substring(start, i-start);
			}
		}
		return s.Substring(start);
	}

	private void ParseParagraph(string ta, int start){
		int startBody = -2;
		int bodyLength = -1;
		bool endOfAll = false;

		Window w = null;

		for(int i = start; !endOfAll; i++){
			// find the first @, that is our name.
			// grab all the text between the new row and the next @ or > or end of document
			if(!(i < ta.Length)){
				w.body = ta.Substring(startBody);
				
				break;
			}

			if(ta[i] == '@' && startBody == -2){
				startBody = -1; // we are looking for the next new row
				w = new Window();
				w.AddName(getWord(ta, i+1));
				windows.Add(w);
			}else if(ta[i] == '#'){ // portrait Assignment
				w.AddPortrait(getWord(ta, i+1));
				startBody = -1;
			}else if(ta[i] == '\n' && startBody == -1){ // new row after the @
				startBody = i+1; // body will start on the next row.
			}else if(startBody >= 0){ // we are looking for the end of our current body
				if(ta[i] == '§'){
					endOfAll = true;
					bodyLength = i-startBody-2;
					w.body = ta.Substring(startBody, bodyLength+1);
				}else if(ta[i] == '@'){
					bodyLength = i-startBody-2;
					w.body = ta.Substring(startBody, bodyLength+1);
					w = new Window(getWord(ta, i+1));
					w.AddName(getWord(ta, i+1));
					windows.Add(w);
					startBody = -1;
					bodyLength = -1;
				}
			}
		}
	}

	private class Window{
		public string name = "";
		public string rightname = "";
		public string body = "";

		public Texture2D left;
		public Texture2D right;

		public Window (string body = "")
		{
			this.body = body;
		}

		public void AddPortrait(string name){
			if(name[0] == '/'){
				right =  Portraits.GetPortrait(name.Substring(1)); //Resources.Load<Texture2D>("portraits/" + name.Substring(1));
			}else{
				left =  Portraits.GetPortrait(name); ; //Resources.Load<Texture2D>("portraits/" + name);
			}
		}

		public void AddName(string name){
			if(name == "none"){
				// do nothing
			}else if(name[0] == '/'){
				rightname =  name.Substring(1);
			}else{
				this.name = name;
			}
		}
	}
}
