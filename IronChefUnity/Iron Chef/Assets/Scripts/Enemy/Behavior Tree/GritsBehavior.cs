using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GritsBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree gritsBehaviorTree;
    private Node CheckPlayer, CheckHurt;
    private const float gritsBounceTime = 1.3f, gritsAnimTime = .5f;

    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        enemyCanvas = GetComponentInChildren<EnemyCanvas>(true);
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        CheckAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);

        //Setup sequence nodes and root
        CheckHurt = new Selector("Check Hurt Sequence", CheckEnemyHurt, CheckAggroRange);
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckHurt, MoveTowards);
        gritsBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt);
    }

    private void Update()
    {
        gritsBehaviorTree.behavior();
        if (Vector3.Distance(player.transform.position, transform.position) >= spawnRange && agent.velocity.magnitude == 0 && !enemyHitpoints.damaged)
            StopAllCoroutines();
    }

    public override Node.STATUS moveTowards()
    {
        //Music and sound effects
        if (!aggrod)
        {
            enemyCanvas.SwapState();
            StartCoroutine("Bounce");
            PlayerHitpoints.CombatCount++;
            aggrod = true;
        }
        

        //Movement calculations
        Vector3 midpoint = player.transform.position - transform.position;
        if (agent.enabled == true)
            agent.destination = transform.position + midpoint;

        //Animation
        animator.SetBool("isMoving", (agent.velocity.magnitude == 0) ? false : true);

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }

    private IEnumerator Bounce()
    {
        agent.enabled = false;
        yield return new WaitForSeconds(gritsAnimTime);
        transform.LookAt(player);
        agent.enabled = true;
        yield return new WaitForSeconds(gritsBounceTime);
        StartCoroutine("Bounce");
    }
}