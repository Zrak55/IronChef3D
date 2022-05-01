using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HydravioliHeadBehavior : EnemyBehaviorTree
{
    bool spawnNewHead = true;
    bool isSlamCD;
    bool isSweepCD;
    bool isBubbleSpitCD;

    bool alreadySlammingCD = false;
    bool alreadySweepingCD = false;
    bool alreadyBubbleSpittingCD = false;
    

    BehaviorTree hydravioliBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;

    private Node CheckGenericCooldown, BubbleSpit, Sweep, Slam;

    float currentCooldownDelay = 0f;

    [Header("Hydravioli Things")]
    public float SpawnInTime;
    public float SlamCDTime;
    public float SweepCDTime;
    public float BubbleSpitCDTime;
    public float GenericAttackCDTime;
    [Space]
    public Transform headTransform;

    private void Start()
    {

        setupWaypoints();

        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        BubbleSpit = new Leaf("Bubble", bubbleSpit);
        Sweep = new Leaf("Sweep", sweep);
        Slam = new Leaf("Slam", slam);

        CheckGenericCooldown = new Leaf("Player in Attack Range?", checkGenericCooldown);

        CheckAttack = new Sequence("Attack Sequence", CheckGenericCooldown, BubbleSpit, Sweep, Slam);
        hydravioliBehaviorTree = new BehaviorTree(CheckAttack);

        currentCooldownDelay = SpawnInTime + Random.Range(0, 4f);

        GetComponent<EnemyHitpoints>().DeathEvents += OnDeath;
    }

    private void Update()
    {
        hydravioliBehaviorTree.behavior();
    }



    Node.STATUS checkGenericCooldown()
    {
        if(currentCooldownDelay > 0)
        {
            CheckGenericCooldown.status = Node.STATUS.FAILURE;
            currentCooldownDelay -= Time.deltaTime;
            return Node.STATUS.FAILURE;
        }
        else
        {
            CheckGenericCooldown.status = Node.STATUS.SUCCESS;
            return Node.STATUS.SUCCESS;
        }
    }


    public void OnDeath()
    {
        if(spawnNewHead)
        {
            FindObjectOfType<HydravioliMainBehavior>().OnHeadKilled();
        }
    }
    public void DeathNoNewHead()
    {
        spawnNewHead = false;
        GetComponent<EnemyHitpoints>().Die();
    }

    Node.STATUS slam()
    {
        //If we aren't already attack and the cd is done, then attack.
        if (!isAttackCD && !isSlamCD && Slam.status != Node.STATUS.RUNNING)
        {
            isSlamCD = true;
            //Rotation
            transform.LookAt(player);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            animator.SetTrigger("Slam");
            Slam.status = Node.STATUS.RUNNING;
            alreadySlammingCD = false;
        }
        //When the attack animation has finished this will play.
        //This is a nifty little hack, the idle and moving animations will loop but attacks don't (for most things). So it won't work on some enemies (try tags instead?).
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if(!alreadySlammingCD)
            {
                alreadySlammingCD = true;
                StartCoroutine("slamCDEnd");
                StartCoroutine("genericCDEnd");
            }
            Slam.status = Node.STATUS.SUCCESS;
            //Don't forget to include hitOn and hitOff animator events. Otherwise put them here (and declare attackbox).
        }
        return Slam.status;
    }
    IEnumerator slamCDEnd()
    {
        yield return new WaitForSeconds(SlamCDTime);
        isSlamCD = false;
    }

    Node.STATUS sweep()
    {


        //If we aren't already attack and the cd is done, then attack.
        if (!isAttackCD && !isSweepCD && Sweep.status != Node.STATUS.RUNNING)
        {
            transform.rotation = transform.parent.rotation;
            isSweepCD = true;
            animator.SetTrigger("Sweep");
            animator.SetInteger("SweepNum", Random.Range(0, 2));
            Sweep.status = Node.STATUS.RUNNING;
            alreadySweepingCD = false;
        }
        //When the attack animation has finished this will play.
        //This is a nifty little hack, the idle and moving animations will loop but attacks don't (for most things). So it won't work on some enemies (try tags instead?).
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!alreadySweepingCD)
            {
                SoundEffectSpawner.soundEffectSpawner.MakeFollowingSoundEffect(headTransform, SoundEffectSpawner.SoundEffect.HydraSweep);
                alreadySweepingCD = true;
                StartCoroutine("sweepCDEnd");
                StartCoroutine("genericCDEnd");
            }
            Sweep.status = Node.STATUS.SUCCESS;
            //Don't forget to include hitOn and hitOff animator events. Otherwise put them here (and declare attackbox).
        }
        return Sweep.status;
    }
    IEnumerator sweepCDEnd()
    {
        yield return new WaitForSeconds(SweepCDTime);
        isSweepCD = false;
    }

    Node.STATUS bubbleSpit()
    {
        //If we aren't already attack and the cd is done, then attack.
        if (!isAttackCD && !isBubbleSpitCD && BubbleSpit.status != Node.STATUS.RUNNING)
        {
            isBubbleSpitCD = true;
            animator.SetTrigger("BubbleSpit");
            BubbleSpit.status = Node.STATUS.RUNNING;
            alreadyBubbleSpittingCD = false;
        }
        //When the attack animation has finished this will play.
        //This is a nifty little hack, the idle and moving animations will loop but attacks don't (for most things). So it won't work on some enemies (try tags instead?).
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!alreadyBubbleSpittingCD)
            {
                alreadyBubbleSpittingCD = true;
                StartCoroutine("bubbleSpitCDEnd");
                StartCoroutine("genericCDEnd");
            }
            BubbleSpit.status = Node.STATUS.SUCCESS;
            //Don't forget to include hitOn and hitOff animator events. Otherwise put them here (and declare attackbox).
        }
        return BubbleSpit.status;
    }
    IEnumerator bubbleSpitCDEnd()
    {
        yield return new WaitForSeconds(BubbleSpitCDTime);
        isBubbleSpitCD = false;
    }



    IEnumerator genericCDEnd()
    {
        isAttackCD = true;
        yield return new WaitForSeconds(GenericAttackCDTime);
        isAttackCD = false;
    }


}
