using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {

	//Path to walk
	List<Tile> path;
	int currTile;

	// animation fields
	private float tweenSpeed;
	const float SPEEDMULT = 1.5f;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private bool moving = false;
	private float tweenPosition;

	// fields visible in the unity editor
	public bool isBoss=false;
	public int team;
	public bool invisible = false;
	private int retaliations = 1;
	public Material greyscale;
	public Material normalMaterial;
	private AttackInfo attackInfo;
	public IAIBehaviour unitAI;
	[NonSerialized]
	Character character;

	// fields
	public static Unit retaliatingUnit;
	private Tile tile;
	public HashSet<Tile> reachableTiles;
	internal int retaliationsMade = 0; // TODO, encapsulate
    public static HashSet<Unit> selectedEnemies=new HashSet<Unit>();
	internal Action currAction;
	private Action issuedAction;
	private static Unit selectedUnit;
	private bool isFirstUpdate = true;
	internal int damageTaken = 0;
	private bool hasActed = false;
	private HashSet<HealthObserver> _healthObservers = new HashSet<HealthObserver>();
	private Stats attackStats;
	internal bool doubleAttack = false;
	private HashSet<object> _inhibs = new HashSet<object>();
	
    DamageData attackData;

	// Properties
	public Stats ModifiedStats{
		get{
			Debug.LogWarning("Unit.Modified Stats is deprechiated!");
			return Character.ModifiedStats + BuffManager.Instance.GetBuffs(this) + attackStats;
		}
	}

	public bool FaceRight{
		get{return transform.localScale.x == 1;}
		set{
			if(value &! FaceRight){
				transform.localScale = new Vector3(1, 1, 1);
			}else if(!value && FaceRight){
				transform.localScale = new Vector3(-1, 1, 1);
			}
		}
	}

	public bool IsAlive{
		get{
			return damageTaken < Character.ModifiedStats.maxHP;
		}
	}

	public bool HasActed {
		get {
			if(_inhibs.Count > 0)
			{
				return true;
			}
			else
			{
				return this.hasActed;
			}
		}
		set {
			hasActed = value;
			if(team == UnitManager.PLAYER_TEAM && _inhibs.Count == 0){
				MakeGrey(value);
			}
			else if(_inhibs.Count > 0)
			{
				MakeGrey(true);
			}
		}
	}
	public Tile Tile{
		get{return tile;}
	}
	public bool isHostile(Unit other)
	{
		if(this == other) return false; // we aint hostile towards ourself now are we.
		if(team == 3 || other.team == 3) return true;
		return other.team%2 != team%2;
	}

	public static Unit SelectedUnit{
		get{return selectedUnit;}
	}

	public int CurrentHP{
		get
		{ 
			if (character == null) return -1;
			return Character.ModifiedStats.maxHP - damageTaken;
		} 
	}

	public int RetaliationsLeft
	{
		get { return Retaliations - retaliationsMade;}
		set {
			retaliationsMade = retaliations - value;
			// if (retaliationsMade > retaliations) retaliationsMade = retaliations;
		}
	}

	// Methods


	private bool givenCommand = false;
	private Action command;
	public void PerformAction(Action act = null)
	{
		// Debug.Log(act.ToString());
		if (act != null)
		{
			command = act;
			currAction = act.Duplicate();
			givenCommand = true;
		}
		if(command != null && StateManager.Instance.State == GameState.unitSelected)
		{
			if (command.movement != null && tile != command.movement)
			{
				MoveToAndAnimate(currAction.movement);
			}
			else if(command.attack != null)
			{
				StartAttackSequence(command.attack.Unit);
				command.attack = null;
				givenCommand = false;
			}
			else
			{
				FinnishMovement();
			}
		}
	}

	void Awake()
	{
		UnitManager.Instance.Add(this);
		// gimme a health bar!
		HealthBar.NewHealthBar(transform);
		attackInfo = new AttackInfo();
		WeaponDamage wd = new WeaponDamage();
		wd.BaseDamage = 1;
		wd.DexScale = 0.5f;
		wd.IntScale = 0.25f;
		wd.StrScale = 0.75f;
		attackInfo.Effect = wd;
		if(character != null )Character = character;
	}

	/// <summary>
	/// Starts the attack sequence.
	/// Attack, counter, second attack, second counter.
	/// </summary>
	/// <param name="target">Target.</param>
	public void StartAttackSequence(Unit target = null)
	{
		if(target == null) target = retaliatingUnit;
		else retaliatingUnit=target;
		StartCoroutine("AttackSequenceCoroutine");
	}

	private IEnumerator AttackSequenceCoroutine(){
		
		StateManager.Instance.DebugPush(GameState.runningAttackSequence);
		// attack
		SendMessage("StartingAttackSequence", retaliatingUnit, SendMessageOptions.DontRequireReceiver);
		retaliatingUnit.SendMessage("StartingAttackSequence", this, SendMessageOptions.DontRequireReceiver);
		while(StateManager.Instance.State != GameState.runningAttackSequence) yield return null; // pause here if were not in the right state.

		Attack(retaliatingUnit);
		while(StateManager.Instance.State != GameState.runningAttackSequence) yield return null;

		// counter
		if(retaliatingUnit && retaliatingUnit.IsAlive && retaliatingUnit.RetaliationsLeft > 0){
			if (retaliatingUnit.Attack(this) && retaliatingUnit.RetaliationsLeft > 0)
			{
				retaliatingUnit.retaliationsMade++;
			}
			while(StateManager.Instance.State != GameState.runningAttackSequence) yield return null;

			// second attack
			if(this && IsAlive){
				if(doubleAttack) Attack(retaliatingUnit);
				while(StateManager.Instance.State != GameState.runningAttackSequence) yield return null;

				// second counter
				if(retaliatingUnit && retaliatingUnit.IsAlive){
					if(retaliatingUnit.doubleAttack) retaliatingUnit.Attack(this);
					while(StateManager.Instance.State != GameState.runningAttackSequence) yield return null;
				}   
			}
		}

		// end
		StateManager.Instance.DebugPop();
		if(this)SendMessage("FinishedAttackSequence", retaliatingUnit,SendMessageOptions.DontRequireReceiver);
		if(retaliatingUnit) retaliatingUnit.SendMessage("FinishedAttackSequence", this,SendMessageOptions.DontRequireReceiver);
		attackStats = new Stats();
		retaliatingUnit.attackStats = new Stats();
		retaliatingUnit = null;
		FinnishMovement();
	}

	/// <summary>
	/// attack a target and do everything needed
	/// </summary>
	/// <param name="target"></param>
	bool Attack(Unit target){

		if(AttackInfo.CanAttack(this, target))
		{
			float dx = transform.position.x - target.transform.position.x;
			if(dx > 0){
				FaceRight = false;
			}else if(dx < 0){
				FaceRight = true;
			}

			Stats s = GetStatsAt(Tile, target);
			Stats es = target.GetStatsAt(target.Tile, this);
			float roll = UnityEngine.Random.Range(0f,1f);
            DamageData dmgData=new DamageData();
			dmgData.hit = roll < s.HitVersus(es); //ModifiedStats.Hit-target.ModifiedStats.Dodge;
			dmgData.crit = roll < s.CritVersus(es); //(ModifiedStats.crit - target.ModifiedStats.critDodge);
            dmgData.baseDamage = s.strength + s.might;
            dmgData.target = target;
            dmgData.source = this;
            attackData = dmgData;
			AnimateAttack();
			return true;
		}
		return false;
	}
	// Use this for initialization
	void Start () {
		foreach(MonoBehaviour mb in GetComponents<MonoBehaviour>()){
			if(mb is IAIBehaviour&& mb!=null)
			{
				unitAI=(IAIBehaviour)mb;
			}
		}
		if(team != UnitManager.PLAYER_TEAM){
			if(unitAI == null){
				unitAI = gameObject.AddComponent<Defensive>();
			}
			AIManager.Instance.AddUnit(this);
		}
		if(SaveFile.Active != null){
			SaveFile.Active.UpdateUnit(this);
			if(GetComponent<Skill>() == null){
				SaveFile.Active.BestowAbilitites(this);
			}
		}
		// gimme manna plox
		if(GetComponent<Mana>() == null){
			gameObject.AddComponent<Mana>();
		}
	}

	/// <summary>
	/// Assumes that it is the player that uses canmove/// </summary>
	/// <value><c>true</c> if this instance can move; otherwise, <c>false</c>.</value>
	public bool CanMove {
		get{
			if(HasActed==false&&team==UnitManager.PLAYER_TEAM)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public int Retaliations
	{
		get
		{
			return retaliations;
		}

		set
		{
			retaliations = value;
		}
	}

	public Character Character
	{
		get
		{
			return character;
		}
		set
		{
			character = value;
			character.Initialize(this);
			// Assign Sprite
			GetComponent<SpriteRenderer>().sprite = character.Sprite;
		}
	}

	public AttackInfo AttackInfo
	{
		get
		{
			// Get attack info from equipped weapon
			Backpack bp = character.Backpack;
			if (bp != null && bp.EquippedWeapon != null)
			{
				return bp.EquippedWeapon.attackInfo;
			}
			return attackInfo;
		}
	}

	// Update is called once per frame
	void Update () {
		if(isFirstUpdate){
			MoveTo(TileGrid.Instance.GetTileAt(transform.position));
			isFirstUpdate = false;
			FaceRight = (team != UnitManager.AI_TEAM);
			if(ReverseStartFacing.reverse) FaceRight = !FaceRight;
		}
		// animation update goes here
		if(moving){ // TODO switch with a state machine PALM: Maybe later when we go more in detail with animation.
			switch(StateManager.Instance.State){
			case GameState.unitMoving :
				tweenPosition += tweenSpeed*Time.deltaTime;
				if(tweenPosition >= 1f){
					if (currTile>path.Count-1)//If has reached end of path
					{
						StateManager.Instance.DebugPop();
						transform.position = endPosition;
						moving = false;
						SendMessage("EndAnimatedMove", SendMessageOptions.DontRequireReceiver);
					}else{
						startPosition = transform.position;
						endPosition = path[currTile].transform.position;
						transform.position = startPosition;
						float
							dx = startPosition.x - endPosition.x,
							dy = startPosition.y - endPosition.y,
							distance = Mathf.Sqrt(dx*dx+dy*dy);
						tweenSpeed = SPEEDMULT*Character.ModifiedStats.movement.moveSpeed/distance;
						moving = true;
						tweenPosition -= 1f;
						currTile++;
						if(dx < 0){
							FaceRight = true;
						}else if(dx > 0){
							FaceRight = false;
						}
					}
				}else{
					//Debug.Log("Animating!");
					transform.position = endPosition*tweenPosition + startPosition*(1f-tweenPosition);
				}
				break;
			}
		}
		else if(givenCommand)
		{
			PerformAction();
		}
	}

	public bool MoveTo(Tile tile)
	{
		if(tile.enterTile(this)){
			if(Tile != null){
				Tile.leaveTile();
			}
			this.tile = tile;
			transform.position = tile.transform.position;

			if(currAction != null) currAction.movement = tile;
			SendMessage("UnitMoved", SendMessageOptions.DontRequireReceiver);
			return true;
		}else if(tile==Tile)
		{
			return true;
		}else{
			Debug.LogError("Tried to enter a tile I cant! " + this + tile);
		}

		return false;
	}

	/// <summary>
	/// Plays a cute little attack aniomation and puts teh statemanger into the unitAttack state untill the end of the animation.
	/// DOES NOT actually issue the attack, use AttackUnit() instead.
	/// </summary>
	/// <returns><c>true</c>, if attack was animated, <c>false</c> otherwise.</returns>
	/// <param name="tile">Tile.</param>
	public bool AnimateAttack()
	{
		if(attackData.hit)AttackInfo.AttackAnimation.Animate(this, attackData.target.tile, new Action<Tile>(ApplyEffect), attackData.hit);
		else AttackInfo.AttackAnimation.Animate(this, attackData.target.tile, new Action<Tile>(SendOnMiss), attackData.hit);
		return true;
	}

	/// <summary>
	/// Called when an attack misses.
	/// </summary>
	/// <param name="t">T, tile the target who are missed stands in.</param>
	public void SendOnMiss(Tile t){
		if(t.isOccuppied){
			t.Unit.SendMessage("OnDodgeAttack", this, SendMessageOptions.DontRequireReceiver);
			SendMessage("OnMissAttack", t.Unit, SendMessageOptions.DontRequireReceiver);
			Particle.Dodge(t.transform.position);
		}
	}

	public bool MoveToAndAnimate(Tile tile, bool noncombat = false)
	{
		if (noncombat)
		{
			reachableTiles = TileGrid.Instance.GetFreeTiles();
		}
		invisible = false;
		reachableTiles.UnionWith( TileGrid.Instance.GetOccuppiedTiles(this));
		Pathfinder temp=new Pathfinder(Tile, tile, reachableTiles);
		path=temp.path;
		currTile=0;
		// just tween the twat at a certain speed.
		startPosition = transform.position;

		if(MoveTo(tile)){
			if(!moving) StateManager.Instance.DebugPush(GameState.unitMoving);

			endPosition = path[currTile].transform.position;
			transform.position = startPosition;
			float
				dx = startPosition.x - endPosition.x,
				dy = startPosition.y - endPosition.y,
				distance = Mathf.Sqrt(dx*dx+dy*dy);
			tweenSpeed = SPEEDMULT* Character.ModifiedStats.movement.moveSpeed/distance;
			moving = true;
			tweenPosition = 0f;
			currTile++;

			SendMessage("StartAnimatedMove", SendMessageOptions.DontRequireReceiver);

			if(dx < 0){
				FaceRight = true;
			}else if(dx > 0){
				FaceRight = false;
			}

			return true;
		}
		return false;
	}

	public override string ToString ()
	{
		return string.Format ("[Unit: name={0}]", name);
	}

	/// <summary>
	/// Damage this Unit for N ammounts of damage, reduced by Defence.
	/// </summary>
	/// <param name="n">N.</param>
	public int Damage(DamageData attackData)
	{
		
        int n = attackData.ApplyDefences(Character.ModifiedStats.defense, Character.ModifiedStats.resistance);

		if (attackData.testAttack) return n;
		
		damageTaken += n;
		NotifyHealthObservers(-n);
		attackData.Callback();
		
		if(damageTaken >= Character.ModifiedStats.maxHP){
			Death (true);
			SFXPlayer.Instance.DeathSound();
		}

		return n;
	}

	/// <summary>
	/// Kill or Remove this unit.
	/// </summary>
	public void Death(bool grantxp = false)
	{
		if(selectedUnit == this){
			if(retaliatingUnit != null){
				SendMessage("UnitDeath", retaliatingUnit, SendMessageOptions.DontRequireReceiver);
				if(grantxp) retaliatingUnit.RewardExperience(this); // please dont break;
			}else{
				SendMessage("UnitDeath", null, SendMessageOptions.DontRequireReceiver);
			}
		}else if(selectedUnit != null){
			SendMessage("UnitDeath", selectedUnit, SendMessageOptions.DontRequireReceiver);
			if(grantxp) selectedUnit.RewardExperience(this); // please dont break;
		}else{
			SendMessage("UnitDeath", null, SendMessageOptions.DontRequireReceiver);
		}

		UnitManager.instance.Remove(this);
		selectedEnemies.Remove(this);
		Tile.leaveTile();
		// maybe time to fix death animation?

		foreach(Debuff db in GetComponents<Debuff>()) Destroy(db); // deads guys dont have debuffs!

		GetComponent<Renderer>().enabled = false; // HACK
	}

	private void RewardExperience(Unit dead){
		// get all alies close enough to me
		HashSet<Unit> clients = UnitManager.instance.GetNearbyAllies(this);

		// hand out XP to them
		float fractionEach = (clients.Count == 0f) ? 0f : 0.5f/clients.Count;
		foreach(Unit u in clients){
			u.RewardExperience(dead, fractionEach);
		}
		// give me some sugar
		float bonus = 0f;
		// I get bonus XP if I killed a boss!
		if(dead.isBoss) bonus += 0.5f;
		RewardExperience(dead, (fractionEach == 0f) ? 1f : 0.5f, bonus);
	}

	private void RewardExperience(Unit dead, float fraction, float bonus = 0f){
		// calc xp gained
		float sum = 0.55f * Mathf.Pow(1.12f, (float)dead.Character.Level) / Mathf.Pow(1.18f, (float)Character.Level) *fraction;
		sum += bonus;
		if(SaveFile.Active != null){
			if(SaveFile.Active.GrantXP(this, sum)){
				Particle.LevelUp(transform.position);
				SendMessage("LevelUp", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public void Heal(int n)
	{
		if(n > 0){
			damageTaken -= n;
			NotifyHealthObservers(n);
			if(damageTaken < 0) damageTaken = 0;
			Particle.HealParticle(transform.position);
		}else{
			Debug.LogWarning("No zero or negative heals!");
		}
	}

	public HashSet<Tile> GetReachableTiles()
	{
		HashSet<Tile> retValue=new HashSet<Tile>();
		Tile.GetReachableTiles(Character.ModifiedStats.movement, retValue, this);
		return retValue;
	}

	public void FinnishMovement()
	{
		HasActed = true;
		Tile.UnColourAll();
		selectedUnit = null;
		if(StateManager.Instance.State==GameState.unitSelected)
		{
			StateManager.Instance.DebugPop();
		}
		History.Instance.Add(currAction);
		currAction = null;
	}

	private void MakeGrey(bool grey){
		try{
			if(grey){
				GetComponent<Renderer>().material = greyscale;
			}else{
				GetComponent<Renderer>().material = normalMaterial;
			}
		}catch(System.NullReferenceException){
			Debug.LogError(this + " is missing a greyscale/normal shader");
		}
	}

	/// <summary>
	/// Selects this unit, making it the selectedUnit.
	/// </summary>
	public void Select(){
		if(selectedUnit != null){
			Debug.LogError("Selected a new unit before deselecting previous unit! Cancelling uncommitted actions! Need to call either the active units FinnishMovement() or Deselect() first");
			Deselect();
		}
		selectedUnit = this;
		SendMessage("UnitSelected", SendMessageOptions.DontRequireReceiver);
		currAction = new Action(Tile);
		reachableTiles = GetReachableTiles();
	}
	/// <summary>
	/// Deselect the active Unit, not commiting its action, moving back to the tile whence it came from.
	/// </summary>
	public static void Deselect(){
		Unit u = selectedUnit;
		selectedUnit.MoveTo(selectedUnit.currAction.startTile);
		selectedUnit.currAction = null;
		selectedUnit = null;
		if(u != null) u.SendMessage("UnitDeselected", SendMessageOptions.DontRequireReceiver);
	}
	
	public void ChangeAI(IAIBehaviour ai){
		if(ai != null){
			unitAI = ai;
		}
	}

	public void ApplyEffect(Tile target){
		AttackInfo.Effect.Apply(attackData);
	}

	public void RegisterHealthObserver(HealthObserver o){
		_healthObservers.Add(o);
	}

	private void NotifyHealthObservers(int n){
		foreach(HealthObserver ho in _healthObservers){
			try{
				ho.NotifyHealth(this, n);
			}catch(System.Exception e){
				Debug.LogException(e);
			}
		}
	}

	// HACK
	void StartingAttackSequence(Unit u){
		invisible = false;
	}
	// HACK
	void StartAnimatedMove(){
		invisible = false;
	}


	private IReach _range;
	private IReach _storeRange;
	public void AddInhibitor(object o){
		if(_inhibs.Count == 0){
			if(_range == null){
				_range = new Norange();
			}
			_storeRange = AttackInfo.Reach;
			AttackInfo.Reach = _range;
		}
		_inhibs.Add(o);
		MakeGrey(true);
	}

	public void RemoveInhibitor(object o){
		if(_inhibs.Remove(o)){
			if(_inhibs.Count == 0){
				AttackInfo.Reach = _storeRange;
				Debug.Log("Removed Last one!");
			}
			HasActed = HasActed;
		}
	}

	private List<AttackBuff> attackBuffs = new List<AttackBuff>();
	
	public void RegisterAttackBuff(AttackBuff a)
	{
		attackBuffs.Add(a);
	}

	/*
	/// <summary>
	/// Applies attack buffs before an attack is being made.
	/// </summary>
	/// <param name="target"></param>
	private void ApplyAttackBuffs(Unit target)
	{
		attackStats = new Stats();
		foreach(AttackBuff ab in attackBuffs)
		{
			if (ab.Applies(target, Tile)) attackStats += ab.Stats;
		}
	}
	*/

	/// <summary>
	/// Get information on a units stats in certain area during a certain interaction.
	/// </summary>
	/// <param name="location"></param>
	/// <param name="enemy"></param>
	/// <param name="enemyTile"></param>
	/// <returns></returns>
	public Stats GetStatsAt(Tile location, Unit enemy = null, Tile enemyTile = null)
	{
		if(enemy != null)
			if (enemyTile == null)
				enemyTile = enemy.Tile;
		Stats s = Character.ModifiedStats + BuffManager.Instance.GetBuffs(this, location);

		if (enemy != null)
		{
			foreach (AttackBuff ab in attackBuffs)
			{
				if (ab.Applies(enemy, location, enemyTile)) s += ab.Stats;
			}
		}

		return s;
	}
}
