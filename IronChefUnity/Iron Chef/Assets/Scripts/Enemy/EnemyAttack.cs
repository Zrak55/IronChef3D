using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Tooltip("Float for the range the enemy will attack the player in.")]
    [SerializeField] public float attackRange;
    private EnemyMove enemyMove;

    public void Awake()
    {
        enemyMove = gameObject.GetComponent<EnemyMove>();
    }

    public void Update()
    {
        if (enemyMove.isAggro == true && enemyMove.playerDistance < attackRange)
            meleeAttack();
    }

    public void meleeAttack()
    {

    }
}