using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemyBehavior : MonoBehaviour
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;
    [Tooltip("Transform for the object the enemy will follow, which is the player.")]
    [SerializeField] private Transform player;
    [Tooltip("Float for the maximum disatnce the enemy will begin to follow the player from.")]
    [SerializeField] private float aggroRange;
    [Tooltip("Float for the maximum disatnce the enemy will move from the spawn.")]
    [SerializeField] private float spawnRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float attackRange;
    [Tooltip("Animation clip for the enemy's attack animation.")]
    [SerializeField] private Animator attackAnim;
    private NavMeshAgent agent;
    private EnemyHitpoints enemyHitpoints;
    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, CheckAttack, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt, PlayerAttackRange, Attack;
    //The spawn location of the enemy is automatically set based on scene placement.
    private Vector3 startPosition;
    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false;

    private void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();

        //Setup leaf nodes
        EnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        PlayerSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        PlayerAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        PlayerAttackRange = new Leaf("Player in Attack Range?", checkPlayerAttackRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        ResetMove = new Leaf("Reset Move", movePause);
        Attack = new Leaf("Attack", attack);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, Attack);
        genericBehaviorTree = new BehaviorTree(ResetMove, CheckPlayer, CheckHurt, CheckAttack);
        genericBehaviorTree.printTree();
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        genericBehaviorTree.behavior();
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS moveTowards()
    {
        agent.destination = player.transform.position;
        return Node.STATUS.SUCCESS;
    }

    public Node.STATUS movePause()
    {
        agent.destination = startPosition;
        return Node.STATUS.SUCCESS;
    }

    public Node.STATUS attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Invoke("attackEnd", 1);

            attackAnim.Play("GenericLoafsterAttack");
            agent.destination = transform.position;

            if (isAttacking)
                return Node.STATUS.RUNNING;
            else
                return Node.STATUS.SUCCESS;
        }
        return Node.STATUS.FAILURE;
    }

    private void attackEnd()
    {
        isAttacking = false;
    }

    public Node.STATUS checkEnemyHurt()
    {
        if (enemyHitpoints.damaged)
            return Node.STATUS.SUCCESS;
        return Node.STATUS.FAILURE;
    }

    public Node.STATUS checkAggroRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < aggroRange)
            return Node.STATUS.SUCCESS;
        return Node.STATUS.FAILURE;
    }

    public Node.STATUS checkSpawnRange()
    {
        if (Vector3.Distance(player.transform.position, startPosition) < spawnRange)
            return Node.STATUS.SUCCESS;
        return Node.STATUS.FAILURE;
    }

    public Node.STATUS checkPlayerAttackRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
            return Node.STATUS.SUCCESS;
        return Node.STATUS.FAILURE;
    }
}
