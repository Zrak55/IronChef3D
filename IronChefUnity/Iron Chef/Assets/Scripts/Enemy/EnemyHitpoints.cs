using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitpoints : MonoBehaviour
{
    [Tooltip("Float representing the enemy's starting hitpoints.")]
    [SerializeField] private float MaxHP;
    [HideInInspector] public bool damaged = false;
    private EnemyDamageTakenModifierController mods;
    private float currentHP;
    [HideInInspector]
    public bool imDead = false;
    float smallDmg = 0;
    bool isInvoking = false;
    EnemyCanvas floatingDmg;


    public SoundEffectSpawner.SoundEffect deathSound;
    private EnemyBehaviorTree enemyBehaviorTree;


    public delegate void SpecialDeathActions();
    public event SpecialDeathActions DeathEvents;

    EnemyVFXController vfx;


    private void Awake()
    {

        currentHP = MaxHP;
        mods = GetComponent<EnemyDamageTakenModifierController>();
        floatingDmg = GetComponentInChildren<EnemyCanvas>();
        enemyBehaviorTree = GetComponent<EnemyBehaviorTree>();

        vfx = GetComponent<EnemyVFXController>();
        if(vfx == null)
        {
            vfx = GetComponentInChildren<EnemyVFXController>();
        }

    }

    public virtual void TakeDamage(float amount, bool wasCrit)
    {
        if(currentHP > 0)
        {
            float dmgNumAmount = amount;

            mods.DoModifierSpecials(amount);

            amount = Mathf.Max(amount * mods.getMultiplier(), 0);
            if (amount > dmgNumAmount)
                dmgNumAmount = amount;

            bool didInvincible = false;
            if (enemyBehaviorTree != null)
            {

                if (enemyBehaviorTree.invincible)
                {
                    didInvincible = true;
                    enemyBehaviorTree.counter();
                    dmgNumAmount = 0;
                    amount = 0;
                }
            }
            if (!didInvincible)
            {
                currentHP -= amount;
                if (!damaged)
                {
                    damaged = true;
                    if(enemyBehaviorTree != null)
                        enemyBehaviorTree.spawnRange *= 2;
                }

                if(amount >= 300)
                {
                    AchievementManager.UnlockAchievement("Brutal Eggs and Ham");
                }
            }

            if (dmgNumAmount >= 1)
            {
                floatingDmg.MakeDamageNumber(amount, wasCrit);
            }
            else
            {
                smallDmg += amount;
                if (!isInvoking && (!didInvincible || enemyBehaviorTree?.hideDamage == false))
                {
                    isInvoking = true;
                    Invoke("SmallDmgDisplay", 0.1f);
                }
            }


            if (currentHP <= 0)
            {
                Die();
            }
        }
        
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, MaxHP);
    }

    void SmallDmgDisplay()
    {
        floatingDmg.MakeDamageNumber(smallDmg, false);
        
        smallDmg = 0;
        isInvoking = false;
    }

    public void Die(bool spawnDeathParticles = true)
    {
        if(!imDead)
        {
            imDead = true;

            if(GetComponent<EnemyBehaviorTree>() != null)
            {
                if (GetComponent<EnemyBehaviorTree>().isAggrod())
                {
                    PlayerHitpoints.CombatCount--;
                }
            }

            GetComponent<EnemyFoodDropper>()?.GiveFood(spawnDeathParticles);

            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, deathSound);

            

            //CheckDeadBoss();

            vfx.SpawnDynamicEffect(0, transform.position);

            if(spawnDeathParticles)
                DeathEvents?.Invoke();

            Destroy(gameObject);
        }
        

    }

    public float GetPercentHP()
    {
        return currentHP / MaxHP;
    }

    private void CheckDeadBoss()
    {
        GetComponent<BenedictBehavior>()?.BossOver();
        GetComponent<MeatosaurusBehavior>()?.BossOver();
        GetComponent<HydravioliHeadBehavior>()?.OnDeath();
        GetComponent<HydravioliMainBehavior>()?.BossOver();
    }


    public float GetMax()
    {
        return MaxHP;
    }
    public void SetMax(float hp)
    {
        MaxHP = hp;
    }
    public void SetCurrent(float hp)
    {
        currentHP = MaxHP;
    }
    public float GetCurrentHP()
    {
        return currentHP;
    }
}
