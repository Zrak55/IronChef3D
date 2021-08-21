using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackbox : MonoBehaviour
{
    public bool CanHit = false;
    public List<EnemyHitpoints> enemiesHit;

    public bool IsCleave = false;

    public float damage;

    private void Awake()
    {
        enemiesHit = new List<EnemyHitpoints>();
    }

    public void HitOn()
    {
        CanHit = true;
    }
    public void HitOff()
    {
        CanHit = false;
        enemiesHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CanHit)
        {
            var opponent = other.gameObject.GetComponentInParent<EnemyHitpoints>();
            if(opponent != null)
            {
                if(!enemiesHit.Contains(opponent))
                {
                    if(IsCleave || enemiesHit.Count < 1)
                    {
                        Debug.Log("Hit!");
                        enemiesHit.Add(opponent);

                        //TODO: Check for status effects for on hit things
                        opponent.TakeDamage(damage);
                    }
                }
            }
        }
    }
}
