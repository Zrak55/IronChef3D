using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OnionKnightBehavior : EnemyBehaviorTree
{
    BehaviorTree onionKnightBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;
    private float speed, acceleration;

    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
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
        CheckAttackRange = new Leaf("Player in Attack Range?", checkAttackRange);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackTwo = new Leaf("Attack", attackTwo);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveTowards);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAttackRange, AttackTwo);
        onionKnightBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }

    private void Update()
    {
        onionKnightBehaviorTree.behavior();
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Parry"))
            invincible = true;
        else
            invincible = false;

        //Obviously, this code interacts unfavorably with the speed mods. Needs fix at some point.
        if (simpleFlag == false && aggrod == true)
        {
            Vector3 midpoint = player.transform.position - transform.position;
            if (midpoint.magnitude < 5)
                midpoint = Vector3.zero;
            agent.destination = transform.position + midpoint;
            agent.speed *= 1.5f;
            agent.acceleration *= 1.5f;
        }
        else
        {
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
        transform.LookAt(player);
        Vector3 midpoint = player.transform.position - transform.position;
        midpoint = midpoint.normalized * -(attackRange - midpoint.magnitude);
        agent.destination = (animator.GetCurrentAnimatorStateInfo(0).loop) ? (transform.position + midpoint) : transform.position;

        //Animation
        animator.SetBool("isMoving", Vector3.Distance(player.transform.position, currentWaypoint) > spawnRange && transform.position == currentWaypoint ? false : true);

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }
}
