using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttackbox : MonoBehaviour
{
    public bool CanHit = false;
    public List<PlayerHitpoints> playersHit;
    private EnemyAttackModifierController modifier;
    public SoundEffectSpawner.SoundEffect soundEffect;
    private SoundEffectSpawner sfx;
    public Collider myCollider;

    public bool IsCleave = false;

    public float damage;

    bool hasPlayedSound = false;

    private void Awake()
    {
        //There probably won't be multiple players, but keep this the same as playerBasicAttackbox for consistency's sake
        playersHit = new List<PlayerHitpoints>();
        sfx = FindObjectOfType<SoundEffectSpawner>();

    }
    private void Start()
    {
        modifier = GetComponentInParent<EnemyAttackModifierController>();
    }

    private void Update()
    {
        if (hasPlayedSound)
        {
            hasPlayedSound = CanHit;
        }


    }

    private void FixedUpdate()
    {
        if (CanHit)
        {
            DoCollisionThings();
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
    }

    private void DoCollisionThings()
    {
        var hits = IronChefUtils.GetCastHits(myCollider);
        foreach (var hit in hits)
        {
            var player = hit.GetComponentInParent<PlayerHitpoints>();
            if (player != null)
            {
                if (!playersHit.Contains(player))
                {
                    if (IsCleave || playersHit.Count < 1)
                    {
                        Debug.Log("Hit!");
                        playersHit.Add(player);

                        var dmgMod = 0f;
                        float dmgToDeal;
                        foreach (var mod in modifier.HitModifiers)
                        {
                            dmgMod += mod.damageIncrease;
                            if (mod.slowAmount > 0)
                            {
                                var oppSpeed = player.GetComponent<PlayerSpeedController>();
                                IronChefUtils.AddSlow(oppSpeed, mod.slowAmount, mod.slowDuration, mod.slowName);


                            }
                        }
                        dmgToDeal = damage * (1 + dmgMod);

                        //No sound yet
                        /*if (!hasPlayedSound)
                        {
                            hasPlayedSound = true;
                            if (sfx == null)
                            {
                                sfx = FindObjectOfType<SoundEffectSpawner>();
                            }
                            sfx.MakeSoundEffect(transform.position, soundEffect);
                        }*/


                        player.TakeDamage(dmgToDeal);
                    }
                }
            }
        }




        /*
        if (CanHit)
        {
            var opponent = other.gameObject.GetComponentInParent<EnemyHitpoints>();
            if (opponent != null)
            {
                if (!enemiesHit.Contains(opponent))
                {
                    if (IsCleave || enemiesHit.Count < 1)
                    {
                        Debug.Log("Hit!");
                        enemiesHit.Add(opponent);

                        var dmgMod = 0f;
                        float dmgToDeal;
                        foreach(var mod in modifier.HitModifiers)
                        {
                            dmgMod += mod.damageIncrease;
                            if(mod.slowAmount > 0)
                            {
                                var oppSpeed = opponent.GetComponent<EnemySpeedController>();
                                IronChefUtils.AddSlow(oppSpeed, mod.slowAmount, mod.slowDuration, mod.slowName);
                                

                            }
                        }
                        dmgToDeal = damage * (1 + dmgMod);

                        if(!hasPlayedSound)
                        {
                            hasPlayedSound = true;
                            if(sfx == null)
                            {
                                sfx = FindObjectOfType<SoundEffectSpawner>();
                            }
                            sfx.MakeSoundEffect(transform.position, soundEffect);
                        }
                        

                        opponent.TakeDamage(dmgToDeal);
                    }
                }
            }
        }
        */
    }
}
