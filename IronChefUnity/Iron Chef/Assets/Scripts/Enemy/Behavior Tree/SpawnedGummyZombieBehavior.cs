using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnedGummyZombieBehavior : EnemyBehaviorTree
{
    BehaviorTree gummyZombieBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;
    public SkinnedMeshRenderer myMesh;
    public List<Material> myMaterials;
    private const float invincibilityTime = .5f;

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
        CheckHurt = new Selector("Check Hurt Sequence", CheckEnemyHurt, CheckAggroRange);
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        gummyZombieBehaviorTree = new BehaviorTree(CheckPlayer, CheckHurt, CheckAttack);

        myMesh.material = myMaterials[Random.Range(0, myMaterials.Count)];

        StartCoroutine("InvincibilityOff");
        hideDamage = true;
    }

    private void Update()
    {
        gummyZombieBehaviorTree.behavior();
    }

    public void ImBossZombie()
    {
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyHitpoints.DeathEvents += NeccreammancerDeathEvent;
    }

    public void NeccreammancerDeathEvent()
    {
        FindObjectOfType<NeccreammancerBehavior>().ZombieIsKilled();
    }

    private IEnumerator InvincibilityOff()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }

}
