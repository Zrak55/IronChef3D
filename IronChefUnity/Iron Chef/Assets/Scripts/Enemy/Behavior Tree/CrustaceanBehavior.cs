using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrustaceanBehavior : EnemyBehaviorTree
{
    BehaviorTree crustaceanBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack, CheckShell;

    private void Start()
    {
        setupWaypoints();

        GetComponentInChildren<Outline>().enabled = false;

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
        CheckBehind = new Leaf("Player behind Enemy?", checkBehind);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackBasic = new Leaf("Attack", attackBasic);
        AttackSecondary = new Leaf("Hide in Shell", attackSecondary);

        //Setup sequence nodes and root
        CheckHurt = new Selector("Check Hurt Sequence", CheckEnemyHurt, CheckAggroRange);
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        CheckShell = new Sequence("Hiding Seqence", CheckBehind, AttackSecondary);
        crustaceanBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack, CheckShell);
    }

    private void Update()
    {
        crustaceanBehaviorTree.behavior();
    }

    public override Node.STATUS attackBasic()
    {
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            animator.SetInteger("AttackNum", Random.Range(0, 2));
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

    public override Node.STATUS attackSecondary()
    {
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            animator.SetInteger("AttackNum", 2);
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

    public override void counter()
    {
    }
}
