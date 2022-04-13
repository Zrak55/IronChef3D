using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortaterProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    float damage;
    float force;
    float launchAngle;
    float radius;

    bool hasSpawned = false;

    public GameObject ExplosionEffect;


    private void Awake()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (checkCollider(collision.collider))
        {
            DoCollideThings(collision.contacts[0].point);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkCollider(other))
        {
            DoCollideThings(other.transform.position);
        }
    }

    private void DoCollideThings(Vector3 point)
    {
        if (!hasSpawned)
        {
            hasSpawned = true;
            var area = IronChefUtils.GetCastHits(radius, transform.position);

            foreach (var obj in area)
            {
                var eh = obj.GetComponentInParent<EnemyHitpoints>();
                if (eh != null)
                {
                    eh.TakeDamage(damage);
                }
            }

            //SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.MalapenoExplosion);
            if(ExplosionEffect != null)
                Destroy(Instantiate(ExplosionEffect, transform.position, Quaternion.identity), 4.0f);

            Destroy(gameObject);

        }

    }

    public void SetData(float dmg, float power, float angle, float rad)
    {
        damage = dmg;
        force = power;
        launchAngle = angle;
        radius = rad;

        GetComponent<ProjectileLaunch>().Launch(force, transform.forward + IronChefUtils.GetSoftLockDirection(transform.forward, transform.position, 1 << LayerMask.NameToLayer("Enemy"), 20, true), angle);
    }

    public bool checkCollider(Collider other)
    {
        return (other.GetComponentInParent<PlayerStats>() == null && other.GetComponent<PlayerStats>() == null);

    }
}