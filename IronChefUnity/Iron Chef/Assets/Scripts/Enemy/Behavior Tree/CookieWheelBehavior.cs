using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookieWheelBehavior : EnemyBehaviorTree
{
    BehaviorTree cookieWheelBehaviorTree;
    private Node CheckPlayer, CheckHurt;

    void Start()
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
        MoveInto = new Leaf("Move into player", moveInto);
        MoveReset = new Leaf("Reset Move", moveReset);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveInto);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveInto);
        cookieWheelBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt);
    }

    // Update is called once per frame
    void Update()
    {
        cookieWheelBehaviorTree.behavior();
    }
}
