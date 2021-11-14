using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsobeanBehavior : EnemyBehaviorTree
{
    BehaviorTree isobeanBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;
    //Isobean is mostly done. Next step is adding attack animation to animator, and a projectileAttack to that. That should also fix the music.
    private void Start()
    {
        randomizeWayponts(15);

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = FindObjectOfType<SoundEffectSpawner>();

        //Setup leaf nodes
        CheckAttackRange = new Leaf("Player in Attack Range?", checkAttackRange);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackProjectileStill = new Leaf("Attack", attackProjectileStill);

        //Setup sequence nodes and root
        CheckAttack = new Sequence("Attack Sequence", CheckAttackRange, AttackProjectileStill);
        CheckPlayer = new Sequence("Move Sequence", MoveReset, CheckAttack);
        isobeanBehaviorTree = new BehaviorTree(CheckPlayer);
    }

    private void Update()
    {
        isobeanBehaviorTree.behavior();
    }
}
