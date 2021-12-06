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
    bool hit = false;

    [Header("Projectile")]
    public float damage;
    public bool multiHit = false;

    public SoundEffectSpawner.SoundEffect HitSound;
    public SoundEffectSpawner.SoundEffect FlightSound;

    List<EnemyHitpoints> allhits;


    // Start is called before the first frame update
    void Start()
    {
        allhits = new List<EnemyHitpoints>();
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
        if (enemyHealth != null)
        {
            if (multiHit || !hit)
            {
                if(!allhits.Contains(enemyHealth))
                {
                    allhits.Add(enemyHealth);
                    hit = true;
                    SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, HitSound);
                    enemyHealth.TakeDamage(damage);
                    ApplyHitEffects();
                    if (!multiHit)
                        Destroy(gameObject);
                }
            }
        }
        
        
    }

    public virtual void FireProjectile(Vector3 target)
    {
        start = transform.position;
        direction = (target - start).normalized;
        currentDistance = 0;
        travelling = true;
    }

    protected virtual void ApplyHitEffects()
    {

    }

    public void MakeFlightSound()
    {

        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, FlightSound);
    }

}
