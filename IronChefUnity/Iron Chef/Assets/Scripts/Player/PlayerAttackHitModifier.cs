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
    
    public float duration = IronChefUtils.InfiniteDuration;

    public float critPercent = -1f;

    public SoundEffectSpawner.SoundEffect soundEffect;
    public bool shouldPlaySound = false;

    public enum PlayerHitModName
    {
        Fridge,
        CookingOil,
        Grill,
        Blender,
        Freezer,
        WardenGamsey
    }

    public void playSound(Vector3 position)
    {
        if (shouldPlaySound)
            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(position, soundEffect);
    }

    public virtual void DoSpecialModifier(EnemyHitpoints enemyHP, float damage)
    {
        
    }

    public virtual void SpecialTickAction()
    {

    }

    public virtual void OncePerAttackEffect(List<EnemyHitpoints> allHitEnemies)
    {

    }
}


