using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrayProjectile : MonoBehaviour
{
    [HideInInspector]
    public float stunTime;

    private void OnCollisionEnter(Collision collision)
    {
        if (checkCollider(collision.collider))
        {
            DoCollideThings(collision.gameObject.GetComponentInParent<EnemyStunHandler>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkCollider(other))
        {
            DoCollideThings(other.GetComponentInParent<EnemyStunHandler>());
        }
    }

    private void DoCollideThings(EnemyStunHandler enemy)
    {
        enemy?.StunAdditive(stunTime);
        Destroy(gameObject);
    }

    public bool checkCollider(Collider other)
    {
        return (other.GetComponentInParent<EnemyStunHandler>() != null || other.gameObject.layer == LayerMask.NameToLayer("Terrain"));

    }
}
