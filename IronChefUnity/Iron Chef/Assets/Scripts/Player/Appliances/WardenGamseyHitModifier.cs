using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenGamseyHitModifier : PlayerAttackHitModifier
{
    public float stunChancePerDamage;
    public float stunTime;
    public override void DoSpecialModifier(EnemyHitpoints enemyHP, float damage)
    {
        base.DoSpecialModifier(enemyHP, damage);
        if(Random.Range(0f, 1f) < (stunChancePerDamage * damage))
        {
            enemyHP.GetComponent<EnemyStunHandler>().Stun(stunTime);
        }
    }
}
