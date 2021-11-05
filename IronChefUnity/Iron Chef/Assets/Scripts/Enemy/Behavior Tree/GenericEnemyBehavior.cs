using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemyBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;
    //Nodes for the behavior tree. Will be adding more later.

    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        //GetComponentInChildren assumes we set up future enemies based on this script similar to those we've already done
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = FindObjectOfType<SoundEffectSpawner>();

        //Setup leaf nodes
        EnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        PlayerSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        PlayerAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        PlayerAttackRange = new Leaf("Player in Attack Range?", checkPlayerAttackRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        Attack = new Leaf("Attack", attackBasic);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, Attack);
        genericBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        genericBehaviorTree.behavior();
    }
}
