using UnityEngine;
using System.Collections;

public class GUInterface : MonoBehaviour {

	static private GUInterface instance;

	static public GUInterface Instance{
		get{
			if(instance == null){
				GameObject tempObj = new GameObject("Announcer");
				tempObj.AddComponent<GUInterface>();
			}
			return instance;
		}
	}

	// fields
	private string text;
	private float duration;
	private IGUIButtonListener[] buttons;
	private Vector2 buttonAnchor;
	private int _buttonMenuButtonSize = 32;
	private Unit attackTarget;
	private Texture _shield = Resources.Load<Texture>("Shield");
	private Texture _swords  = Resources.Load<Texture>("Swords");
	private Texture _resistance = Resources.Load<Texture>("Resistance");
	private Texture _healthBar = Resources.Load<Texture>("HealthBar");
	private Texture _healthBarRed = Resources.Load<Texture>("HealthBarRed");
	private Texture _healthFrame = Resources.Load<Texture>("HealthFrame");

	void Awake(){
		instance = this;
		Pauser.Instance.UnPause();
	}
	/// <summary>
	/// Prints a message in the center of the screen for 1 + 0.1 seconds for every letter in the text.
	/// </summary>
	/// <param name="text">Text.</param>
	public void PrintMessage(string text)
	{
		this.text = text;
		duration = 1f+0.1f*text.Length;
	}

	/// <summary>
	/// Asks for coinfirmation if the player wish to go through with an attack.
	/// This assumes that its teh selected unit that is being evaluated.
	/// Will enter an EvaluateAttack state. And will leave it either due to cancelling or confirming the attack.
	/// </summary>
	/// <param name="target">Target that might be attacked.</param>
	public void EvaluateAttack(Unit target){
		// enter state
		StateManager.Instance.DebugPush(GameState.evaluateAttack);
		attackTarget = target;
		Unit.selectedEnemies.Clear();
	}

	void OnGUI(){
		Vector2 mpos = Input.mousePosition;
		// display the PrintMessage()
		if(duration > 0){
			int width = text.Length*8+20;
			GUI.Box(new Rect(Screen.width/2-width/2,Screen.height/4,width,30), text);
			duration -= Time.deltaTime;
		}
		if(StateManager.Instance.State == GameState.evaluateAttack){
			BuildAttackConfirmation();
		}else if(StateManager.Instance.Turn == GameState.playerTurn && StateManager.Instance.State != GameState.dialogue){
			// displaying stat blocks
#if UNITY_STANDALONE
			// Mouseover statblock
			Tile underMouseTile = TileGrid.Instance.GetTileAt(Camera.main.ScreenToWorldPoint(mpos));
			if(underMouseTile != null && underMouseTile.Visible && underMouseTile.isOccuppied && !underMouseTile.Unit.invisible){
				if(mpos.x < Screen.width/2){
					BuildUnitStatBlock(underMouseTile.Unit, (int)mpos.x+10, Screen.height-(int)mpos.y-50);
				}else{
					BuildUnitStatBlock(underMouseTile.Unit, (int)mpos.x-105, Screen.height-(int)mpos.y-50);
				}
			}
#endif
#if UNITY_ANDROID
			// selected unit statblock.
			foreach(Unit u in Unit.selectedEnemies){
				BuildUnitStatBlock(u, 10, -222);
				break;
			}
#endif
			if(Unit.SelectedUnit != null){
				if(mpos.x < 120 && mpos.y < 160){
					BuildUnitStatBlock(Unit.SelectedUnit, 10, 10);
				}
				else{
					BuildUnitStatBlock(Unit.SelectedUnit);
				}
			}
		}

		// displayes the button menu
		if(StateManager.Instance.State == GameState.buttonMenu){
			for(int i = 0; i < buttons.Length; i++){
				if(GUI.Button(new Rect(buttonAnchor.x, Screen.height-buttonAnchor.y+(_buttonMenuButtonSize+3)*i, 145, _buttonMenuButtonSize), buttons[i].ButtonLabel)){
					StateManager.Instance.DebugPop();
					buttons[i].ButtonPressed();
					break;
				}
			}
		}
	}

