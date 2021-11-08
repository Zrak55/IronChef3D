using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Outdated script. Attackbox controller now contains all functionality
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
        GetComponentInParent<EnemyBehaviorTree>().playSound(0);
    }
}
