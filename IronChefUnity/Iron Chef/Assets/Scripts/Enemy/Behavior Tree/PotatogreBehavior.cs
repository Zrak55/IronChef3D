using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PotatogreBehavior : EnemyBehaviorTree
{
    BehaviorTree tomatrollBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;

    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = FindObjectOfType<SoundEffectSpawner>();

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        CheckAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        CheckAttackRange = new Leaf("Player in Attack Range?", checkAttackRange);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackBasic = new Leaf("Attack", attackBasic);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveTowards);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAttackRange, AttackBasic);
        tomatrollBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }

    private void Update()
    {
        tomatrollBehaviorTree.behavior();
    }
}