	/// <summary>
	/// Shows the button menu under the current mouse position.
	/// </summary>
	/// <param name="buttons">Buttons.</param>
	public void ShowButtonMenu(params IGUIButtonListener[] buttons){
		if(buttons.Length > 0){
			StateManager.Instance.DebugPush(GameState.buttonMenu);
			if(Input.mousePosition.x < Screen.width/2){
				buttonAnchor = Input.mousePosition + new Vector3(20+Screen.width/30, _buttonMenuButtonSize*buttons.Length/2, 0);
			}else{
				buttonAnchor = Input.mousePosition + new Vector3(-150-Screen.width/30, _buttonMenuButtonSize*buttons.Length/2, 0);
			}
			if(buttonAnchor.y < buttons.Length*(_buttonMenuButtonSize+3)){
				buttonAnchor.y = buttons.Length*(_buttonMenuButtonSize+3);
			}else if(buttonAnchor.y > Screen.height){
				buttonAnchor.y = Screen.height;
			}
			this.buttons = buttons;
		}else{
			Debug.LogWarning("ShowButtonMenu() was given an empty list.");
		}
	}
	
	private void BuildUnitStatBlock(Unit client, int x = 10, int y = -111)
	{

		int MARGAIN = 23;
		if(y == -111){
			y = Screen.height - MARGAIN*12 -20;
		}else if (y == -222) {
			y = Screen.height - MARGAIN*20 - 30;
		}else if(y < 0){
			y = 0;
		}else if(y > Screen.height-MARGAIN*9-10){
			y = Screen.height-MARGAIN*12-10;
		}

		Stats s = client.GetStatsAt(client.Tile);

		GUI.Box(new Rect(x,y,100,MARGAIN*12+10), client.name);
		GUI.Label(new Rect(x+5, y+5+MARGAIN*1, 90, 23), ("Level: " + client.level));
		GUI.Label(new Rect(x+5, y+5+MARGAIN*2, 90, 23), ("HP: " + client.CurrentHP + "/" + client.ModifiedStats.maxHP));
		if(client.doubleAttack) GUI.Label(new Rect(x+5, y+5+MARGAIN*3, 90, 23), ("Attack: " + (s.strength + s.might))  + " x2");
		else GUI.Label(new Rect(x+5, y+5+MARGAIN*3, 90, 23), ("Attack: " + (s.strength + s.might)));
		GUI.Label(new Rect(x+5, y+5+MARGAIN*4, 90, 23), ("Hit: " + Mathf.RoundToInt(s.Hit*100f) + "%"));
		GUI.Label(new Rect(x+5, y+5+MARGAIN*5, 90, 23), ("Dodge: " + Mathf.RoundToInt(s.Dodge*100f) + "%"));
		GUI.Label(new Rect(x+5, y+5+MARGAIN*6, 90, 23), ("Defence: " + s.defense));
		GUI.Label(new Rect(x+5, y+5+MARGAIN*7, 90, 23), ("Resitance: " + s.resistance));
		GUI.Label(new Rect(x+5, y+5+MARGAIN*8, 90, 23), ("Move: " + s.movement.moveSpeed));
        GUI.Label(new Rect(x + 5, y + 5 + MARGAIN * 9, 90, 23), ("Crit: " + s.crit * 100+"%"));
        GUI.Label(new Rect(x + 5, y + 5 + MARGAIN * 10, 90, 23), ("Crit Ddg: " + s.critDodge * 100 + "%"));
        GUI.Label(new Rect(x+5, y+5+MARGAIN*11, 90, 23), (client.toolTip));
	}

