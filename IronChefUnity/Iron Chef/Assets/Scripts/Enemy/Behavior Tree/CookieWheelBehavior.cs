using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CookieWheelBehavior : EnemyBehaviorTree
{
    BehaviorTree cookieWheelBehaviorTree;
    private Node CheckPlayer, CheckHurt;

    [Header("Cookie Wheel Thing")]
    public Transform cookie;

    void Start()
    {
        setupWaypoints();
        setupEncounter();

        agent = GetComponent<NavMeshAgent>();
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
        cookieWheelBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        cookieWheelBehaviorTree.behavior();
        cookie.Rotate(new Vector3(agent.velocity.magnitude * 60 * Time.deltaTime, 0, 0));
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
            enemyBasicAttackbox.HitOn();
        }
        else
        {
            simpleFlag = false;
            enemyBasicAttackbox.HitOff();
        }

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }
}
