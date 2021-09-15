using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttackbox : MonoBehaviour
{
    //This is simply a copy and paste of PlayerBasicAttackBox for the sake of consistency.
    public bool CanHit = false;
    public List<PlayerHitpoints> playersHit;
    //No sounds or modifiers yet.
    //private PlayerAttackModifierController modifier;
    //public SoundEffectSpawner.SoundEffect soundEffect;
    private SoundEffectSpawner sfx;

    public bool IsCleave = false;

    public float damage;

    bool hasPlayedSound = false;

    private void Awake()
    {
        playersHit = new List<PlayerHitpoints>();
        //sfx = FindObjectOfType<SoundEffectSpawner>();
    }
    private void Start()
    {
        //TODO: add modifiers and such.
        //modifier = GetComponentInParent<PlayerAttackModifierController>();
    }

    private void Update()
    {
        if (hasPlayedSound)
        {
            hasPlayedSound = CanHit;
        }
    }

    public void HitOn()
    {
        CanHit = true;
    }
    public void HitOff()
    {
        CanHit = false;
        playersHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        DoCollisionThings(other);
    }

    private void DoCollisionThings(Collider other)
    {
        if (CanHit)
        {
            var opponent = other.gameObject.GetComponentInParent<PlayerHitpoints>();
            if (opponent != null)
            {
                if (!playersHit.Contains(opponent))
                {
                    if (IsCleave || playersHit.Count < 1)
                    {
                        Debug.Log("Hit!");
                        playersHit.Add(opponent);

                        var dmgMod = 0f;
                        float dmgToDeal;
                        /*foreach (var mod in modifier.HitModifiers)
                        {
                            dmgMod += mod.damageIncrease;
                            if (mod.slowAmount > 0)
                            {
                                var oppSpeed = opponent.GetComponent<EnemySpeedController>();
                                IronChefUtils.AddSlow(oppSpeed, mod.slowAmount, mod.slowDuration, mod.slowName);


                            }
                        }*/
                        dmgToDeal = damage * (1 + dmgMod);

                        /*if (!hasPlayedSound)
                        {
                            hasPlayedSound = true;
                            if (sfx == null)
                            {
                                sfx = FindObjectOfType<SoundEffectSpawner>();
                            }
                            sfx.MakeSoundEffect(transform.position, soundEffect);
                        }*/


                        opponent.TakeDamage(dmgToDeal);
                        Debug.Log("Hurt by the enemy's attack!");
                        CanHit = false;
                    }
                }
            }
        }
    }
}
