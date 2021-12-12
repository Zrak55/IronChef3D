using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PotatogreBehavior : EnemyBehaviorTree
{
    BehaviorTree potatogreBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack, CheckProjectile;

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
        CheckAngleRange = new Leaf("Player in Attack Range?", checkAngleRange);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackTwo = new Leaf("Attack", attackTwo);
        AttackProjectile = new Leaf("Projectile", attackProjectile);
        RunOnce = new Leaf("Once", runOnce);

        //Setup sequence nodes and root
        CheckProjectile = new Selector("Projectile Selector", RunOnce, AttackProjectile);
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveTowards, CheckProjectile);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackTwo);
        potatogreBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }

    private void Update()
    {
        potatogreBehaviorTree.behavior();
    }
}
