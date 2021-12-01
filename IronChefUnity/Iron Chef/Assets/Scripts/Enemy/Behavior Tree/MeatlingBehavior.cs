using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeatlingBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree meatlingBehaviorTree;
    private Node CheckPlayer, CheckHurt;

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
        soundEffectSpawner = FindObjectOfType<SoundEffectSpawner>();

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        CheckAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        MoveBoss = new Leaf("Move towards boss", moveBoss);
        MoveReset = new Leaf("Reset Move", moveReset);

        //Setup sequence nodes and root
        meatlingBehaviorTree = new BehaviorTree(MoveBoss);
    }

    private void Update()
    {
        meatlingBehaviorTree.behavior();
        BossCollide();
    }

    private void BossCollide()
    {
        if (Vector3.Distance(transform.position, boss.transform.position) < 10)
        {
            EnemyHitpoints bossHitpoints = boss.GetComponent<EnemyHitpoints>();
            bossHitpoints.Heal(50);
            enemyHitpoints.Die();
        }
    }
}