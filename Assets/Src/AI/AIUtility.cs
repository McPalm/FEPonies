using UnityEngine;
using UnityEngineInternal;
using System.Collections;

public class AIUtility
{ 
    public static void AITypeChanger(Unit u, AITypes changeTo)
    {
        switch(changeTo)
        {
            case AITypes.Aggressive:
                u.ChangeAI((IAIBehaviour)u.gameObject.AddComponent<Aggressive>());
                break;
            case AITypes.Defensive:
                u.ChangeAI((IAIBehaviour)u.gameObject.AddComponent<Defensive>());
                break;
            case AITypes.Stationary:
                u.ChangeAI((IAIBehaviour)u.gameObject.AddComponent<Stationary>());
                break;

        }
    }
}
