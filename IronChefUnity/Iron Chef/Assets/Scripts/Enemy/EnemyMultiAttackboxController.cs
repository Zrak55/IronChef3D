using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMultiAttackboxController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<EnemyBasicAttackbox> attacks;
    public EnemyBehaviorTree enemyBehaviorTree;
    void Start()
    {
        enemyBehaviorTree = GetComponentInParent<EnemyBehaviorTree>();
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

    public void InvincibilityOn()
    {
        enemyBehaviorTree.invincible = true;
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.Parry);
    }

    public void InvincibilityOff()
    {
        enemyBehaviorTree.invincible = false;
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
