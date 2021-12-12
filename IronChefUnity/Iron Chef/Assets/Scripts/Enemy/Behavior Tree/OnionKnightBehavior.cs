using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OnionKnightBehavior : EnemyBehaviorTree
{
    BehaviorTree onionKnightBehaviorTree;
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
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        CheckAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        CheckAttackRange = new Leaf("Player in Attack Range?", checkAttackRange);
        MoveClose = new Leaf("Move towards player", moveClose);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackTwo = new Leaf("Attack", attackTwo);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveClose);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveClose);
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
        transform.LookAt(player);
    }
}
