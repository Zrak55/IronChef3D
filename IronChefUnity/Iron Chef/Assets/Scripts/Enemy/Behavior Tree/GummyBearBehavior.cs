using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GummyBearBehavior : EnemyBehaviorTree
{
    BehaviorTree gummyBearBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;
    public GameObject gummyObjects;
    public Transform gummySpawn1, gummySpawn2, gummySpawn3;

    public SkinnedMeshRenderer myMesh;
    public List<Material> myMaterials;

    private void Start()
    {
        setupWaypoints();
        setupEncounter();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        //Subscribe to delegate
        enemyHitpoints.DeathEvents += OnDeath;
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
        CheckHurt = new Selector("Check Hurt Sequence", CheckEnemyHurt, CheckAggroRange);
        CheckPlayer = new Sequence("Player Location Sequence", CheckHurt, CheckSpawnRange, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        gummyBearBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckAttack);

        myMesh.material = myMaterials[Random.Range(0, myMaterials.Count)];
    }

    private void Update()
    {
        gummyBearBehaviorTree.behavior();
    }

    public override Node.STATUS moveTowards()
    {
        animator.SetBool("IsAggrod", false);
        return base.moveTowards();
    }

    public override Node.STATUS moveReset()
    {
        animator.SetBool("IsAggrod", false);
        return base.moveReset();
    }

    public override Node.STATUS attackBasic()
    {
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            animator.SetInteger("AttackNum", Random.Range(1, 3));
            animator.SetTrigger("Attack");
            AttackBasic.status = Node.STATUS.RUNNING;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!isAttackCD)
                StartCoroutine("atttackCDEnd");
            AttackBasic.status = Node.STATUS.SUCCESS;
        }
        return AttackBasic.status;
    }

    private void OnDeath()
    {
        Instantiate(gummyObjects, gummySpawn1.position, new Quaternion());
        Instantiate(gummyObjects, gummySpawn2.position, new Quaternion());
        Instantiate(gummyObjects, gummySpawn3.position, new Quaternion());
    }
}
