using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerHitModifier : PlayerAttackHitModifier
{
    public int hitCount;
    public float damage;
    public int currentHits = 0;
    bool critReady;

    public override void OncePerAttackEffect(List<EnemyHitpoints> allHitEnemies)
    {
        base.OncePerAttackEffect(allHitEnemies);

        if(allHitEnemies.Count > 0)
        {
            currentHits++;
            if (currentHits >= hitCount)
            {
                SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(GameObject.FindObjectOfType<CharacterMover>().transform.position, soundEffect);
                foreach (var e in allHitEnemies)
                {
                    if (e != null)
                    {

                        e.TakeDamage(damage, false);
                    }
                }
                currentHits = 0;

            }
        }
        
    }


}
