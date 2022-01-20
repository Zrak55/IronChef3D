using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFXEventHandler : MonoBehaviour
{
    EnemyBehaviorTree behavior;
    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponentInParent<EnemyBehaviorTree>();
    }

    public void playSound(int value)
    {
        behavior.playSound(value);
    }
}
