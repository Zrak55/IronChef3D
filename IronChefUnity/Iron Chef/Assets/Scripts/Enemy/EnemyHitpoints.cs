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
    bool imDead = false;
    float smallDmg = 0;
    bool isInvoking = false;
    EnemyCanvas floatingDmg;

    public GameObject DeathParticleSystem;
    public GameObject DeathParticle;

    private void Awake()
    {
        currentHP = MaxHP;
        mods = GetComponent<EnemyDamageTakenModifierController>();
        floatingDmg = GetComponentInChildren<EnemyCanvas>();
    }

    public void TakeDamage(float amount)
    {
        float dmgNumAmount = amount;

        mods.DoModifierSpecials(amount);

        amount = Mathf.Max(amount * mods.getMultiplier(), 0);
        if (amount > dmgNumAmount)
            dmgNumAmount = amount;

        currentHP -= amount;
        damaged = true;
        

        if(dmgNumAmount >= 1)
        {
            floatingDmg.MakeDamageNumber(amount);
        }
        else
        {
            smallDmg += amount;
            if(!isInvoking)
            {
                isInvoking = true;
                Invoke("SmallDmgDisplay", 1f);
            }
        }


        if(currentHP <= 0)
        {
            Die();
        }
    }

    void SmallDmgDisplay()
    {
        floatingDmg.MakeDamageNumber(smallDmg);
        
        smallDmg = 0;
        isInvoking = false;
    }

    public void Die()
    {
        if(!imDead)
        {
            imDead = true;

            GetComponent<EnemyFoodDropper>().GiveFood();

            if(GetComponent<EnemyBehaviorTree>().isAggrod())
            {
                FindObjectOfType<MusicManager>().combatCount--;
            }

            //TODO: Play death animation before deletion

            var dp = Instantiate(DeathParticleSystem, transform.position, Quaternion.identity);
            dp.GetComponent<EnemyDeathParticles>().MakeParticles(DeathParticle);

            CheckDeadBoss();

            Destroy(gameObject);
        }
        

    }

    public float GetPercentHP()
    {
        return currentHP / MaxHP;
    }

    private void CheckDeadBoss()
    {
        GetComponent<BenedictBehavior>()?.bossWall.SetActive(false);
        GetComponent<BenedictBehavior>()?.postBossPortal.SetActive(true);
        GetComponent<BenedictBehavior>()?.BossOver();


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
}
