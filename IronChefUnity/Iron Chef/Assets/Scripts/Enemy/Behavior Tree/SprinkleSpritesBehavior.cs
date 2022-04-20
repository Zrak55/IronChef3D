using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SprinkleSpritesBehavior : EnemyBehaviorTree
{
    BehaviorTree sprinkleSpritesBehaviorTree;
    private Node CheckPlayer, CheckHurt;

    void Start()
    {
        setupWaypoints();
        setupEncounter();

        agent = GetComponent<NavMeshAgent>();
        agent.radius = .1f;
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        enemyBasicAttackbox = GetComponentInChildren<EnemyBasicAttackbox>();
        enemyCanvas = GetComponentInChildren<EnemyCanvas>(true);
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        CheckAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        MoveTowards = new Leaf("Move into player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);

        //Setup sequence nodes and root
        CheckHurt = new Selector("Check Hurt Sequence", CheckEnemyHurt, CheckAggroRange);
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckHurt, MoveTowards);
        sprinkleSpritesBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt);
    }

    // Update is called once per frame
    void Update()
    {
        sprinkleSpritesBehaviorTree.behavior();
    }

    public override Node.STATUS moveTowards()
    {
        if (!aggrod)
        {
            enemyCanvas.SwapState();
            PlayerHitpoints.CombatCount++;
            aggrod = true;
        }

        //Movement calculations
        agent.destination = player.position;
        if (!simpleFlag && (player.position - transform.position).magnitude < 5)
        {
            simpleFlag = true;
            animator.SetTrigger("Attack");
        }
        else
            simpleFlag = false;

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }
}
