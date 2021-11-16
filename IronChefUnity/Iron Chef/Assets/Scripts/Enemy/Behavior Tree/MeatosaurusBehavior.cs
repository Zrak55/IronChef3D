using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeatosaurusBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;

    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float biteRange;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float BiteCD;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float biteTime;


    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float tailRange;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float TailCD;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float tailTime;


    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float ChargeCD;



    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float breathTime;


    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float stompTime;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float StompCD;


    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float roarTime;

    [Tooltip("Float for the time between the roar and the start of the next stomping cycle")]
    [SerializeField] private float RoarToNextStompCD;

    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, CheckAttack, CheckSpecial, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt, PlayerAttackRange, BiteAttack, TailAttack, ChargeAttack, StompAttack, BreathAttack, RoarAttack;
    //The spawn location of the enemy is automatically set based on scene placement.

    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false;

    private bool BiteOnCD = false, ChargeOnCD = false, TailOnCD = false, StompOnCD = false, CanBreath = false, CanRoar = false;
    bool genericCD = false;
    private bool InBiteRange = false, InTailRange = false, InChargeRange = false;


    private int numStomps;
    [Space]
    public int StompsBeforeBreath;

    bool shouldBreath, shouldRoar;

    [Space]
    public GameObject bossWallEnter;
    public GameObject bossWallExit;

    private MeatosaurusCharge chargeBehavior;
    private MeatosaurusStomp stompBehavior;
    private MeatosaurusBreath breathBehavior;
    private MeatosaurusRoar roarBehavior;

    private MusicManager music;

    private void Start()
    {


        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        enemyBasicAttackbox = GetComponent<EnemyBasicAttackbox>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        chargeBehavior = GetComponent<MeatosaurusCharge>();
        stompBehavior = GetComponent<MeatosaurusStomp>();
        breathBehavior = GetComponent<MeatosaurusBreath>();
        roarBehavior = GetComponent<MeatosaurusRoar>();

        //Setup leaf nodes
        EnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        PlayerSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        PlayerAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        PlayerAttackRange = new Leaf("Player in Attack Range?", checkPlayerAttackRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        ResetMove = new Leaf("Reset Move", movePause);
        BiteAttack = new Leaf("BiteAttack", biteAttack);
        TailAttack = new Leaf("TailAttack", tailAttack);
        ChargeAttack = new Leaf("ChargeAttack", chargeAttack);
        StompAttack = new Leaf("StompAttack", stompAttack);
        BreathAttack = new Leaf("BreathAttack", breathAttack);
        RoarAttack = new Leaf("RoarAttack", roarAttack);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        CheckSpecial = new Sequence("Special Sequence", StompAttack, BreathAttack, RoarAttack);
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, BiteAttack, TailAttack, ChargeAttack);
        genericBehaviorTree = new BehaviorTree(ResetMove, CheckPlayer, CheckHurt, CheckSpecial, CheckAttack);



        music = FindObjectOfType<MusicManager>();
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        genericBehaviorTree.behavior();
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS moveTowards()
    {
        animator.SetBool("isMoving", true);
        if(agent.enabled)
        {

            Vector3 midpoint = (player.transform.position - transform.position);
            if (midpoint.magnitude < (biteRange /1.5f))
                midpoint *= (0.05f / midpoint.magnitude);


            Vector3 target = transform.position + midpoint;

            agent.destination = target;
        }
        MoveTowardsPlayer.status = Node.STATUS.SUCCESS;



        return MoveTowardsPlayer.status;
    }

    public Node.STATUS movePause()
    {
        if(agent.enabled)
        {
            agent.destination = startPosition;
            if (agent.velocity == Vector3.zero)
                animator.SetBool("isMoving", false);
        }
        
        ResetMove.status = Node.STATUS.SUCCESS;
        return ResetMove.status;
    }



    public Node.STATUS biteAttack()
    {
        if(aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (BiteAttack.status == Node.STATUS.RUNNING && !isAttacking)   
            {
                BiteAttack.status = Node.STATUS.SUCCESS;
                return BiteAttack.status;
            }
            else if(BiteAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                BiteAttack.status = Node.STATUS.RUNNING;
                return BiteAttack.status;
            }
            else if (BiteOnCD || !InBiteRange)
            {
                BiteAttack.status = Node.STATUS.SUCCESS;
                return BiteAttack.status;
            }
            else
            {
                Invoke("BiteCDEnd", BiteCD + biteTime);
                BiteOnCD = true;
                isAttacking = true;
                animator.SetTrigger("Bite");
                Invoke("attackEnd", biteTime);

                agent.destination = transform.position;


                BiteAttack.status = Node.STATUS.RUNNING;
            }


            
        }
        
        return BiteAttack.status;
    }


    public Node.STATUS tailAttack()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (TailAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                TailAttack.status = Node.STATUS.SUCCESS;
                return TailAttack.status;
            }
            else if (TailAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                TailAttack.status = Node.STATUS.RUNNING;
                return TailAttack.status;
            }
            else if (TailOnCD || !InTailRange)
            {
                TailAttack.status = Node.STATUS.SUCCESS;
                return TailAttack.status;
            }
            else
            {
                Invoke("TailCDEnd", TailCD + tailTime);
                TailOnCD = true;
                isAttacking = true;
                animator.SetTrigger("Tail");
                Invoke("attackEnd", tailTime);

                agent.destination = transform.position;


                TailAttack.status = Node.STATUS.RUNNING;
            }



        }

        return TailAttack.status;
    }

    public Node.STATUS chargeAttack()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (ChargeAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                ChargeAttack.status = Node.STATUS.SUCCESS;
                return ChargeAttack.status;
            }
            else if (ChargeAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                ChargeAttack.status = Node.STATUS.RUNNING;
                return ChargeAttack.status;
            }
            else if (ChargeOnCD || !InChargeRange)
            {
                ChargeAttack.status = Node.STATUS.SUCCESS;
                return ChargeAttack.status;
            }
            else
            {
                ChargeOnCD = true;
                isAttacking = true;
                animator.SetBool("Charge", true);
                animator.SetTrigger("ChargeTrigger");
                

                agent.enabled = false;
                transform.LookAt(player);

                //Timing handled by charge script: unknown when charge needs to stop

                ChargeAttack.status = Node.STATUS.RUNNING;
            }



        }

        return ChargeAttack.status;
    }

    public Node.STATUS stompAttack()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (StompAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                StompAttack.status = Node.STATUS.SUCCESS;
                return StompAttack.status;
            }
            else if (StompAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                StompAttack.status = Node.STATUS.RUNNING;
                return StompAttack.status;
            }
            else if (StompOnCD)
            {
                StompAttack.status = Node.STATUS.SUCCESS;
                return StompAttack.status;
            }
            else
            {
                StompOnCD = true;
                isAttacking = true;
                animator.SetTrigger("Stomp");


                numStomps++;
                if(numStomps >= StompsBeforeBreath)
                {
                    numStomps = 0;
                    shouldBreath = true;
                    Invoke("StompCDEnd", stompTime + breathTime + roarTime + RoarToNextStompCD);

                }
                else
                {
                    Invoke("StompCDEnd", stompTime + StompCD);
                }

                Invoke("attackEnd", stompTime);

                StompAttack.status = Node.STATUS.RUNNING;
            }



        }

        return StompAttack.status;
    }

    public Node.STATUS breathAttack()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (BreathAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                BreathAttack.status = Node.STATUS.SUCCESS;
                return BreathAttack.status;
            }
            else if (BreathAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                BreathAttack.status = Node.STATUS.RUNNING;
                return BreathAttack.status;
            }
            else if (!shouldBreath)
            {
                BreathAttack.status = Node.STATUS.SUCCESS;
                return BreathAttack.status;
            }
            else
            {
                shouldBreath = false;
                shouldRoar = true;
                isAttacking = true;
                animator.SetTrigger("Breath");
                agent.enabled = false;

                Invoke("attackEnd", breathTime);
                Invoke("breathEnd", breathTime);

                BreathAttack.status = Node.STATUS.RUNNING;
            }



        }

        return BreathAttack.status;
    }


    public Node.STATUS roarAttack()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (RoarAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                RoarAttack.status = Node.STATUS.SUCCESS;
                return RoarAttack.status;
            }
            else if (RoarAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                RoarAttack.status = Node.STATUS.RUNNING;
                return RoarAttack.status;
            }
            else if (!shouldRoar)
            {
                RoarAttack.status = Node.STATUS.SUCCESS;
                return RoarAttack.status;
            }
            else
            {
                shouldRoar = false;
                isAttacking = true;
                animator.SetTrigger("Roar");

                agent.enabled = false;

                Invoke("attackEnd", roarTime);
                Invoke("roarEnd", roarTime);

                RoarAttack.status = Node.STATUS.RUNNING;
            }



        }

        return RoarAttack.status;
    }



    public void attackEnd()
    {
        isAttacking = false;
        agent.enabled = true;
        genericCD = true;
        Invoke("UndoGenericCD", 2f);
    }


    public void StartChargeCD()
    {
        Invoke("ChargeCDEnd", ChargeCD);
    }

    void UndoGenericCD()
    {
        genericCD = false;
    }

    private void BiteCDEnd()
    {
        BiteOnCD = false;
    }

    private void ChargeCDEnd()
    {
        ChargeOnCD = false;
    }

    private void TailCDEnd()
    {
        TailOnCD = false;
    }

    private void StompCDEnd()
    {
        StompOnCD = false;
    }
    
    private void breathEnd()
    {
        breathBehavior.StopBreathing();
    }
    private void stompEnd()
    {

    }
    private void roarEnd()
    {

    }

    public Node.STATUS checkEnemyHurt()
    {
        if (enemyHitpoints.damaged)
        {
            EnemyHurt.status = Node.STATUS.SUCCESS;
            return EnemyHurt.status;
        }
        EnemyHurt.status = Node.STATUS.FAILURE;
        return EnemyHurt.status;
    }

    public Node.STATUS checkAggroRange()
    {
        if(!aggrod)
        {
            PlayerAggroRange.status = Node.STATUS.FAILURE;
            
            if (Vector3.Distance(player.transform.position, transform.position) < aggroRange)
            {
                PlayerAggroRange.status = Node.STATUS.SUCCESS;
                aggrod = true;

                music.combatCount++;

                if (bossWallEnter.activeSelf == false)
                    bossWallEnter.SetActive(true);
                if (bossWallExit.activeSelf == false)
                    bossWallExit.SetActive(true);

                FindObjectOfType<PlayerHUDManager>().BossInfoOn("Beefcake, the Meatosaurus", GetComponent<EnemyHitpoints>(), "");
            }
        }
        else
        {
            PlayerAggroRange.status = Node.STATUS.SUCCESS;
        }
        
        return PlayerAggroRange.status;
    }

    public Node.STATUS checkSpawnRange()
    {
        if (Vector3.Distance(player.transform.position, startPosition) < spawnRange)
        {
            PlayerSpawnRange.status = Node.STATUS.SUCCESS;
            return PlayerSpawnRange.status;
        }
        PlayerSpawnRange.status = Node.STATUS.FAILURE;
        if (aggrod)
            music.combatCount--;
        aggrod = false;

        return PlayerSpawnRange.status;
    }


    public Node.STATUS checkPlayerAttackRange()
    {
        InBiteRange = false;
        InTailRange = false;
        InChargeRange = false;
        PlayerAttackRange.status = Node.STATUS.SUCCESS;

        if (Vector3.Distance(player.transform.position, transform.position) < biteRange)
        {
            InBiteRange = true;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < tailRange)
        {
            InTailRange = true;
        }
        if(Vector3.Angle(transform.forward, player.position - transform.position) <= 45)
        {
            InChargeRange = true;
        }
        if (genericCD)
        {
            PlayerAttackRange.status = Node.STATUS.FAILURE;
        }

        return PlayerAttackRange.status;
    }




    public void Tip(string s)
    {
        FindObjectOfType<PlayerHUDManager>().SetBossTip(s);

    }
    public void BossOver()
    {
        bossWallEnter.SetActive(false);
        bossWallExit.SetActive(false);
        FindObjectOfType<PlayerHUDManager>().BossOver();
    }

}
