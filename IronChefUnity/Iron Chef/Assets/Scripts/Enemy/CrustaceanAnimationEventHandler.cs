using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrustaceanAnimationEventHandler : MonoBehaviour
{
    public EnemyMultiAttackboxController behavior;

    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponentInParent<EnemyMultiAttackboxController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RightClawOn()
    {
        behavior.TurnHitboxOn(0);
    }

    public void LeftClawOn()
    {
        behavior.TurnHitboxOn(1);

    }
    public void RightClawOff()
    {
        behavior.TurnHitboxOff(0);
    }

    public void LeftClawOff()
    {
        behavior.TurnHitboxOff(1);

    }
}
