using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedEnemyArcingProjectile : MonoBehaviour
{
    [Tooltip("Float for how much damage the projectile does on contact with the player.")]
    [SerializeField] private float damage;
    [Tooltip("Audio to play on hit")]
    [SerializeField] private SoundEffectSpawner.SoundEffect sound;
    private PlayerHitpoints playerHitpoints;
    [Tooltip("Bool representing if the object should destroy itself on hitting the ground")]
    [SerializeField] private bool destroyOnFloor = true;
    [Tooltip("Float representing amount of time it should destroy itself after, 0  means it won't")]
    [SerializeField] private float time = 0;

    [Space]
    [SerializeField] private GameObject OnHitEffect;


    private void Awake()
    {
        if (time != 0)
            Destroy(gameObject, time);
    }

    private void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        playerHitpoints = other.gameObject.GetComponentInParent<PlayerHitpoints>();
        if (playerHitpoints != null)
        {
            //TODO: Check for status effects for on hit things
            playerHitpoints.TakeDamage(damage);

            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, sound);

            if (OnHitEffect != null)
            {
                Destroy(Instantiate(OnHitEffect, transform.position, Quaternion.identity), 3f);
            }

            Destroy(gameObject);
        }

        if (destroyOnFloor || other.gameObject.name != "Floor")
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, sound);
                if (OnHitEffect != null)
                {
                    Destroy(Instantiate(OnHitEffect, transform.position, Quaternion.identity), 3f);
                }
                Destroy(gameObject);
            }
        }
    }


    public void Launch(Vector3 target, float time, float launchHeight)
    {
        StartCoroutine(flightTick(target, time, launchHeight));
    }

    private IEnumerator flightTick(Vector3 target, float time, float launchHeight)
    {
        float cTime = 0;
        float yOffset = -4 * launchHeight / Mathf.Pow(time, 2);
        Vector3 startPos = transform.position;
        while (cTime < time)
        {

            transform.position = startPos;
            transform.position = Vector3.Lerp(startPos, target, cTime / time);
            float yAmount = yOffset * cTime * (cTime - time);
            transform.position = new Vector3(transform.position.x, transform.position.y + yAmount, transform.position.z);

            yield return null;
            cTime += Time.deltaTime;
        }
        transform.position = target;

    }

}
