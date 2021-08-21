using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitpoints : MonoBehaviour
{
    public float MaxHP;
    [SerializeField]
    protected float currentHP;

    private void Awake()
    {
        currentHP = MaxHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        //TODO: Check powers/status effects for taking damage
        
        currentHP -= amount;
        
        if(currentHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        //TODO: Give ingredients

        //TODO: Play death animation before deletion

        Destroy(gameObject);

    }
}
