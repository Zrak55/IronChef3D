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
    [HideInInspector] public float attackAnimTime;
    public float baseAttackAnimTime;

    public bool IsCleave = false;

    public float damage;

    bool hasPlayedSound = false;

    float baseDamage;
    public float damagePercentIncreaseEachHit = 0;
    public float scalarResetTime = 3f;
    public float maxPercentDamage = 3f;
    float currentScalarTimer = 0f;
    public float baseCritValue = 0f;

    CharacterMover myMover;

    public GameObject OnHitEffect;

    public static float GlobalCritPercent = 0f;

    private void Awake()
    {

        enemiesHit = new List<EnemyHitpoints>();
        sfx = SoundEffectSpawner.soundEffectSpawner;
        pcam = FindObjectOfType<PlayerCamControl>();

        baseDamage = damage;
        if (damagePercentIncreaseEachHit > 0)
            InvokeRepeating("UndoTimer", 0f, 0.05f);

        myMover = FindObjectOfType<CharacterMover>();

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

    public void UndoTimer()
    {
        if (currentScalarTimer > 0)
        {
            currentScalarTimer -= 0.05f;
            if (currentScalarTimer <= 0)
            {
                currentScalarTimer = 0;
                damage = baseDamage;
                PlayerHUDManager.PlayerHud.HideScalarBar();
            }
            PlayerHUDManager.PlayerHud.UpdateScalarBar(currentScalarTimer / scalarResetTime);
        }
    }

    private void FixedUpdate()
    {
        if (CanHit && myMover.IsRolling() == false)
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
        foreach(var mod in modifier.HitModifiers)
        {
            mod.OncePerAttackEffect(enemiesHit);
        }
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
                        enemiesHit.Add(enemy);

                        if(OnHitEffect != null)
                        {
                            Destroy(Instantiate(OnHitEffect, hit.GetComponent<Collider>().ClosestPoint(myCollider.bounds.center), Quaternion.identity), 3f);
                        }

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

                        bool wasCrit = false;

                        float critVal = baseCritValue;
                        foreach (var h in modifier.HitModifiers)
                        {
                            if (h.critPercent > 0)
                            {
                                critVal += h.critPercent;

                            }
                        }
                        critVal += GlobalCritPercent;
                        if(Random.Range(0.00001f, 1f) <= critVal)
                        {
                            dmgToDeal *= 2;
                            wasCrit = true;
                        }

                        foreach(var h in modifier.HitModifiers)
                        {
                            h.DoSpecialModifier(enemy, dmgToDeal);
                        }


                        pcam.ShakeCam(dmgToDeal/12.5f , .4f);
                        enemy.TakeDamage(dmgToDeal, wasCrit);


                        if (!hasPlayedSound)
                        {
                            hasPlayedSound = true;
                            if (sfx == null)
                            {
                                sfx = SoundEffectSpawner.soundEffectSpawner;
                            }
                            sfx.MakeSoundEffect(transform.position, soundEffect);
                        }

                        if(damagePercentIncreaseEachHit > 0)
                        {
                            damage =Mathf.Min(damage + baseDamage * damagePercentIncreaseEachHit, baseDamage * (1+maxPercentDamage));
                            
                            currentScalarTimer = scalarResetTime;
                            PlayerHUDManager.PlayerHud.ShowScalarBar();
                            PlayerHUDManager.PlayerHud.UpdateScalarText(damage / baseDamage);
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
                                sfx = SoundEffectSpawner.soundEffectSpawner;
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
