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
        abilityLibrary.Add("BackupWeapon", typeof(BackupWeapon));
        abilityLibrary.Add("Bastion", typeof(Bastion));
        abilityLibrary.Add("Charge", typeof(Charge));
        abilityLibrary.Add("Charm", typeof(Charm));
        abilityLibrary.Add("Cure", typeof(Cure));
        abilityLibrary.Add("Explosion", typeof(Explosion));
        abilityLibrary.Add("Feint", typeof(Feint));
        abilityLibrary.Add("GravityField", typeof(GravityField));
        abilityLibrary.Add("Guard", typeof(Guard));
        abilityLibrary.Add("HealingSalve", typeof(HealingSalve));
        abilityLibrary.Add("Inspire", typeof(Inspire));
        abilityLibrary.Add("Lasso", typeof(Lasso));
        abilityLibrary.Add("MageSlayer", typeof(MageSlayer));
        abilityLibrary.Add("PinningStrike", typeof(PinningStrike));
        abilityLibrary.Add("Riposte", typeof(Riposte));
        abilityLibrary.Add("Sap", typeof(Sap));
        abilityLibrary.Add("Sing", typeof(Sing));
        abilityLibrary.Add("Sniper", typeof(Sniper));
        abilityLibrary.Add("Summon", typeof(Summon));
        abilityLibrary.Add("Teleport", typeof(Teleport));
        abilityLibrary.Add("Transfusion", typeof(Transfusion));
        abilityLibrary.Add("Vampony", typeof(Vampony));
        abilityLibrary.Add("Wings", typeof(Wings));
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
