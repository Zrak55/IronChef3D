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

    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float CreamyCometRange;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float CreamyCometCD;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float CreamyCometTime;

    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float BlinkRange;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float BlinkCD;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float BlinkTime;

    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float IceWallRange;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float IceWallCD;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float IceWallTime;

    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, SpecialSequence, CheckAttack, CheckRise, CheckPhylactery, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, 
        PlayerAggroRange, EnemyHurt, PlayerAttackRange, FrostboltAttack, CreamyCometAttack, Blink, IceWallAttack;
    //The spawn location of the enemy is automatically set based on scene placement.

    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false;

    private bool FrostboltOnCD = false;
    bool genericCD = false;
    private bool InFrostboltRange = false;

    private bool BlinkOnCD = false;

    private bool CreamyCometOnCD = false;
    private bool InCreamyCometRange = false;

    private bool IceWallOnCD = false;
    private bool InIceWallRange = false;


    [Space]
    public GameObject bossWallEnter;


    private MusicManager music;


    public EnemyProjectile FB;

    public Transform stageCenter;
    public float stageRadius;


    bool isRising = false;
    bool doneRising = false;
    bool isPhylactery = false;

    [Space]

    public List<Transform> RiseMinionsLocations;
    public GameObject ZombiePrefab;
    int ZombieCount = 0;

    [Space]

    public float PhylacteryTimer;
    public NeccreammancerPhylactery MyPhylactery;

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
        CreamyCometAttack = new Leaf("Creamy Comet", creamyComets);
        Blink = new Leaf("Blink", blink);
        IceWallAttack = new Leaf("Ice Wall", iceWall);

        CheckPhylactery = new Leaf("Phylactery", phylcateryCheck);
        CheckRise = new Leaf("Rise", riseCheck);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, Blink, FrostboltAttack, CreamyCometAttack, IceWallAttack);
        SpecialSequence = new Sequence("Special Abilities", CheckPhylactery, CheckRise);
        genericBehaviorTree = new BehaviorTree(ResetMove, CheckPlayer, CheckHurt, SpecialSequence, CheckAttack);



        music = FindObjectOfType<MusicManager>();


        GetComponent<EnemyHitpoints>().DeathEvents += BossOver;
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        genericBehaviorTree.behavior();

        if(agent.enabled && isAggrod())
        {
            transform.LookAt(player.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS moveTowards()
    {
        animator.SetBool("isMoving", true);
        if(agent.enabled)
        {
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

    public Node.STATUS blink()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (Blink.status == Node.STATUS.RUNNING && !isAttacking)
            {
                Blink.status = Node.STATUS.SUCCESS;
                return Blink.status;
            }
            else if (Blink.status == Node.STATUS.RUNNING && isAttacking)
            {
                Blink.status = Node.STATUS.RUNNING;
                return Blink.status;
            }
            else if (BlinkOnCD)
            {
                Blink.status = Node.STATUS.SUCCESS;
                return Blink.status;
            }
            else
            {
                Invoke("BlinkCDEnd", BlinkCD + BlinkTime);
                BlinkOnCD = true;
                isAttacking = true;
                animator.SetTrigger("Blink");
                Invoke("attackEnd", BlinkTime);




                Blink.status = Node.STATUS.RUNNING;
            }



        }
        return Blink.status;
    }

    public void DoBlink()
    {
        Vector3 target = transform.position;
        while((target - transform.position).magnitude < 20)
            target = stageCenter.position + (Random.Range(0, stageRadius) * new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized);
        agent.Move(target - transform.position);
    }
    public Node.STATUS creamyComets()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (CreamyCometAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                CreamyCometAttack.status = Node.STATUS.SUCCESS;
                return CreamyCometAttack.status;
            }
            else if (CreamyCometAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                CreamyCometAttack.status = Node.STATUS.RUNNING;
                return CreamyCometAttack.status;
            }
            else if (CreamyCometOnCD || !InCreamyCometRange)
            {
                CreamyCometAttack.status = Node.STATUS.SUCCESS;
                return CreamyCometAttack.status;
            }
            else
            {
                Invoke("CreamyCometCDEnd", CreamyCometCD + CreamyCometTime);
                CreamyCometOnCD = true;
                isAttacking = true;
                animator.SetTrigger("CreamyComet");
                Invoke("attackEnd", CreamyCometTime);



                CreamyCometAttack.status = Node.STATUS.RUNNING;
            }



        }

        return CreamyCometAttack.status;
    }


    public Node.STATUS iceWall()
    {
        if (aggrod && !isAttacking)
        {
            //When the attack animation has finished this will play.
            if (IceWallAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                IceWallAttack.status = Node.STATUS.SUCCESS;
                return IceWallAttack.status;
            }
            else if (IceWallAttack.status == Node.STATUS.RUNNING && isAttacking)
            {
                IceWallAttack.status = Node.STATUS.RUNNING;
                return IceWallAttack.status;
            }
            else if (IceWallOnCD || !InIceWallRange)
            {
                IceWallAttack.status = Node.STATUS.SUCCESS;
                return IceWallAttack.status;
            }
            else
            {
                Invoke("IceWallCDEnd", IceWallCD + IceWallTime);
                IceWallOnCD = true;
                isAttacking = true;
                animator.SetTrigger("IceWall");
                Invoke("attackEnd", IceWallTime);



                IceWallAttack.status = Node.STATUS.RUNNING;
            }



        }

        return IceWallAttack.status;
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

    private void CreamyCometCDEnd()
    {
        CreamyCometOnCD = false;
    }

    private void IceWallCDEnd()
    {
        IceWallOnCD = false;
    }

    private void BlinkCDEnd()
    {
        BlinkOnCD = false;
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

                if (bossWallEnter.activeSelf == true)
                    bossWallEnter.SetActive(false);

                FindObjectOfType<PlayerHUDManager>().BossInfoOn("The Ice Nec-Cream-Mancer", GetComponent<EnemyHitpoints>(), "");

                BlinkOnCD = true;
                Invoke("BlinkCDEnd", BlinkCD/2);

                IceWallOnCD = true;
                Invoke("IceWallCDEnd", IceWallCD / 2);


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
        if (Vector3.Distance(player.transform.position, transform.position) < CreamyCometRange)
        {
            InCreamyCometRange = true;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < IceWallRange)
        {
            InIceWallRange = true;
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
        bossWallEnter.SetActive(true);
        FindObjectOfType<PlayerHUDManager>().BossOver();
    }


    public Node.STATUS riseCheck()
    {
        if(isAttacking)
        {
            CheckRise.status = Node.STATUS.SUCCESS;
        }
        else if(enemyHitpoints.GetPercentHP() <= 0.5f)
        {
            if(doneRising)
            {
                CheckRise.status = Node.STATUS.SUCCESS;
            }
            else if(isRising)
            {
                CheckRise.status = Node.STATUS.RUNNING;
            }
            else
            {
                isRising = true;
                agent.enabled = false;
                animator.SetTrigger("Rise");
                CheckRise.status = Node.STATUS.RUNNING;
            }
        }
        else
        {
            CheckRise.status = Node.STATUS.SUCCESS;
        }


        return CheckRise.status;
    }

    public void SpawnMinions()
    {
        transform.position = new Vector3(10000, 10000, 10000);
        foreach(var l in RiseMinionsLocations)
        {
            var z = Instantiate(ZombiePrefab, l.position, Quaternion.identity);
            z.GetComponent<SpawnedGummyZombieBehavior>().ImBossZombie();
            ZombieCount++;
        }
    }

    public void ZombieIsKilled()
    {
        ZombieCount--;
        if(ZombieCount <= 0)
        {
            transform.position = stageCenter.position;
            agent.enabled = true;
            isRising = false;
            doneRising = true;
            BlinkOnCD = true;
            Invoke("BlinkCDEnd", BlinkCD / 2);
        }
    }
    
    public void SpawnPhylcatery()
    {
        Invoke("PhylacteryTimerOver", PhylacteryTimer);
        MyPhylactery.transform.position = stageCenter.position;
        MyPhylactery.ShowTimer(PhylacteryTimer);
        agent.enabled = false;
        transform.position = new Vector3(10000, 10000, 10000);
        isPhylactery = true;
        GetComponent<EnemyDamageTakenModifierController>().AddMod(DamageTakenModifier.ModifierName.NeccreammancerPhylacteryStage, -10000, PhylacteryTimer);
        Tip("Destroy the phylactery before his body reforms!");
    }
    public void PhylacteryTimerOver()
    {
        BlinkOnCD = true;
        Invoke("BlinkCDEnd", BlinkCD / 2);

        isPhylactery = false;
        enemyHitpoints.Heal(enemyHitpoints.GetMax());
        doneRising = false;
        MyPhylactery.transform.position = new Vector3(10000, 10000, 10000);
        transform.position = stageCenter.position;
        agent.enabled = true;

        Tip("You'll have another shot at the phylactery - defeat him again!");
    }


    public Node.STATUS phylcateryCheck()
    {
        if (isPhylactery)
            CheckPhylactery.status = Node.STATUS.RUNNING;
        else
            CheckPhylactery.status = Node.STATUS.SUCCESS;

        return CheckPhylactery.status;
    }


    public void LaunchFrostbolts()
    {
        FB.projectileAttack(transform.rotation.eulerAngles);
        FB.projectileAttack(transform.rotation.eulerAngles + new Vector3(0, 20, 0));
        FB.projectileAttack(transform.rotation.eulerAngles + new Vector3(0, -20, 0));
    }
}
