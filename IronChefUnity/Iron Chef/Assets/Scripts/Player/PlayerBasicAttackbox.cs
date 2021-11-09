using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackbox : MonoBehaviour
{
    public bool CanHit = false;
    public List<EnemyHitpoints> enemiesHit;
    private PlayerAttackModifierController modifier;
    public SoundEffectSpawner.SoundEffect soundEffect;
    private SoundEffectSpawner sfx;
    public Collider myCollider;
    PlayerCamControl pcam;
    public float attackAnimTime;

    public bool IsCleave = false;

    public float damage;

    bool hasPlayedSound = false;

    private void Awake()
    {
        enemiesHit = new List<EnemyHitpoints>();
        sfx = FindObjectOfType<SoundEffectSpawner>();
        pcam = FindObjectOfType<PlayerCamControl>();

    }
    private void Start()
    {
        modifier = GetComponentInParent<PlayerAttackModifierController>();
    }

    private void Update()
    {
        if(hasPlayedSound)
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
        enemiesHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    private void DoCollisionThings()
    {
        var hits = IronChefUtils.GetCastHits(myCollider);
        foreach(var hit in hits)
        {
            var enemy = hit.GetComponentInParent<EnemyHitpoints>();
            if(enemy != null)
            {
                if (!enemiesHit.Contains(enemy))
                {
                    if (IsCleave || enemiesHit.Count < 1)
                    {
                        Debug.Log("Hit!");
                        enemiesHit.Add(enemy);

                        var dmgMod = 0f;
                        float dmgToDeal;
                        foreach (var mod in modifier.HitModifiers)
                        {
                            dmgMod += mod.damageIncrease;
                            if (mod.slowAmount > 0)
                            {
                                var oppSpeed = enemy.GetComponent<EnemySpeedController>();
                                IronChefUtils.AddSlow(oppSpeed, mod.slowAmount, mod.slowDuration, mod.slowName);


                            }

                            mod.playSound(transform.position);
                        }
                        dmgToDeal = damage * (1 + dmgMod);



                        bool hasCrit = false;
                        foreach (var h in modifier.HitModifiers)
                        {
                            if(h.critPercent > 0)
                            {
                                if (Random.Range(0f, 1f) <= h.critPercent)
                                {
                                    hasCrit = true;
                                    break;
                                }
                            }
                        }
                        if(hasCrit)
                        {
                            dmgToDeal *= 2;
                        }

                        foreach(var h in modifier.HitModifiers)
                        {
                            h.DoSpecialModifier(enemy, dmgToDeal);
                        }


                        pcam.ShakeCam(dmgToDeal/10f , dmgToDeal * 0.4f / 10f);
                        enemy.TakeDamage(dmgToDeal);


                        if (!hasPlayedSound)
                        {
                            hasPlayedSound = true;
                            if (sfx == null)
                            {
                                sfx = FindObjectOfType<SoundEffectSpawner>();
                            }
                            sfx.MakeSoundEffect(transform.position, soundEffect);
                        }
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