	/// <summary>
	/// Builds the attack confirmation.
	/// </summary>
	private void BuildAttackConfirmation(){

		int yAnchor = System.Math.Min(Screen.height-100, Screen.height/10*8);

		Unit selected = Unit.SelectedUnit;

		if(GUI.Button(new Rect(Screen.width/2+50, yAnchor, 100, 30), "Attack!")){
			ConfirmAttack();
		}
		if(GUI.Button(new Rect(Screen.width/2-150, yAnchor, 100, 30), "Cancel")){
			StateManager.Instance.DebugPop();
		}

		BuildAttackDefenceWindow(attackTarget, selected, Screen.width/2+155, yAnchor);
		BuildAttackDefenceWindow(selected, attackTarget, Screen.width/2-155-134, yAnchor);

		int targetHealth = attackTarget.CurrentHP;
		int unitHealth = selected.CurrentHP;
		try{targetHealth -= selected.AttackInfo.effect.Apply(attackTarget.Tile, selected, true);}catch{};
		if(targetHealth > 0){
			try{
				if(attackTarget.AttackInfo.GetAttackTiles(attackTarget).Contains(selected.Tile))
					unitHealth -= attackTarget.AttackInfo.effect.Apply(selected.Tile, attackTarget, true);
			}catch{};
			if(unitHealth > 0 && selected.doubleAttack){
				try{targetHealth -= selected.AttackInfo.effect.Apply(attackTarget.Tile, selected, true);}catch{};
				if(targetHealth > 0 && attackTarget.doubleAttack){
					if(attackTarget.AttackInfo.GetAttackTiles(attackTarget).Contains(selected.Tile))
					try{unitHealth -= attackTarget.AttackInfo.effect.Apply(selected.Tile, attackTarget, true);}catch{};
				}
			}
		}

		BuildHealthBar(Screen.width/2+255, yAnchor+50, attackTarget.ModifiedStats.maxHP, attackTarget.CurrentHP, targetHealth, true);
		BuildHealthBar(Screen.width/2-255, yAnchor+50, selected.ModifiedStats.maxHP, selected.CurrentHP, unitHealth);

		GUI.Box(new Rect(Screen.width/2+155, yAnchor-30, 100, 30), attackTarget.name);
		GUI.Box(new Rect(Screen.width/2-155-134, yAnchor-30, 100, 30), selected.name);
	}

	private void BuildAttackDefenceWindow(Unit unit, Unit other, int x, int y){
		GUI.Box(new Rect(x, y, 134, 36), "");
		GUI.Label(new Rect(x+3, y+3, 32, 32), (other.AttackInfo.effect.damageType.Magic)? _resistance : _shield);
		GUI.Label(new Rect(x+3+32, y+9, 32, 32), (other.AttackInfo.effect.damageType.Magic)? unit.ModifiedStats.resistance.ToString() : unit.ModifiedStats.defense.ToString());
		GUI.Label(new Rect(x+3+64, y+3, 32, 32), _swords);
		Stats s = unit.GetStatsAt(unit.Tile);
		GUI.Label(new Rect(x+3+96, y+9, 32, 32), (unit.doubleAttack) ? (s.might + s.strength) + " x2" : (s.might + s.strength).ToString());
	}

	private void BuildHealthBar(int x, int y, int maxHealth, int currentHealth, int projectedHealth, bool mirror = false){
		int mult = (mirror) ? -1 : 1;
		GUI.DrawTexture(new Rect(x, y, mult*200*currentHealth/maxHealth, 48), _healthBarRed);
		if(projectedHealth > 0) GUI.DrawTexture(new Rect(x, y, mult*System.Math.Max(12, 200*projectedHealth/maxHealth), 48), _healthBar);
		GUI.DrawTexture(new Rect(x, y, mult*200, 48), _healthFrame);
	}

	/// <summary>
	/// Called when the Specified tile is clicked
	/// </summary>
	/// <param name="tile">Tile.</param>
	public void Clicked(Tile tile){
		if(StateManager.Instance.State == GameState.evaluateAttack){
			if(tile == attackTarget.Tile){
				ConfirmAttack();
			}
			if(tile.isOccuppied && tile.Unit.isHostile(Unit.SelectedUnit) && Unit.SelectedUnit.AttackInfo.GetAttackTiles(Unit.SelectedUnit).Contains(tile))
			{
				attackTarget=tile.Unit;
			}
		}
	}

	private void ConfirmAttack(){
		StateManager.Instance.DebugPop();
		Unit.SelectedUnit.StartAttackSequence(attackTarget);
		Tile.UnColourAll();
	}

}
