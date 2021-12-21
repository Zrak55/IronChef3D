using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeccreammancerEnemyHitpoints : EnemyHitpoints
{
    public override void TakeDamage(float amount)
    {
        if(amount >= GetCurrentHP())
        {
            amount = GetCurrentHP() - 0.1f;
            base.TakeDamage(amount);
            GetComponent<NeccreammancerBehavior>().SpawnPhylcatery();
        }
        else
        {
            base.TakeDamage(amount);
        }

    }
}
