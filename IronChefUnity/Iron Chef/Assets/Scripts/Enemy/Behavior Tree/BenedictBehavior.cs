using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BenedictBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float biteRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float rollRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float jumpRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float yolkRange;
    [Tooltip("Float for the time it takes the enemy to attack.")]
    [SerializeField] private float biteTime;
    [Tooltip("Float for the time it takes the enemy to attack.")]
    [SerializeField] private float rolTime;
    [Tooltip("Float for the time it takes the enemy to attack.")]
    [SerializeField] private float yolkTime;
    [Tooltip("Float for the time it takes the enemy to attack.")]
    [SerializeField] private float jumpTime;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float BiteCD;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float RollCD;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float YolkCD;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float JumpCD;
    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, CheckAttack, CheckYolk, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt, PlayerAttackRange, BiteAttack, JumpAttack, RollAttack, YolkAttack;
    //The spawn location of the enemy is automatically set based on scene placement.

    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false;

    private bool BiteOnCD = false, RollOnCD = false, JumpOnCD = false, YolkOnCD = false;
    bool genericCD = false;
    private bool InBiteRange = false, InRollRange = false, InJumpRange = false, InYolkRange = false;
    [HideInInspector]
    public bool DoneRolling = false;
    private BenedictRoll rollBehavior;
    private BenedictJump jumpBehavior;

    public GameObject yolkBomb;
    public Transform BombSpawnPoint;

    private int currentPhase;

    bool phaseDelay = false;

    [Space]
    public GameObject bossWall;
    public GameObject postBossPortal;

    [Header("Meshes")]
    //public MeshRenderer BenedictMeshMaterial;
    public SkinnedMeshRenderer BenedictMesh;
    public Mesh BenedictP1Mesh;
    public Material BenedictP1material;
    public Mesh BenedictP2Mesh;
    public Material BenedictP2Material;
    public Mesh BenedictP3Mesh;
    public Material BenedictP3Material;

    private MusicManager music;

    private AudioSource laugh;

    private void Start()
    {
        currentPhase = 1;

        BenedictMesh.material = BenedictP1material;

        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        enemyBasicAttackbox = GetComponent<EnemyBasicAttackbox>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        rollBehavior = GetComponent<BenedictRoll>();
        jumpBehavior = GetComponent<BenedictJump>();

        //Setup leaf nodes
        EnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        PlayerSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        PlayerAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        PlayerAttackRange = new Leaf("Player in Attack Range?", checkPlayerAttackRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        ResetMove = new Leaf("Reset Move", movePause);
        BiteAttack = new Leaf("BiteAttack", biteAttack);
        JumpAttack = new Leaf("JumpAttack", jumpAttack);
        RollAttack = new Leaf("RollAttack", rollAttack);
        YolkAttack = new Leaf("YolkAttack", yolkAttack);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, BiteAttack, RollAttack, JumpAttack);
        CheckYolk = new Sequence("Yolk Sequence", YolkAttack);
        genericBehaviorTree = new BehaviorTree(ResetMove, CheckPlayer, CheckHurt, CheckYolk, CheckAttack);


        GetComponent<EnemyDamageTakenModifierController>().AddMod(DamageTakenModifier.ModifierName.BenedictImmunity, -10000, IronChefUtils.InfiniteDuration);

        music = FindObjectOfType<MusicManager>();

        GetComponent<EnemyHitpoints>().DeathEvents += BossOver;

    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        genericBehaviorTree.behavior();
        StopEscaping();
    }

    public void LaughOverride()
    {
        Laugh(true);
    }

    public void Laugh(bool overrideLaugh = false)
    {
        if(laugh == null || overrideLaugh)
            laugh = SoundEffectSpawner.soundEffectSpawner.MakeFollowingSoundEffect(player, SoundEffectSpawner.SoundEffect.BenedictLaugh);

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


    public Node.STATUS yolkAttack()
    {
        if (aggrod)
        {
            YolkAttack.status = Node.STATUS.SUCCESS;
            if (!YolkOnCD && currentPhase >= 3)
            {
                Invoke("YolkCDEnd", YolkCD + yolkTime);
                YolkOnCD = true;
                float scale = Random.Range(1f, 2f);
                var projectile = Instantiate(yolkBomb, BombSpawnPoint.position, Random.rotation).GetComponent<ProjectileLaunch>();
                projectile.transform.localScale = new Vector3(scale, scale, scale);
                projectile.Launch(Random.Range(5, 40), new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized, Random.Range(10, 80));
                Destroy(projectile.gameObject, 5f);
            }
        }
        return YolkAttack.status;
    }

    public Node.STATUS jumpAttack()
    {
        if (aggrod && !isAttacking)
        {


            if (JumpAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                JumpAttack.status = Node.STATUS.SUCCESS;
                return JumpAttack.status;
            }
            else if (JumpOnCD || !InJumpRange)
            {
                JumpAttack.status = Node.STATUS.SUCCESS;
                return JumpAttack.status;
            }
            else
            {
                Invoke("JumpCDEnd", JumpCD + jumpTime);
                JumpOnCD = true;
                isAttacking = true;
                animator.SetBool("Jump", true);
                Invoke("attackEnd", jumpTime);
                jumpBehavior.BeginJumping(jumpTime);
                agent.enabled = false;
                JumpAttack.status = Node.STATUS.RUNNING;
            }
        }
        return JumpAttack.status;
        
    }

    public Node.STATUS rollAttack()
    {
        if(aggrod && !isAttacking)
        {
           
            if (RollAttack.status == Node.STATUS.RUNNING && !isAttacking)
            {
                RollAttack.status = Node.STATUS.SUCCESS;
                return RollAttack.status;
            }
            else if (RollOnCD || !InRollRange)
            {
                RollAttack.status = Node.STATUS.SUCCESS;
                return RollAttack.status;
            }
            else
            {
                Invoke("RollCDEnd", RollCD + rolTime);
                RollOnCD = true;
                isAttacking = true;
                animator.SetBool("Roll", true);
                Invoke("attackEnd", rolTime);
                rollBehavior.BeginRolling(rolTime);
                agent.enabled = false;


                RollAttack.status = Node.STATUS.RUNNING;
            }
            
            
        }
        return RollAttack.status;
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
            else if(BiteOnCD || currentPhase < 2 || !InBiteRange)
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

    public void UndoPhaseDelay()
    {
        phaseDelay = false;
    }
    private void attackEnd()
    {
        isAttacking = false;
        agent.enabled = true;
        genericCD = true;
        Invoke("UndoGenericCD", 1f);
    }

    void UndoGenericCD()
    {
        genericCD = false;
    }

    private void BiteCDEnd()
    {
        BiteOnCD = false;
    }
    private void JumpCDEnd()
    {
        JumpOnCD = false;
    }
    private void RollCDEnd()
    {
        RollOnCD = false;
    }
    private void YolkCDEnd()
    {
        YolkOnCD = false;
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

                FindObjectOfType<PlayerHitpoints>().StartBossCombat();

                PlayerHitpoints.CombatCount++;

                if (bossWall.activeSelf == false)
                    bossWall.SetActive(true);

                FindObjectOfType<PlayerHUDManager>().BossInfoOn("Benedict, the Arm-egg-eddon", GetComponent<EnemyHitpoints>(), "");
                Invoke("Phase1Tip", 15f);
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
            PlayerHitpoints.CombatCount--;
        aggrod = false;

        return PlayerSpawnRange.status;
    }


    public Node.STATUS checkPlayerAttackRange()
    {
        InBiteRange = false;
        InYolkRange = false;
        InJumpRange = false;
        InRollRange = false;
        PlayerAttackRange.status = Node.STATUS.FAILURE;
        if (Vector3.Distance(player.transform.position, transform.position) < biteRange)
        {
            InBiteRange = true;
            PlayerAttackRange.status = Node.STATUS.SUCCESS;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < rollRange)
        {
            InRollRange = true;
            PlayerAttackRange.status = Node.STATUS.SUCCESS;
        }
        if(Vector3.Distance(player.transform.position, transform.position) < jumpRange)
        {
            InJumpRange = true;
            PlayerAttackRange.status = Node.STATUS.SUCCESS;
        }
        if (Vector3.Distance(player.transform.position, transform.position) < yolkRange)
        {
            InYolkRange = true;
            PlayerAttackRange.status = Node.STATUS.SUCCESS;
        }

        if(genericCD)
        {
            PlayerAttackRange.status = Node.STATUS.FAILURE;
        }

        return PlayerAttackRange.status;
    }

   

    public void GoToNextPhase()
    {
        if(!phaseDelay && currentPhase < 3)
        {
            Debug.Log("Phased!!!!!");

            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.EggCrack);

            currentPhase++;

            phaseDelay = true;
            //TODO: ANIMATIONS FOR PHASING



            if(currentPhase == 2)
            {
                //BenedictMeshMaterial.material = BenedictP2Material;
                BenedictMesh.sharedMesh = BenedictP2Mesh;
                BenedictMesh.material = BenedictP2Material;
                
                GetComponent<EnemyDamageTakenModifierController>().removeMod(DamageTakenModifier.ModifierName.BenedictImmunity);
                GetComponentInChildren<EnemyVFXController>().StartEffect(0);
                GetComponentInChildren<EnemyVFXController>().StartEffect(0);
                GetComponent<EnemyVFXController>().OneTimeEffects[0].StartEffect();
                

                Phase2Tip();
            }
            else if(currentPhase == 3)
            {
                animator.SetBool("Yolk?", true);

                BenedictMesh.sharedMesh = BenedictP3Mesh;
                BenedictMesh.material = BenedictP3Material;

                GetComponent<EnemyDamageTakenModifierController>().AddMod(DamageTakenModifier.ModifierName.BenedictDouble, 1, IronChefUtils.InfiniteDuration);
                GetComponentInChildren<EnemyVFXController>().StartEffect(1);
                SpeedEffector fast = new SpeedEffector();
                fast.effectName = SpeedEffector.EffectorName.BenedictSpeed;
                fast.percentAmount = 0.5f;
                fast.duration = IronChefUtils.InfiniteDuration;
                GetComponent<EnemySpeedController>().Modifiers.Add(fast);
                GetComponent<EnemyVFXController>().OneTimeEffects[1].StartEffect();

                //rollBehavior.rollSpeed *= 1.25f;
                jumpTime *= 0.75f;

                Phase3Tip();
            }
            
        }
    }

    void Phase1Tip()
    {
        if(currentPhase == 1)
        {
            FindObjectOfType<PlayerHUDManager>().SetBossTip("Your attacks appear to have no effect, find a way to crack his shell...");
        }
    }
    void Phase2Tip()
    {
        FindObjectOfType<PlayerHUDManager>().SetBossTip("His shell is cracked, he is vulnerable!  But it looks like there's more to break...");
    }

    void Phase3Tip()
    {
        FindObjectOfType<PlayerHUDManager>().SetBossTip("He's totally exposed, but extra dangerous!");
    }

    public void BossOver()
    {
        bossWall.SetActive(false);
        postBossPortal.SetActive(true);
        FindObjectOfType<PlayerHUDManager>().BossOver();
        bool IDealtDamage = FindObjectOfType<PlayerHitpoints>().EndBossCombat();
        if(!IDealtDamage)
        {
            AchievementManager.UnlockAchievement("Over Easy");
        }
    }

    public void StopEscaping()
    {
        Vector3 myPos = transform.position;
        myPos.y = 0;
        Vector3 centerPos = rollBehavior.RoomCenter.position;
        centerPos.y = 0;



        if(Vector3.Distance(myPos, centerPos) > 65)
        {
            transform.position = new Vector3(centerPos.x, transform.position.y, centerPos.z) + (myPos - centerPos).normalized * 65;
        }
    }
}
