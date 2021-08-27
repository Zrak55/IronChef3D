using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [Header("Projectile Movement")]
    public float speed;
    Vector3 direction;
    public float maxAllowedDistance;
    Vector3 start;
    float currentDistance;
    bool travelling = false;

    [Header("Projectile")]
    public float damage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Travel();
    }


    protected virtual void Travel()
    {
        if (travelling)
        {
            var oldTransform = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (direction * speed), speed * Time.deltaTime);
            currentDistance += (transform.position - oldTransform).magnitude;
            if (currentDistance > maxAllowedDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DoCollisionThings(other);
    }

    protected virtual void DoCollisionThings(Collider other)
    {
        var enemyHealth = other.gameObject.GetComponentInParent<EnemyHitpoints>();
        if(enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            ApplyHitEffects();
            Destroy(gameObject);
        }
    }

    protected virtual void FireProjectile(Vector3 target)
    {
        start = transform.position;
        direction = (target - start).normalized;
        currentDistance = 0;
        travelling = true;
    }

    protected virtual void ApplyHitEffects()
    {

    }

}
