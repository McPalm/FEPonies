using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This is not used. No reason to use it
/// </summary>

public struct BattleInfo
{
    public int damage;
    public float hitChance;
    public float critChance;
    public bool willRetaliate;
}

public static class BattleSeer
{
    /// <summary>
	/// Calculates and returns the result of a fight between the two units.
	/// </summary>
	/// <param name="source">The attacker</param>
	/// <param name="target">The attacked</param>
	/// <param name="sourcePos">The position of the attacker when attacking</param>
    /// <param name="targetPos">The position of the target when attacking</param>
	/// <returns>a struct containing data about the attack</returns>
    public static BattleInfo Checkfight(Unit source, Unit target, Tile sourcePos = null, Tile targetPos = null)
    {
        BattleInfo rv = new BattleInfo();
        if (sourcePos == null)
        {
            sourcePos = source.Tile;
        }
        if (targetPos == null)
        {
            targetPos = target.Tile;
        }
        Stats sourceStats = source.GetStatsAt(sourcePos, target, targetPos);
        Stats targetStats = target.GetStatsAt(targetPos, target, targetPos);
        rv.hitChance = sourceStats.Hit - targetStats.Dodge;
        rv.critChance = sourceStats.crit - targetStats.critDodge;
        rv.willRetaliate = canRetaliate(source, target, sourcePos, targetPos);
        if (source.AttackInfo.effect.damageType.Normal)
        {
 ///           rv.damage=sourceStats.
        }
        return rv;
    }

    public static bool canRetaliate(Unit source, Unit target, Tile sourcePos = null, Tile targetPos = null)
    {
        if (sourcePos == null)
        {
            sourcePos = source.Tile;
        }
        if (targetPos == null)
        {
            targetPos = target.Tile;
        }
        if (target.retaliationsLeft <= 0)
        {
            return false;
        }
        else if (target.AttackInfo.CanAttack(target, targetPos, sourcePos))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}