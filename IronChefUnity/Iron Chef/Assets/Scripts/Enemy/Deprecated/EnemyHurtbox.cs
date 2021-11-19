using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    [Tooltip("Float for how much damage the projectile does on contact with the player.")]
    [SerializeField] private float damage;
    private PlayerHitpoints playerHitpoints;

    private void OnTriggerEnter(Collider other)
    {
        playerHitpoints = other.gameObject.GetComponentInParent<PlayerHitpoints>();
        if (playerHitpoints != null)
        {
            //TODO: Check for status effects for on hit things
            playerHitpoints.TakeDamage(damage);
        }
    }
}
