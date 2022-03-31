using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrayProjectile : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (checkCollider(collision.collider))
        {
            DoCollideThings(collision.gameObject.GetComponentInParent<EnemyHitpoints>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkCollider(other))
        {
            DoCollideThings(other.GetComponentInParent<EnemyHitpoints>());
        }
    }

    private void DoCollideThings(EnemyHitpoints enemy)
    {
        enemy?.TakeDamage(damage);
        Destroy(gameObject);
    }

    public bool checkCollider(Collider other)
    {
        return (other.GetComponentInParent<EnemyHitpoints>() != null || other.gameObject.layer == LayerMask.NameToLayer("Terrain"));

    }
}
