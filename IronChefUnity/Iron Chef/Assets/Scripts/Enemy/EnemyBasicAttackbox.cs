using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttackbox : MonoBehaviour
{
    //Copied and pasted from PlayerBasicAttackbox
    public bool CanHit = false;
    public List<PlayerHitpoints> playersHit;
    private EnemyAttackModifierController modifier;
    //No sound yet
    //public SoundEffectSpawner.SoundEffect soundEffect;
    //private SoundEffectSpawner sfx;
    public Collider myCollider;

    public bool IsCleave = false;

    public float damage;

    //bool hasPlayedSound = false;

    private void Awake()
    {
        playersHit = new List<PlayerHitpoints>();
        //sfx = FindObjectOfType<SoundEffectSpawner>();

    }
    private void Start()
    {
        modifier = GetComponentInParent<EnemyAttackModifierController>();
    }

    private void Update()
    {
        /*if (hasPlayedSound)
        {
            hasPlayedSound = CanHit;
        }*/


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
    }
}
