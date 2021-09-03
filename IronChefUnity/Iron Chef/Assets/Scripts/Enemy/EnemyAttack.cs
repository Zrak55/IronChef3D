using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Tooltip("Float for the range the enemy will attack the player in.")]
    [SerializeField] public float attackRange;
    [Tooltip("Float for the time between the enemy's attacks.")]
    [SerializeField] public float attackTime;
    //Once there's actual animation remove this. Use code like anim.GetCurrentAnimatorStateInfo(layer).length.
    [Tooltip("Float for the length of the attack animation.")]
    [SerializeField] public float time;
    [Tooltip("Float for how fast the enemy turns in place when targetting the enemy.")]
    [SerializeField] public float turnSpeed;
    private EnemyMove enemyMove;
    private Animator anim;
    private Quaternion currentRotation;

    public void Awake()
    {
        enemyMove = gameObject.GetComponent<EnemyMove>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyMove.playerDistance < attackRange)
        {
            enemyMove.movePause(true);
            currentRotation = Quaternion.LookRotation(enemyMove.moveTowards.position);
            transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, currentRotation, Time.deltaTime * turnSpeed);
        }
        if (enemyMove.playerDistance > attackRange)
        {
            enemyMove.movePause(false);
        }
    }
}