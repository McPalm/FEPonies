using System.Collections.Generic;

static public class SkillDB{

	static public string[] GetSkills(string name){
		switch(name){
		case "Tinker Spark" :
			return TINKER;
		case "Dawn" :
			return DAWN;
		case "Ma Cherie" :
			return FENCER;
		case "Rusty" :
			return RUSTY;
		case "Syrah":
			return BAT;
		case "Moonlight" :
			return MOONLIGHT;
		case "Luna" :
			return LUNA;
		default :
			UnityEngine.Debug.LogWarning(name + " has no skills!");
			break;
		}

		return new string[0];
	}

	static public string GetDescription(string ability){
		switch(ability){
		case "Charm" :
			return CHARMABILITY;
		case "Teleport" :
			return TELEPORT;
		case "Summon" :
			return SUMMON;
		case "Lasso" :
			return LASSO;
		case "Sing" :
			return SING;
		case "BackupWeapon" :
			return BACKUPWEAPON;
		case "Sniper" :
			return SNIPER;
		case "Assassin" :
			return ASS;
		case "Guard" :
			return GUARD;
		case "ArmourPiercing" :
			return AP;
		case "Vampony" :
			return VAMPIRE;
		case "Wings" :
			return WINGS;
		case "Charge" :
			return CHARGE;
		case "DoubleAttack" :
			return DOUBLEATTACK;
		case "PinningStrike" :
			return PINSTRIKE;
		case "Inspire":
			return INSPIRE;
		case "HealingSalve" :
			return SALV;
		case "Transfusion" :
			return TRANS;
		case "Wubs" :
			return WUBS;
		case "Explosion" :
			return BOOM;
		case "Feint" :
			return FEINT;
		case "Cure" :
			return CURE;
		case "Backflip" :
			return BACKFLIP;
		case "Wormhole" :
			return WORMHOLE;
		case "Wingslayer" :
			return WINGSLAYER;
		case "MageSlayer" :
			return MAGESLAYER;
		case "GravityField" :
			return GRAVITYFIELD;
		case "Riposte" :
			return RIPOSTE;
		case "Sap" :
			return SAP;
		case "Bastion" :
			return BASTION;
		default :
			return "<missing description>";
		}
	}

	static private readonly string[] TINKER = {
		"GravityField",
		"Wormhole",
		"Teleport",
	};

	static private readonly string[] DAWN = {
		"Cure",
		"Bastion"
	};
	static private readonly string[] FENCER = {
		"Feint",
		"DoubleAttack",
		"Riposte"
	};
	static private readonly string[] RUSTY = {
		"Guard",
		"ArmourPiercing",
		"Wingslayer"
	};
	static private readonly string[] BAT = {
		"Vampony",
		"Transfusion",
		"MageSlayer"
	};
	static private readonly string[] MOONLIGHT = {
		"Backflip",
		"Assassin",
		"Sap"
	};
	static private readonly string[] LUNA = {
		"Summon",
		"Charm",
		"ManaLeech"
	};

	static private readonly string CHARMABILITY =
		"<b>Charm</b>\n" +
			"Charms a single unit for the remainder of the fight, making them follow {0} around and fight for her.\n" +
			"Costs 2 Mana."
			;
	static private readonly string TELEPORT =
		"<b>Teleport</b>\n" +
			"{0} may Teleport anywhere on the map, but watch out, as {0} cant move or attack after the teleport.\n" +
			"Cost 1 Mana."
			;
	static private readonly string SUMMON =
		"<b>Summon</b>\n" +
			"Summons a weak changeling to follow {0} around and fight for her, its power is based on {0}'s level but has overall poor stats, great for soaking damage. Unlimited uses but cost health equal to 3 plus half of {0}'s level."
			;
	static private readonly string LASSO =
		"<b>Lasso</b>\n" +
			"Snare an enemy with an lasso, making them unable to move their next turn, they can still attack however. Can be used on targets at both melee and range."
			;
	static private readonly string SING =
		"<b>Boost Morale!</b>\n" +
			"{0} Burt out in song! Increasing the attack power of allies within 3 steps of {0} by 2+(level/5). If {0} suffers any damage the song is interupted."
			;

	static private readonly string BACKUPWEAPON =
		"<b>Backup Weapon!</b>\n" +
			"Keep a backup weapon handy, a bow if the pony is melee, or a melee weapon if the pony is an archer. Switch whenever at no cost."
			;

	static private readonly string SNIPER =
		"<b>Sniper</b>\n" +
			"{0} archery skills icreases, her attacks can now reach 3 squares away as well as 2 squares."
			;

