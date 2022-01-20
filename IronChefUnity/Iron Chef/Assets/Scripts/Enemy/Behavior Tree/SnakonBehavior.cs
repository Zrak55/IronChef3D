using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakonBehavior : EnemyBehaviorTree
{
    BehaviorTree snakonBehaviorTree;
    EnemyJump enemyJump;
    private Node JumpOnce, CheckPlayer, CheckHurt, CheckAttack;

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
        MoveTowards = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        AttackBasic = new Leaf("Attack", attackBasic);
        AttackSecondary = new Leaf("Jump", attackSecondary);
        RunOnce = new Leaf("Once", runOnce);

        //Setup sequence nodes and root
        JumpOnce = new Selector("Jump on Player Sight", RunOnce, AttackSecondary);
        CheckPlayer = new Sequence("Player Location Sequence", CheckSpawnRange, CheckAggroRange, JumpOnce, MoveTowards);
        CheckHurt = new Sequence("Check Hurt Sequence", CheckEnemyHurt, MoveTowards);
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackBasic);
        snakonBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }
    private void Update()
    {
        snakonBehaviorTree.behavior();
    }

    public override Node.STATUS attackSecondary()
    {
        if (!isAttackCD && AttackSecondary.status != Node.STATUS.RUNNING)
        {
            //Hopefully there will be another animation for this eventually.
            animator.SetTrigger("Jump");
            StartCoroutine("Jumping");
            AttackSecondary.status = Node.STATUS.RUNNING;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!isAttackCD)
                StartCoroutine("atttackCDEnd");
            AttackSecondary.status = Node.STATUS.SUCCESS;
        }
        return AttackSecondary.status;
    }
    
    private IEnumerator Jumping()
    {
        yield return new WaitForSeconds(.75f);
        enemyJump.BeginJumping(enemyJump.time);
    }
}
