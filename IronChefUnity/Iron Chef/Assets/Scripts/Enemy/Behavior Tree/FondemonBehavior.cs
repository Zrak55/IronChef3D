using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondemonBehavior : EnemyBehaviorTree
{
    BehaviorTree fondemonBehaviorTree;
    private Node CheckAttack, CheckPlayer;

    private void Start()
    {
        enemyProjectile = GetComponent<EnemyProjectile>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = FindObjectOfType<SoundEffectSpawner>();

        //Setup leaf nodes (Note: the fondemon must have attackAngle set to 0)
        StillReset = new Leaf("Don't move", stillReset);
        CheckAttackRange = new Leaf("Player in Attack Range?", checkAttackRange);
        AttackProjectile = new Leaf("Attack", attackProjectile);

        //Setup sequence nodes and root
        CheckAttack = new Sequence("Attack Sequence", CheckAttackRange, AttackProjectile);
        CheckPlayer = new Sequence("Still Sequence", StillReset, CheckAttack);
        fondemonBehaviorTree = new BehaviorTree(CheckPlayer);
    }

    private void Update()
    {
        fondemonBehaviorTree.behavior();
    }
}
