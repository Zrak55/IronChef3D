using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttackbox : MonoBehaviour
{
    //Copied and pasted from PlayerBasicAttackbox (with small edits)
    [Tooltip("Collider representing the enemies' attack range.")]
    public Collider attackbox;
    [HideInInspector] public bool CanHit = false;
    [HideInInspector] public List<PlayerHitpoints> playersHit;
    private EnemyAttackModifierController modifier;

    [SerializeField]
    public SoundEffectSpawner.SoundEffect sound = SoundEffectSpawner.SoundEffect.Cleaver;
    //No sound yet
    //public SoundEffectSpawner.SoundEffect soundEffect;
    //private SoundEffectSpawner sfx;

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
        Debug.Log(gameObject.name);
        var hits = IronChefUtils.GetCastHits(attackbox);
        foreach (var hit in hits)
        {
            var player = hit.GetComponentInParent<PlayerHitpoints>();
            if (player != null)
            {
                if (!playersHit.Contains(player))
                {
                    if (playersHit.Count < 1)
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


                        player.TakeDamage(dmgToDeal, sound);

                        
                    }
                }
            }
        }
    }
}
