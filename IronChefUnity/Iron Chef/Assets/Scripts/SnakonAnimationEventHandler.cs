using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakonAnimationEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackWindupSoundEffect()
    {
        GetComponentInParent<EnemyBehaviorTree>().playAttackSound();
        //Uncomment this line and remove the line above, this is for testing purposes
        //GetComponentInParent<SnakonBehavior>().playAttackSound();
    }
}
