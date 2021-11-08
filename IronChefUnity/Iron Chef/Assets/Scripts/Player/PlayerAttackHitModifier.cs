using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitModifier
{
    public PlayerHitModName modName;

    public float damageIncrease = 0f;

    public float slowAmount = 0f;
    public float slowDuration = 0f;
    public SpeedEffector.EffectorName slowName;
    
    public float duration;

    public float critPercent = -1f;


    public enum PlayerHitModName
    {
        Fridge,
        CookingOil,
        Grill,
        Blender,
        Freezer,
        WardenGamsey
    }

    public virtual void DoSpecialModifier(EnemyHitpoints enemyHP, float damage)
    {
        
    }

    public virtual void SpecialTickAction()
    {

    }
}


