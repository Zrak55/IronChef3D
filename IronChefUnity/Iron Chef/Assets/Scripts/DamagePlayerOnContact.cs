using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        var hp = other.GetComponent<PlayerHitpoints>();
        if (hp == null)
            hp = other.GetComponentInParent<PlayerHitpoints>();
        if (hp != null)
            hp.TakeDamage(damage);
    }
}
