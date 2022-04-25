using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PotatogreBehavior : EnemyBehaviorTree
{
    BehaviorTree potatogreBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack, CheckProjectile;

    private void Start()
    {
        setupWaypoints();
        setupEncounter();

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
        AttackProjectile = new Leaf("Projectile", attackProjectile);
        CheckDoubleRange = new Leaf("Player in Double Range?", checkDoubleRange);

        //Setup sequence nodes and root
        CheckProjectile = new Sequence("Projectile Selector", CheckDoubleRange, AttackProjectile);
        CheckHurt = new Selector("Check Hurt Sequence", CheckEnemyHurt, CheckAggroRange);
        CheckPlayer = new Sequence("Player Location Sequence", CheckHurt, CheckSpawnRange, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        potatogreBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckProjectile, CheckAttack);
    }

    private void Update()
    {
        potatogreBehaviorTree.behavior();
    }

    public override Node.STATUS attackProjectile()
    {
        //Rotation
        transform.LookAt(player);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        return base.attackProjectile();
    }

    public override Node.STATUS attackBasic()
    {
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            animator.SetInteger("AttackNum", Random.Range(0, 2));
            animator.SetTrigger("Attack");
            AttackBasic.status = Node.STATUS.RUNNING;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            if (!isAttackCD)
                StartCoroutine("atttackCDEnd");
            AttackBasic.status = Node.STATUS.SUCCESS;
        }
        return AttackBasic.status;
    }
}
