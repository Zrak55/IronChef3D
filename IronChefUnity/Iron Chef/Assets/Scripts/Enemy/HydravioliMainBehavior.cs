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

    public List<GameObject> spawningBubbles;

    bool didOriginatingThings = false;

    private void Start()
    {
        currentHeads = new List<GameObject>();

        setupWaypoints();
        setupEncounter();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", ALEX_OVERRIDE_SPAWN_RANGE);
        CheckAggroRange = new Leaf("Player in Aggro Range?", ALEX_OVERRIDE_AGGRO);
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

    public Node.STATUS ALEX_OVERRIDE_AGGRO()
    {
        CheckAggroRange.status = Node.STATUS.FAILURE;
        if (!aggrod)
        {
            FindObjectOfType<PlayerHitpoints>().StartBossCombat();
        }
        if(aggrod || Vector3.Distance(transform.position, player.transform.position) < aggroRange)
        {
            becomeAggro();
            aggrod = true;
            CheckAggroRange.status = Node.STATUS.SUCCESS;

        }
        return CheckAggroRange.status;
    }
    public Node.STATUS ALEX_OVERRIDE_SPAWN_RANGE()
    {
        CheckSpawnRange.status = Node.STATUS.FAILURE;
        if (aggrod || Vector3.Distance(transform.position, player.transform.position) < spawnRange)
        {
            CheckSpawnRange.status = Node.STATUS.SUCCESS;
        }
        else
        {
            becomeDeAggro();
        }
        return CheckSpawnRange.status;
    }

    Node.STATUS aggro()
    {
        if (!didOriginatingThings)
        {
            foreach (var go in BossWalls)
                go.SetActive(true);

            FindObjectOfType<PlayerHUDManager>().BossInfoOn("Italernean, The Hydra-violi", GetComponent<EnemyHitpoints>(), "");

            foreach(var b in spawningBubbles)
            {
                b.SetActive(false);
            }

            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace]));
            foreach(var s in FindObjectOfType<PlayerHUDManager>().spareSliders)
            {
                if(s.gameObject.activeSelf == false)
                {
                    s.gameObject.SetActive(true);
                    currentHeads[currentHeads.Count - 1].GetComponentInChildren<EnemyCanvas>().hpSlider = s;
                    break;
                }
            }

            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(spawnLocations[currentHeadPlace].position, SoundEffectSpawner.SoundEffect.HydraSpawnEffects);



            didOriginatingThings = true;
        }
        return becomeAggro();
    }

    public void OnHeadKilled()
    {


        if (myHP.GetCurrentHP() > 1)
            Invoke("TwoNewHeads", 2f);
        else
            foreach (var head in currentHeads)
                if(head != null)
                    head.GetComponent<HydravioliHeadBehavior>().DeathNoNewHead();

        myHP.TakeDamage(1, false);
    }

    public void TwoNewHeads()
    {
        if (myHP.GetCurrentHP() > 0)
        {
            currentHeadPlace++;
            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace])); 
            foreach (var s in FindObjectOfType<PlayerHUDManager>().spareSliders)
            {
                if (s.gameObject.activeSelf == false)
                {
                    s.gameObject.SetActive(true);
                    currentHeads[currentHeads.Count - 1].GetComponentInChildren<EnemyCanvas>().hpSlider = s;
                    break;
                }
            }
            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(spawnLocations[currentHeadPlace].position, SoundEffectSpawner.SoundEffect.HydraSpawnEffects);
            currentHeadPlace++;
            currentHeads.Add(Instantiate(HeadPrefab, spawnLocations[currentHeadPlace])); 
            foreach (var s in FindObjectOfType<PlayerHUDManager>().spareSliders)
            {
                if (s.gameObject.activeSelf == false)
                {
                    s.gameObject.SetActive(true);
                    currentHeads[currentHeads.Count - 1].GetComponentInChildren<EnemyCanvas>().hpSlider = s;
                    break;
                }
            }
            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(spawnLocations[currentHeadPlace].position, SoundEffectSpawner.SoundEffect.HydraSpawnEffects);
        }
    }


    public void BossOver()
    {
        foreach (var go in BossWalls)
            go.SetActive(false);
        FindObjectOfType<PlayerHUDManager>().BossOver();

        foreach (var b in spawningBubbles)
            Destroy(b);

        bool IDealtDamage = FindObjectOfType<PlayerHitpoints>().EndBossCombat();
        if (!IDealtDamage)
        {
            AchievementManager.UnlockAchievement("Give Me the Formuoli");
        }
    }
}
