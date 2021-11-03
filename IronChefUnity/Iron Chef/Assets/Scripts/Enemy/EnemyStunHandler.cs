using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunHandler : MonoBehaviour
{
    float stunTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TickStun();
    }

    public bool IsStunned()
    {
        return stunTime > 0;
    }

    void TickStun()
    {
        if(stunTime > 0)
        {
            stunTime = Mathf.Max(stunTime -= Time.deltaTime, 0);
        }
    }

    public void Stun(float f)
    {
        if (stunTime < f)
            stunTime = f;
    }
    public void StunAdditive(float f)
    {
        stunTime += f;
    }
}
