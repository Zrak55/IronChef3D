using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiAttackboxController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<EnemyBasicAttackbox> attacks;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitOn(int i)
    {
        if (i < attacks.Count)
        {
            attacks[i].HitOn();
        }
    }
    public void HitOff(int i)
    {
        if(i < attacks.Count)
        {
            attacks[i].HitOff();

        }
    }

    public void PlayAttackSound(int value)
    {
        GetComponentInParent<EnemyBehaviorTree>().playSound(value);
    }

    public void PlaySwingSound(int value)
    {
        attacks[value].PlaySwingSound();
    }
}
