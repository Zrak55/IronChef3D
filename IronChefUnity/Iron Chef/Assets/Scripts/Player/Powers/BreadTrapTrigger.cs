using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadTrapTrigger : MonoBehaviour
{
    float stunTime;
    bool canCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(float Stun, float Radius)
    {
        GetComponent<SphereCollider>().radius = Radius;
        stunTime = Stun;
        canCollide = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided " + collision.gameObject.name);
        if (canCollide)
        {
            if (collision.gameObject.GetComponentInParent<EnemyStunHandler>() != null)
            {
                collision.gameObject.GetComponentInParent<EnemyStunHandler>().Stun(stunTime);
                FindObjectOfType<SoundEffectSpawner>().MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.BreadTrap);
                canCollide = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canCollide)
        {
            if (other.GetComponentInParent<EnemyStunHandler>() != null)
            {
                other.gameObject.GetComponentInParent<EnemyStunHandler>().Stun(stunTime);
                canCollide = false;
                FindObjectOfType<SoundEffectSpawner>().MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.BreadTrap);
                Destroy(gameObject);
            }
        }
    }
}
