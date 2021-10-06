using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GritsBehavior : MonoBehaviour
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;
    [Tooltip("Float for the maximum disatnce the enemy will begin to follow the player from.")]
    [SerializeField] private float aggroRange;
    [Tooltip("Float for the maximum disatnce the enemy will move from the spawn.")]
    [SerializeField] private float spawnRange;
    [Tooltip("Float for the maximum distance the enemy will slow moving from.")]
    [SerializeField] private float moveRange;
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    private EnemyHitpoints enemyHitpoints;
    private EnemyBasicAttackbox enemyBasicAttackbox;
    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt;
    //The spawn location of the enemy is automatically set based on scene placement.
    private Vector3 startPosition;
    //Ensure the enemy doesn't start a new attack in the middle of an old one.

    private void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        enemyBasicAttackbox = GetComponent<EnemyBasicAttackbox>();
        if (enemyBasicAttackbox == null)
            enemyBasicAttackbox = GetComponentInChildren<EnemyBasicAttackbox>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;

        //Setup leaf nodes
        EnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        PlayerSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        PlayerAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        ResetMove = new Leaf("Reset Move", movePause);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        genericBehaviorTree = new BehaviorTree(ResetMove, CheckPlayer, CheckHurt);
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        genericBehaviorTree.behavior();
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS moveTowards()
    {
        animator.SetBool("isMoving", true);

        Vector3 midpoint = (player.transform.position - transform.position);
        if (midpoint.magnitude < (moveRange/2)) 
            midpoint *= (0.05f / midpoint.magnitude);


        Vector3 target = transform.position + midpoint;

        agent.destination = target;
        MoveTowardsPlayer.status = Node.STATUS.SUCCESS;
        return MoveTowardsPlayer.status;
    }

    public Node.STATUS movePause()
    {
        agent.destination = startPosition;
        if (agent.velocity == Vector3.zero)
            animator.SetBool("isMoving", false);
        ResetMove.status = Node.STATUS.SUCCESS;
        return ResetMove.status;
    }

   

   
    public Node.STATUS checkEnemyHurt()
    {
        if (enemyHitpoints.damaged)
        {
            EnemyHurt.status = Node.STATUS.SUCCESS;
            return EnemyHurt.status;
        }
        EnemyHurt.status = Node.STATUS.FAILURE;
        return EnemyHurt.status;
    }

    public Node.STATUS checkAggroRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < aggroRange)
        {
            PlayerAggroRange.status = Node.STATUS.SUCCESS;
            return PlayerAggroRange.status;
        }
        PlayerAggroRange.status = Node.STATUS.FAILURE;
        return PlayerAggroRange.status;
    }

    public Node.STATUS checkSpawnRange()
    {
        if (Vector3.Distance(player.transform.position, startPosition) < spawnRange)
        {
            PlayerSpawnRange.status = Node.STATUS.SUCCESS;
            return PlayerSpawnRange.status;
        }
        PlayerSpawnRange.status = Node.STATUS.FAILURE;
        return PlayerSpawnRange.status;
    }


    

    
}
