using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitpoints : MonoBehaviour
{
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
    }

    public void TakeDamage(float amount)
    {
        //TODO: Check powers/status effects for taking damage

        playerStats.CurrentHP -= amount;

        if (playerStats.CurrentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //TODO: Say you lose, restart level, other things of that nature

        //TODO: Play death animation before deletion

    }
}
