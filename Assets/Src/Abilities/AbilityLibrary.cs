﻿using UnityEngine;
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
		abilityLibrary.Add("Advice", typeof(Advice));
		abilityLibrary.Add("Aim for the Head", typeof(AimForTheHead));
		abilityLibrary.Add("ArmourPiercing", typeof(ArmourPiercing));
        abilityLibrary.Add("Backflip", typeof(Backflip));
		abilityLibrary.Add("Backstab", typeof(Backstab));
		abilityLibrary.Add("Bastion", typeof(Bastion));
		abilityLibrary.Add("Berserking", typeof(Berserking));
		abilityLibrary.Add("Blade Dancing", typeof(BladeDancing));
		abilityLibrary.Add("Blood Thirst", typeof(BloodThirst));
		abilityLibrary.Add("Block", typeof(Block));
		abilityLibrary.Add("Careful Aim", typeof(CarefulAim));
		abilityLibrary.Add("Charge", typeof(Charge));
        abilityLibrary.Add("Charm", typeof(Charm));
        abilityLibrary.Add("Cure", typeof(Cure));
		abilityLibrary.Add("Execute", typeof(Execute));
		abilityLibrary.Add("Explosion", typeof(Explosion));
		abilityLibrary.Add("Eye for an Eye", typeof(EyeForAnEye));
		abilityLibrary.Add("Feint", typeof(Feint));
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
		abilityLibrary.Add("Pin", typeof(Pin));
		abilityLibrary.Add("PinningStrike", typeof(PinningStrike));
		abilityLibrary.Add("Revenge", typeof(Revenge));
		abilityLibrary.Add("Riposte", typeof(Riposte));
        abilityLibrary.Add("Sap", typeof(Sap));
		abilityLibrary.Add("Shove", typeof(Shove));
		abilityLibrary.Add("Sing", typeof(Sing));
        abilityLibrary.Add("Sniper", typeof(Sniper));
        abilityLibrary.Add("Summon", typeof(Summon));
		abilityLibrary.Add("Spearpoint", typeof(Spearpoint));
		abilityLibrary.Add("Study", typeof(Study));
		abilityLibrary.Add("Teleport", typeof(Teleport));
		abilityLibrary.Add("Thievery", typeof(Thievery));
		abilityLibrary.Add("Transfusion", typeof(Transfusion));
		abilityLibrary.Add("Twist the Blade", typeof(TwistTheBlades));
		abilityLibrary.Add("Vampony", typeof(Vampony));
		abilityLibrary.Add("Wind Stance", typeof(WindStance));
		abilityLibrary.Add("Wings", typeof(Wings));
        abilityLibrary.Add("Wingslayer", typeof(Wingslayer));
        abilityLibrary.Add("Wormhole", typeof(Wormhole));
        abilityLibrary.Add("Wubs", typeof(Wubs));
	}

    public System.Type getTypeFromAbility(string s)
    {
        System.Type rv = abilityLibrary[s];
        return rv;
    }

	public bool Contains(string s)
	{
		return abilityLibrary.ContainsKey(s);
	}
}
