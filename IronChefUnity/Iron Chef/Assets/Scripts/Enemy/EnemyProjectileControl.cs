using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileControl : MonoBehaviour
{
    [Tooltip("Float for how much damage the projectile does on contact with the player.")]
    [SerializeField] private float damage;
    [Tooltip("Float for how fast the projectile moves.")]
    [SerializeField] private float speed;
    private Vector3 currentPosition;
    private PlayerHitpoints playerHitpoints;
    [Tooltip("Audio to play on hit")]
    [SerializeField] private SoundEffectSpawner.SoundEffect sound;

    private void Update()
    {
        currentPosition = gameObject.transform.position;
        gameObject.transform.position += transform.forward * speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        playerHitpoints = other.gameObject.GetComponentInParent<PlayerHitpoints>();
        if (playerHitpoints != null)
        {
            Debug.Log("Hurt!");

            //TODO: Check for status effects for on hit things
            playerHitpoints.TakeDamage(damage);

            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }    
    }
}
