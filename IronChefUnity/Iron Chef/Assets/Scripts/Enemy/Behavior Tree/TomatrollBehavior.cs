using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TomatrollBehavior : EnemyBehaviorTree
{
    BehaviorTree tomatrollBehaviorTree;
    private EnemyJump enemyJump;
    private Node CheckPlayer, CheckHurt, CheckAttack, CheckJump, CheckJumpBehind;
    private bool isJumpCD = false;
    private int attackNum;

    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        enemyJump = GetComponent<EnemyJump>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        CheckEnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        CheckSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        CheckAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        CheckAngleRange = new Leaf("Player in Attack Range?", checkAngleRange);
        CheckDoubleRange = new Leaf("Player in Jump Range?", checkDoubleRange);
        CheckBehind = new Leaf("Player Behind Enemy?", checkBehind);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackBasic = new Leaf("Attack", attackBasic);
        AttackSecondary = new Leaf("Jump", attackSecondary);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, MoveTowards);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        CheckJump = new Sequence("Jump Sequence", CheckDoubleRange, AttackSecondary);
        CheckJumpBehind = new Sequence("Jump Behind Sequence", CheckBehind, AttackSecondary);
        tomatrollBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckJump, CheckJumpBehind, CheckAttack);
    }

    private void Update()
    {
        tomatrollBehaviorTree.behavior();
    }

    public override Node.STATUS attackBasic()
    {
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            attackNum = Random.Range(0, 5);
            animator.SetInteger("AttackNum", attackNum);
            animator.SetTrigger("Attack");
            if (attackNum == 0)
                enemyJump.BeginJumping(enemyJump.time);
            AttackBasic.status = Node.STATUS.RUNNING;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (attackNum == 0 && !isJumpCD)
                StartCoroutine("jumpCdEnd");
            if (!isAttackCD)
                StartCoroutine("atttackCDEnd");
            AttackBasic.status = Node.STATUS.SUCCESS;
        }
        return AttackBasic.status;
    }

    public override Node.STATUS attackSecondary()
    {
        if (!isJumpCD && !isAttackCD && animator.GetCurrentAnimatorStateInfo(0).loop && AttackSecondary.status != Node.STATUS.RUNNING)
        {
            animator.SetInteger("AttackNum", 0);
            animator.SetTrigger("Attack");
            enemyJump.BeginJumping(enemyJump.time);
            AttackSecondary.status = Node.STATUS.RUNNING;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!isJumpCD)
                StartCoroutine("jumpCDEnd");
            AttackSecondary.status = Node.STATUS.SUCCESS;
        }
        return AttackSecondary.status;
    }

    protected IEnumerator jumpCDEnd()
    {
        isJumpCD = true;
        yield return new WaitForSeconds(attackCD * 2 + enemyJump.time);
        isJumpCD = false;
    }
}
