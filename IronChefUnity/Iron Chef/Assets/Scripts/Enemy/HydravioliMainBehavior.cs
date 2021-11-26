using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HydravioliMainBehavior : EnemyBehaviorTree
{
    BehaviorTree hydravioliBehaviorTree;
    private Node CheckPlayer, CheckHurt, ImAggrod;


    [Header("Hydravioli Things")]
    public List<Transform> spawnLocations;
    public GameObject HeadPrefab;
    EnemyHitpoints myHP;
    List<GameObject> currentHeads;
    int currentHeadPlace = 0;

    public List<GameObject> BossWalls;

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
        ImAggrod = new Leaf("Become Aggrod", aggro);


        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, ImAggrod);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, ImAggrod);
        hydravioliBehaviorTree = new BehaviorTree(CheckPlayer, CheckHurt);



        myHP = GetComponent<EnemyHitpoints>();
        myHP.DeathEvents += BossOver;
    }

    private void Update()
    {
        hydravioliBehaviorTree.behavior();
    }

    public Node.STATUS overrideMoveTowards()
    {

        return Node.STATUS.SUCCESS;
    }

    Node.STATUS aggro()
    {
        if (!aggrod)
        {
            foreach (var go in BossWalls)
                go.SetActive(true);

            FindObjectOfType<PlayerHUDManager>().BossInfoOn("Italernean, The Hydra-violi", GetComponent<EnemyHitpoints>(), "");


            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace]));


            musicManager.combatCount++;
        }
        aggrod = true;
        ImAggrod.status = Node.STATUS.SUCCESS;
        return ImAggrod.status;
    }

    public void OnHeadKilled()
    {


        if (myHP.GetCurrentHP() > 1)
            Invoke("TwoNewHeads", 2f);
        else
            foreach (var head in currentHeads)
                if(head != null)
                    head.GetComponent<HydravioliHeadBehavior>().DeathNoNewHead();

        myHP.TakeDamage(1);
    }

    public void TwoNewHeads()
    {
        if (myHP.GetCurrentHP() > 0)
        {
            currentHeadPlace++;
            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace]));
            currentHeadPlace++;
            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace]));
        }
    }


    public void BossOver()
    {
        foreach (var go in BossWalls)
            go.SetActive(false);
        FindObjectOfType<PlayerHUDManager>().BossOver();
    }
}
