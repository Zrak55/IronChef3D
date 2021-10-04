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

    private void Awake()
    {
        currentHP = MaxHP;
        mods = GetComponent<EnemyDamageTakenModifierController>();
    }

    public void TakeDamage(float amount)
    {
        amount = Mathf.Max(amount * mods.getMultiplier(), 0);

        Debug.Log(amount);
        
        currentHP -= amount;
        damaged = true;
        
        if(currentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        if(!imDead)
        {
            imDead = true;

            GetComponent<EnemyFoodDropper>().GiveFood();

            //TODO: Play death animation before deletion

            GetComponent<BenedictBehavior>()?.bossWall.SetActive(false);

            Destroy(gameObject);
        }
        

    }
}
