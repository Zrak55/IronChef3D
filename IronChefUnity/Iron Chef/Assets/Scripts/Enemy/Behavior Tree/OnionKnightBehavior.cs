using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OnionKnightBehavior : EnemyBehaviorTree
{
    BehaviorTree onionKnightBehaviorTree;
    EnemySpeed enemySpeed;
    EnemySpeedController enemySpeedController;
    private Node CheckPlayer, CheckHurt, CheckAttack;
    private float speed, acceleration;

    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        enemySpeed = GetComponent<EnemySpeed>();
        enemySpeedController = GetComponent<EnemySpeedController>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;
        //Speed modifier
        speed = agent.speed;
        acceleration = agent.acceleration;

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        CheckAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        CheckAngleRange = new Leaf("Player in Attack Range?", checkAngleRange);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackBasic = new Leaf("Attack", attackBasic);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveTowards);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        onionKnightBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }

    private void Update()
    {
        onionKnightBehaviorTree.behavior();
        transform.LookAt(player);
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Parry"))
            invincible = true;
        else
            invincible = false;

        if (simpleFlag == false && aggrod == true)
        {
            enemySpeed.enabled = false;
            enemySpeedController.enabled = false;
            Vector3 midpoint = player.transform.position - transform.position;
            if (midpoint.magnitude < 5)
                midpoint = Vector3.zero;
            agent.destination = transform.position + midpoint;
            agent.speed = speed * 25;
            if (Vector3.Angle(agent.velocity, new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z)) > 100)
                agent.velocity = Vector3.zero;
            agent.acceleration = acceleration * 25;
        }
        else
        {
            enemySpeed.enabled = true;
            enemySpeedController.enabled = true;
            agent.speed = speed;
            agent.acceleration = acceleration;
        }
    }

    public override Node.STATUS moveTowards()
    {
        //Music and sound effects
        if (!aggrod)
            musicManager.combatCount++;
        aggrod = true;

        //Movement calculations
        Vector3 midpoint = player.transform.position - transform.position;
        midpoint = midpoint.normalized * -(attackRange - midpoint.magnitude - .5f);
        agent.destination = (animator.GetCurrentAnimatorStateInfo(0).loop) ? (transform.position + midpoint) : transform.position;

        //Animation
        animator.SetBool("isMoving", Vector3.Distance(player.transform.position, currentWaypoint) > spawnRange && transform.position == currentWaypoint ? false : true);

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }

    public override Node.STATUS attackBasic()
    {
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            animator.SetInteger("AttackNum", Random.Range(0, 2));
            animator.SetTrigger("Attack");
            AttackBasic.status = Node.STATUS.RUNNING;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!isAttackCD)
                StartCoroutine("atttackCDEnd");
            AttackBasic.status = Node.STATUS.SUCCESS;
        }
        return AttackBasic.status;
    }
}
