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

    public bool isFryingPan = false;
    public static int ExtraFryingPanBounces = 0;

    List<EnemyHitpoints> allhits;

    [Header("Bouncing")]
    public bool bounces = false;
    public int numBounces = 0;
    public float bounceRange = 0f;

    private int currentBounces = 0;

    // Start is called before the first frame update
    void Start()
    {
        allhits = new List<EnemyHitpoints>();

        if (bounces)
            multiHit = true;
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
            if (multiHit || !hit || bounces)
            {
                if(!allhits.Contains(enemyHealth))
                {
                    allhits.Add(enemyHealth);
                    hit = true;
                    SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, HitSound);
                    enemyHealth.TakeDamage(damage, false);
                    ApplyHitEffects();
                    int bounceMax = numBounces;
                    if(isFryingPan)
                    {
                        bounceMax += ExtraFryingPanBounces;
                    }
                    if (!multiHit || (bounces && currentBounces >= bounceMax))
                        Destroy(gameObject);

                    if(bounces)
                    {
                        currentBounces++;
                        var newBounce = GetBounceTarget(enemyHealth);
                        if(newBounce == null)
                        {
                            Destroy(gameObject);
                        }    
                        else
                        {
                            transform.LookAt(newBounce.transform);
                            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
                            FireProjectile(new Vector3(newBounce.transform.position.x, transform.position.y, newBounce.transform.position.z));
                        }

                    }
                }
            }
        }
        
        
    }

    private EnemyHitpoints GetBounceTarget(EnemyHitpoints currentHit, bool firstTry = true)
    {
        EnemyHitpoints bounceT = null;
        var cols = Physics.OverlapSphere(transform.position, bounceRange, 1 << LayerMask.NameToLayer("Enemy"));
        float dist = Mathf.Infinity;
        foreach (var e in cols)
        {
            var eh = e.GetComponent<EnemyHitpoints>();
            if (eh != null)
            {
                if (eh != currentHit && allhits.Contains(eh) == false)
                {
                    float nd = Vector3.Distance(transform.position, eh.transform.position);
                    if (nd < dist)
                    {
                        dist = nd;
                        bounceT = eh;
                    }
                }

            }
        }

        if(bounceT == null && firstTry)
        {
            allhits.Clear();
            allhits.Add(currentHit);
            bounceT = GetBounceTarget(currentHit, false);
        }

        return bounceT;
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
