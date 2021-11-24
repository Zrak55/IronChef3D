using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HydravioliMainBehavior : EnemyBehaviorTree
{
    BehaviorTree hydravioliBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;


    [Header("Hydravioli Things")]
    public List<Transform> spawnLocations;
    public GameObject HeadPrefab;
    EnemyHitpoints myHP;
    List<GameObject> currentHeads;
    int currentHeadPlace = 0;


    private void Start()
    {
        currentHeads = new List<GameObject>();

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
        MoveTowards = new Leaf("Move towards player", overrideMoveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackTwo = new Leaf("Attack", attackTwo);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveTowards);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAttackRange);
        hydravioliBehaviorTree = new BehaviorTree(CheckPlayer, CheckHurt, CheckAttack);


        currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace]));
    }

    private void Update()
    {
        hydravioliBehaviorTree.behavior();
    }

    public Node.STATUS overrideMoveTowards()
    {

        return Node.STATUS.SUCCESS;
    }



    public void OnHeadKilled()
    {


        if (myHP.GetCurrentHP() > 1)
            Invoke("TwoNewHeads", 2f);
        else
            foreach (var head in currentHeads)
                head.GetComponent<HydravioliHeadBehavior>().DeathNoNewHead();

        myHP.TakeDamage(1);
    }

    public void TwoNewHeads()
    {
        if (myHP.GetCurrentHP() > 1)
        {
            currentHeadPlace++;
            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace]));
            currentHeadPlace++;
            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace]));
        }
    }
}
