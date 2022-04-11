using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FudgeSludgeBehavior : EnemyBehaviorTree
{
    // Start is called before the first frame update
    BehaviorTree fudgeSludgeBehaviorTree;
    [SerializeField] public GameObject sludge;
    private Node CheckPlayer, CheckHurt, CheckAttack;

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
        CheckAngleRange = new Leaf("Player in Attack Range?", checkAngleRange);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackBasic = new Leaf("Attack", attackBasic);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveTowards);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        fudgeSludgeBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }

    private void Update()
    {
        fudgeSludgeBehaviorTree.behavior();
        if (!simpleFlag && aggrod)
        {
            StartCoroutine("SpawnSludge");
            simpleFlag = true;
        }
        else if (!aggrod)
        {
            StopCoroutine("SpawnSludge");
            simpleFlag = false;
        }
    }

    private IEnumerator SpawnSludge()
    {
        Instantiate(sludge, transform.position + transform.forward * -.05f, transform.rotation);
        yield return new WaitForSeconds(3);
        StartCoroutine("SpawnSludge");
    }
}
