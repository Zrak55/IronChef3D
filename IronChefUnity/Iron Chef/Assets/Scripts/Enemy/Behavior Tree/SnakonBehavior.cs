using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakonBehavior : EnemyBehaviorTree
{
    BehaviorTree snakonBehaviorTree;
    [Tooltip("Float for the maximum disatnce the enemy will begin to follow the player from.")]
    [SerializeField] private float aggroRange = 20;
    [Tooltip("Float for the maximum disatnce the enemy will move from the spawn.")]
    [SerializeField] private float spawnRange = 50;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from. (Should be changed to check if collision)")]
    [SerializeField] private float attackRange = 6;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float attackTime = 2;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float attackCD = 1;
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    private EnemyHitpoints enemyHitpoints;
    private EnemyBasicAttackbox enemyBasicAttackbox;
    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, CheckAttack, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt, PlayerAttackRange, Attack;
    //The spawn location of the enemy is automatically set based on scene placement.
    private Vector3 startPosition;
    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false, isAttackCD = false;

    MusicManager music;

    private void Start()
    {
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        enemyBasicAttackbox = GetComponent<EnemyBasicAttackbox>();
        if (enemyBasicAttackbox == null)
            enemyBasicAttackbox = GetComponentInChildren<EnemyBasicAttackbox>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;

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
        snakonBehaviorTree = new BehaviorTree(ResetMove, CheckPlayer, CheckHurt, CheckAttack);


        music = FindObjectOfType<MusicManager>();
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        snakonBehaviorTree.behavior();
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS moveTowards()
    {
        animator.SetBool("isMoving", true);

        Vector3 midpoint = (player.transform.position - transform.position);
        if (midpoint.magnitude < (attackRange / 3))
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

    public Node.STATUS attack()
    {
        //When the attack animation has finished this will play.
        if (Attack.status == Node.STATUS.RUNNING && !isAttacking)
        {
            isAttackCD = true;
            Invoke("attackCDEnd", attackCD);
            //This is in genericEnemyBehavior as a placeholder, it is now an animation event. So we will turn it off afterwards
            enemyBasicAttackbox.HitOff();

            Attack.status = Node.STATUS.SUCCESS;
            return Attack.status;
        }
        //If we aren't already attack and the cd is done, then attack.
        else if (!isAttacking && !isAttackCD)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            Invoke("attackEnd", attackTime);
            enemyBasicAttackbox.HitOff();
        }
        agent.destination = transform.position;
        Attack.status = Node.STATUS.RUNNING;
        return Attack.status;
    }

    private void attackEnd()
    {
        isAttacking = false;
    }

    private void attackCDEnd()
    {
        isAttackCD = false;
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
            if (!aggrod)
                music.combatCount++;
            aggrod = true;

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
        if (aggrod)
            music.combatCount--;
        aggrod = false;
        PlayerSpawnRange.status = Node.STATUS.FAILURE;
        return PlayerSpawnRange.status;
    }

    public Node.STATUS checkPlayerAttackRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
        {
            if (Vector3.Dot(player.position - transform.position, transform.forward) > .5f)
            {
                PlayerAttackRange.status = Node.STATUS.SUCCESS;
                return PlayerAttackRange.status;
            }
        }
        PlayerAttackRange.status = Node.STATUS.FAILURE;
        return PlayerAttackRange.status;
    }

    

    
}