	static private readonly string ASS =
		"<b>Assassin</b>\n" +
			"{0} has a chance equal to 10+agi*2% to deal critical damage on a hit." +
			"+1 Movespeed."
			;

	static private readonly string GUARD =
		"<b>Guard</b>\n" +
			"Activate for +3 attack for one turn, whenever {0} suffer any damage this turn, she heals for 3 hitpoints."
			;
	static private readonly string AP =
		"<b>Armour Piercing Attacks</b>\n" +
			"{0}'s attacks now penetrate half of the opponents defence. Also increase {0}'s attack by +1"
			;

	static private readonly string VAMPIRE =
		"<b>Vampony</b>\n" +
			"Whenever {0} deals damage, {0} heals for half the damage dealt."
			;

	static private readonly string WINGS =
		"<b>Gossamer Wings</b>\n" +
			"{0} Cast a spell on an ally or self. That unit can fly for the duration of the fight, this also makes them vulnerable to archers."
			;
	static private readonly string DOUBLEATTACK =
		"<b>Double Attack</b>\n" +
			"{0} attacks a second time when attacking, enemies may retaliate before this attack."
			;

	static private readonly string CHARGE =
		"<b>Charge</b>\n" +
			"Deal bonus damage based on far you moved this turn."
			;

	static private readonly string PINSTRIKE =
		"<b>Pinning Strike</b>\n" +
			"Make a regular attack (with retaliation) that pins an opponent in place, making them unable to move."
			;

	static private readonly string INSPIRE =
		"<b>Inspire</b>\n" +
			"When {0} attacks, ponies around her is healed for one third of the damage dealt."
			;

	static private readonly string SALV =
		"<b>Healing Salve</b>\n" +
			"Can use a healing Salve an any ally twice per fight, heals for level+10 hitspoints and increase the targets attack, defence and resistance by +2 for two turns."
			;

	static private readonly string TRANS =
		"<b>Transfusion</b>\n" +
			"{0} can sacrifice 1/4 of her current life to heal an ally for twice that ammount."
			;

	static private readonly string WUBS =
		"<b>Wubs</b>\n" +
			"{0} fire her basscannon, damaging up to three targets in a line."
			;

	static private readonly string BOOM =
		"<b>Explosion</b>\n" +
			"{0} hurl a large ball of fire explode at a target square and hurt everyone in it and around it."
			;

	static private readonly string FEINT =
		"<b>Feint</b>\n" +
			"Add {0}'s intelligence to dodge and twice that to hit."
			;

	static private readonly string CURE =
		"<b>Cure</b>\n" +
			"Heals a target for 5 plus {0}'s intelligence and removes debuffs.\n" +
			"Cost 1 Mana"
			;

	static private readonly string BACKFLIP =
		"<b>Backflip</b>\n" +
			"If {0} dodge a melee strike, she backsteps from the target and counter attack.\n" +
			"Does not work if backed up against a wall or ally"
			;

	static private readonly string WORMHOLE =
		"<b>Wormhole</b>\n" +
			"{0} Swap place with one other unit, range based on intelligence\n" +
			"Cost 1 Mana."
			;

	static private readonly string WINGSLAYER =
		"<b>Wingslayer</b>\n" +
			"{0} specializes in fighting flying foes.\n" +
			"Increase chance to hit by 30% and double damage when fighting flying enemies."
			;

	static private readonly string MAGESLAYER =
		"<b>MageSlayer</b>\n" +
			"{0} specializes in fighting casters.\n" +
			"Doubles damage against units with Mana, also comes with a slight boost to resistance."
			;

	static private readonly string GRAVITYFIELD =
		"<b>Gravity Field</b>\n" +
			"{0} can erect a gravity field, hampering movement in that area, and reducing hit and dodge chance by 20% to those in that area. The gravity field persist for two rounds.\n" +
			"Cost 1 Mana."
			;

	static private readonly string RIPOSTE =
		"<b>Riposte</b>\n" +
			"Whenever {0} dodge, any attack she does immediately after scores a critical hit for double damage!"
			;

	static private readonly string SAP =
		"<b>Sap</b>\n" +
			"Sap a melee target, greatly reducing their movement, accuracy and dodge for two turns, as well as dealing a small ammount of damage!"
			;

	static private readonly string BASTION =
		"<b>Bastion</b>\n" +
			"Rally allies within two squares of {0}, increasing their defence and resistance. Based on intelligence.\n" +
			"Cost 1 Mana"
			;
}
