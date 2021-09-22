using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitpoints : MonoBehaviour
{
    [Tooltip("Float representing the enemy's starting hitpoints.")]
    [SerializeField] private float MaxHP;
    [HideInInspector] public bool damaged = false;
    private float currentHP;

    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void TakeDamage(float amount)
    {
        //TODO: Check enemy's powers/status effects for taking damage
        
        currentHP -= amount;
        damaged = true;
        
        if(currentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        GetComponent<EnemyFoodDropper>().GiveFood();

        //TODO: Play death animation before deletion

        Destroy(gameObject);

    }
}
