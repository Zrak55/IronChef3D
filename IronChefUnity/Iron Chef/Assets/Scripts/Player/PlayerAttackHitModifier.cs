using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitModifier
{
    public PlayerHitModName modName;

    public float damageIncrease;

    public float slowAmount;
    public float slowDuration;
    public SpeedEffector.EffectorName slowName;
    
    public float duration;

    public float critPercent = 0f;


    public enum PlayerHitModName
    {
        Fridge,
        CookingOil,
        Grill,
        Blender,
        Freezer
    }

    public virtual void DoSpecialModifier(EnemyHitpoints enemyHP, float damage)
    {
        
    }

    public virtual void SpecialTickAction()
    {

    }
}


