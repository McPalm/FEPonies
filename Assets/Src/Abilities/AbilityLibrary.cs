using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityLibrary
{
    private Dictionary<string, System.Type> abilityLibrary = new Dictionary<string, System.Type>();
    static private AbilityLibrary instance;
    static public AbilityLibrary Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AbilityLibrary();
            }
            return instance;
        }
    }

    public AbilityLibrary()
    {
        abilityLibrary.Add("ArmourPiercing", typeof(ArmourPiercing));
        abilityLibrary.Add("Backflip", typeof(Backflip));
<<<<<<< HEAD
        abilityLibrary.Add("Bastion", typeof(Bastion));
        abilityLibrary.Add("Charge", typeof(Charge));
=======
		abilityLibrary.Add("Backstab", typeof(Backstab));
		abilityLibrary.Add("Bastion", typeof(Bastion));
		abilityLibrary.Add("Blade Dancing", typeof(BladeDancing));
		abilityLibrary.Add("Block", typeof(Block));
		abilityLibrary.Add("Charge", typeof(Charge));
>>>>>>> dee71704e70c6d067d00bcb81c6fa6b32e4d6b39
        abilityLibrary.Add("Charm", typeof(Charm));
        abilityLibrary.Add("Cure", typeof(Cure));
        abilityLibrary.Add("Explosion", typeof(Explosion));
        abilityLibrary.Add("Feint", typeof(Feint));
<<<<<<< HEAD
        abilityLibrary.Add("GravityField", typeof(GravityField));
        abilityLibrary.Add("Guard", typeof(Guard));
        abilityLibrary.Add("HealingSalve", typeof(HealingSalve));
        abilityLibrary.Add("Inspire", typeof(Inspire));
        abilityLibrary.Add("Lasso", typeof(Lasso));
        abilityLibrary.Add("MageSlayer", typeof(MageSlayer));
        abilityLibrary.Add("PinningStrike", typeof(PinningStrike));
=======
		abilityLibrary.Add("Follow Through", typeof(FollowThrough));
		abilityLibrary.Add("GravityField", typeof(GravityField));
		abilityLibrary.Add("Grit", typeof(Grit));
		abilityLibrary.Add("Guard", typeof(Guard));
        abilityLibrary.Add("HealingSalve", typeof(HealingSalve));
        abilityLibrary.Add("Inspire", typeof(Inspire));
        abilityLibrary.Add("Lasso", typeof(Lasso));
		abilityLibrary.Add("Lightning Reflexes", typeof(LightningReflexes));
		abilityLibrary.Add("MageSlayer", typeof(MageSlayer));
		abilityLibrary.Add("Momentum", typeof(Momentum));
		abilityLibrary.Add("PinningStrike", typeof(PinningStrike));
>>>>>>> dee71704e70c6d067d00bcb81c6fa6b32e4d6b39
        abilityLibrary.Add("Riposte", typeof(Riposte));
        abilityLibrary.Add("Sap", typeof(Sap));
        abilityLibrary.Add("Sing", typeof(Sing));
        abilityLibrary.Add("Sniper", typeof(Sniper));
        abilityLibrary.Add("Summon", typeof(Summon));
<<<<<<< HEAD
        abilityLibrary.Add("Teleport", typeof(Teleport));
        abilityLibrary.Add("Transfusion", typeof(Transfusion));
        abilityLibrary.Add("Vampony", typeof(Vampony));
        abilityLibrary.Add("Wings", typeof(Wings));
=======
		abilityLibrary.Add("Spearpoint", typeof(Spearpoint));
		abilityLibrary.Add("Teleport", typeof(Teleport));
		abilityLibrary.Add("Thievery", typeof(Thievery));
		abilityLibrary.Add("Transfusion", typeof(Transfusion));
		abilityLibrary.Add("Twist the Blade", typeof(TwistTheBlades));
		abilityLibrary.Add("Vampony", typeof(Vampony));
		abilityLibrary.Add("Wind Stance", typeof(WindStance));
		abilityLibrary.Add("Wings", typeof(Wings));
>>>>>>> dee71704e70c6d067d00bcb81c6fa6b32e4d6b39
        abilityLibrary.Add("Wingslayer", typeof(Wingslayer));
        abilityLibrary.Add("Wormhole", typeof(Wormhole));
        abilityLibrary.Add("Wubs", typeof(Wubs));
		abilityLibrary.Add("Backstab", typeof(Backstab));
		abilityLibrary.Add("Follow Through", typeof(FollowThrough));
		abilityLibrary.Add("Execute", typeof(Execute));
		abilityLibrary.Add("Lightning Reflexes", typeof(LightningReflexes));
		abilityLibrary.Add("Block", typeof(Block));
		abilityLibrary.Add("Spearpoint", typeof(Spearpoint));
	}

    public System.Type getTypeFromAbility(string s)
    {
        System.Type rv = abilityLibrary[s];
        return rv;
    }
}
