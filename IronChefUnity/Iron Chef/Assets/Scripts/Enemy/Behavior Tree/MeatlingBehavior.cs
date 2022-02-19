using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeatlingBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree meatlingBehaviorTree;
    private Node CheckPlayer, CheckHurt;

    [Space]
    public float BossContactHeal = 50;

    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        boss = GameObject.Find("Meatosaurus");
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        MoveTowards = new Leaf("Move towards boss", moveTowards);

        //Setup sequence nodes and root
        meatlingBehaviorTree = new BehaviorTree(MoveTowards);
    }

    private void Update()
    {
        meatlingBehaviorTree.behavior();
        BossCollide();
    }

    public override Node.STATUS moveTowards()
    {
        //Music and sound effects
        if (!aggrod)
            musicManager.combatCount++;
        aggrod = true;

        //Movement calculations
        Vector3 midpoint = boss.transform.position - transform.position;
        if (midpoint.magnitude < attackRange && Vector3.Angle(transform.forward, boss.transform.position - transform.position) < attackAngle)
            midpoint = Vector3.zero;
        agent.destination = (animator.GetCurrentAnimatorStateInfo(0).loop) ? (transform.position + midpoint) : transform.position;

        //Animation
        animator.SetBool("isMoving", (agent.velocity.magnitude == 0) ? false : true);

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }

    private void BossCollide()
    {
        if (Vector3.Distance(transform.position, boss.transform.position) < 10)
        {
            if(enemyHitpoints.imDead == false)
            {
                EnemyHitpoints bossHitpoints = boss.GetComponent<EnemyHitpoints>();
                bossHitpoints.Heal(BossContactHeal);
                enemyHitpoints.Die();
            }
        }
    }
}