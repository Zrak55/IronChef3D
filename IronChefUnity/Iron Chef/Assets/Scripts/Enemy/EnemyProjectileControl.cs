using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileControl : MonoBehaviour
{
    [Tooltip("Float for how much damage the projectile does on contact with the player.")]
    [SerializeField] private float damage;
    [Tooltip("Float for how fast the projectile moves.")]
    [SerializeField] private float speed;
    [Tooltip("Audio to play on hit")]
    [SerializeField] private SoundEffectSpawner.SoundEffect sound;
    [Tooltip("Bool representing whether the projectile is angled or not")]
    [SerializeField] private bool isArc;
    private Vector3 currentPosition;
    private PlayerHitpoints playerHitpoints;



    private void Awake()
    {
        if(isArc)
        {
            Vector3 player = GameObject.Find("Player").transform.position;
            GetComponent<ProjectileLaunch>().Launch(speed * Mathf.Sqrt(Vector3.Distance(player, transform.position)), player - transform.position, 45);
        }
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (!isArc)
        {
            currentPosition = gameObject.transform.position;

            gameObject.transform.position += transform.forward * speed * Time.deltaTime;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerHitpoints = other.gameObject.GetComponentInParent<PlayerHitpoints>();
        if (playerHitpoints != null)
        {
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
