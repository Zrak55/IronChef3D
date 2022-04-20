using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakonBehavior : EnemyBehaviorTree
{
    BehaviorTree snakonBehaviorTree;
    EnemyJump enemyJump;
    private Node CheckPlayer, CheckHurt, CheckBack, CheckAttack, CheckJump;
    private bool isJumpCD = false;
    private const float snakonJumpAnimTime = 1.3f, snakonJumpBackTime = .3f;

    private void Start()
    {
        setupWaypoints();
        setupEncounter();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        enemyJump = GetComponent<EnemyJump>();
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
        CheckDoubleRange = new Leaf("Player in Double Range?", checkDoubleRange);
        CheckBehind = new Leaf("Player behind Enemy?", checkBehind);
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackBasic = new Leaf("Attack", attackBasic);
        AttackSecondary = new Leaf("Jump", attackSecondary);
        JumpBack = new Leaf("Jump Backwards", jumpBack);

        //Setup sequence nodes and root
        CheckHurt = new Selector("Check Hurt Sequence", CheckEnemyHurt, CheckAggroRange);
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        CheckJump = new Sequence("Jump Sequence", CheckDoubleRange, AttackSecondary);
        CheckBack = new Sequence("Back Jump Sequence", CheckBehind, JumpBack);
        snakonBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckBack, CheckJump, CheckAttack);
    }
    private void Update()
    {
        snakonBehaviorTree.behavior();
    }

    public override Node.STATUS attackBasic()
    {
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            animator.SetTrigger("Attack");
            StopCoroutine("Jumping");
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
        if (!isJumpCD && !isAttackCD && AttackSecondary.status != Node.STATUS.RUNNING)
        {
            simpleFlag = false;
            animator.SetTrigger("Jump");
            StartCoroutine("Jumping");
            AttackSecondary.status = Node.STATUS.RUNNING;
        }
        else if (simpleFlag == true && animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            StopCoroutine("Jumping");
            if (!isJumpCD)
                StartCoroutine("jumpCDEnd");
            AttackSecondary.status = Node.STATUS.SUCCESS;
        }
        return AttackSecondary.status;
    }

    public override Node.STATUS jumpBack()
    {
        if (!isAttackCD && JumpBack.status != Node.STATUS.RUNNING)
        {
            animator.SetTrigger("Back");
            enemyJump.BeginJumping(snakonJumpBackTime, (transform.forward * -attackRange) + transform.position);
        }
        else if (JumpBack.status == Node.STATUS.RUNNING)
            return JumpBack.status = Node.STATUS.SUCCESS;
        return JumpBack.status = Node.STATUS.RUNNING;
    }

    private IEnumerator Jumping()
    {
        agent.destination = transform.position;
        yield return new WaitForSeconds(snakonJumpAnimTime);
        simpleFlag = true;
        transform.LookAt(player);
        enemyJump.BeginJumping(enemyJump.time);
    }

    protected IEnumerator jumpCDEnd()
    {
        isJumpCD = true;
        yield return new WaitForSeconds(attackCD * 2);
        isJumpCD = false;
    }
}
