using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatrollAnimationEventHandler : MonoBehaviour
{
    EnemyMultiAttackboxController attacks;

    // Start is called before the first frame update
    void Start()
    {
        attacks = GetComponentInParent<EnemyMultiAttackboxController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitOn(int i)
    {
        attacks.TurnHitboxOn(i);
    }
    public void HitOff(int i)
    {
        attacks.TurnHitboxOff(i);

    }
}
