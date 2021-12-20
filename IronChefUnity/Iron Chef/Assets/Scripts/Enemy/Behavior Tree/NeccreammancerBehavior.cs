using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeccreammancerBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;

    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float frostboltRange;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float frostboltCD;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float frostboltTime;

    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, SpecialSequence, CheckAttack, CheckRise, CheckPhylactery, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt, PlayerAttackRange, FrostboltAttack;
    //The spawn location of the enemy is automatically set based on scene placement.

    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false;

    private bool FrostboltOnCD = false;
    bool genericCD = false;
    private bool InFrostboltRange = false;



    [Space]
    public GameObject bossWallEnter;
    public GameObject bossWallExit;


    private MusicManager music;


    public EnemyProjectile FB;

    private void Start()
    {


        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;

        //Setup leaf nodes
        EnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        PlayerSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        PlayerAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        PlayerAttackRange = new Leaf("Player in Attack Range?", checkPlayerAttackRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        ResetMove = new Leaf("Reset Move", movePause);
        FrostboltAttack = new Leaf("FrostboltAttack", frostboltAttack);

        CheckPhylactery = new Leaf("Phylactery", phylcateryCheck);
        CheckRise = new Leaf("Rise", riseCheck);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, FrostboltAttack);
        SpecialSequence = new Sequence("Special Abilities", CheckPhylactery, CheckRise);
        genericBehaviorTree = new BehaviorTree(ResetMove, CheckPlayer, CheckHurt, SpecialSequence, CheckAttack);



        music = FindObjectOfType<MusicManager>();


        GetComponent<EnemyHitpoints>().DeathEvents += BossOver;
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
            if (midpoint.magnitude < (frostboltRange /1.5f))
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



    public Node.STATUS frostboltAttack()
    {
        if(aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (FrostboltAttack.status == Node.STATUS.RUNNING && !isAttacking)   
            {
                FrostboltAttack.status = Node.STATUS.SUCCESS;
                return FrostboltAttack.status;
            }
            else if(FrostboltAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                FrostboltAttack.status = Node.STATUS.RUNNING;
                return FrostboltAttack.status;
            }
            else if (FrostboltOnCD || !InFrostboltRange)
            {
                FrostboltAttack.status = Node.STATUS.SUCCESS;
                return FrostboltAttack.status;
            }
            else
            {
                Invoke("FrostboltCDEnd", frostboltCD + frostboltTime);
                FrostboltOnCD = true;
                isAttacking = true;
                animator.SetTrigger("Frostbolt");
                Invoke("attackEnd", frostboltTime);



                FrostboltAttack.status = Node.STATUS.RUNNING;
            }


            
        }
        
        return FrostboltAttack.status;
    }



    public void attackEnd()
    {
        isAttacking = false;
        agent.enabled = true;
        genericCD = true;
        Invoke("UndoGenericCD", 2f);
    }



    void UndoGenericCD()
    {
        genericCD = false;
    }

    private void FrostboltCDEnd()
    {
        FrostboltOnCD = false;
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

                FindObjectOfType<PlayerHUDManager>().BossInfoOn("The Ice Nec-Cream-Mancer", GetComponent<EnemyHitpoints>(), "");
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
        InFrostboltRange = false;
        PlayerAttackRange.status = Node.STATUS.SUCCESS;

        if (Vector3.Distance(player.transform.position, transform.position) < frostboltRange)
        {
            InFrostboltRange = true;
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


    public Node.STATUS riseCheck()
    {
        return Node.STATUS.SUCCESS;
    }
    public Node.STATUS phylcateryCheck()
    {
        return Node.STATUS.SUCCESS;
    }


    public void LaunchFrostbolts()
    {
        FB.projectileAttack(transform.rotation.eulerAngles);
        FB.projectileAttack(transform.rotation.eulerAngles + new Vector3(0, 45, 0));
        FB.projectileAttack(transform.rotation.eulerAngles + new Vector3(0, -45, 0));
    }
}
