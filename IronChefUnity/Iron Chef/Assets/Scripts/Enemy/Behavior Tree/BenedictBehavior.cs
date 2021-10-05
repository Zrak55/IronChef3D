using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BenedictBehavior : MonoBehaviour
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;
    [Tooltip("Float for the maximum disatnce the enemy will begin to follow the player from.")]
    [SerializeField] private float aggroRange;
    [Tooltip("Float for the maximum disatnce the enemy will move from the spawn.")]
    [SerializeField] private float spawnRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float biteRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float rollRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float jumpRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float yolkRange;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float biteTime;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float rolTime;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float yolkTime;
    [Tooltip("Float for the time it takes the enemy to attack. (Will be replaced later with animation).")]
    [SerializeField] private float jumpTime;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float BiteCD;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float RollCD;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float YolkCD;
    [Tooltip("Float for the time between the enemy's attack")]
    [SerializeField] private float JumpCD;
    private Transform player;
    private Animator animator;
    private NavMeshAgent agent;
    private EnemyHitpoints enemyHitpoints;
    private EnemyBasicAttackbox enemyBasicAttackbox;
    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, CheckAttack, CheckYolk, ResetMove, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt, PlayerAttackRange, BiteAttack, JumpAttack, RollAttack, YolkAttack;
    //The spawn location of the enemy is automatically set based on scene placement.
    private Vector3 startPosition;

    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false;

    private bool BiteOnCD = false, RollOnCD = false, JumpOnCD = false, YolkOnCD = false;
    private bool InBiteRange = false, InRollRange = false, InJumpRange = false, InYolkRange = false;
    [HideInInspector]
    public bool DoneRolling = false;
    private BenedictRoll rollBehavior;
    private BenedictJump jumpBehavior;

    bool aggrod;
    public GameObject yolkBomb;
    public Transform BombSpawnPoint;

    private int currentPhase;

    bool phaseDelay = false;

    [Space]
    public GameObject bossWall;
    public GameObject postBossPortal;

    [Header("Meshes")]
    public MeshRenderer BenedictMeshMaterial;
    public MeshFilter BenedictMesh;
    public Mesh BenedictP1Mesh;
    public Material BenedictP1material;
    public Mesh BenedictP2Mesh;
    public Material BenedictP2Material;
    public Mesh BenedictP3Mesh;
    public Material BenedictP3Material;


    private void Start()
    {
        currentPhase = 1;

        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        enemyBasicAttackbox = GetComponent<EnemyBasicAttackbox>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        animator = GetComponent<Animator>();
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
            if (midpoint.magnitude < (biteRange / 3))
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
                RollAttack.status = Node.STATUS.RUNNING;
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
                    phaseDelay = false;
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

    private void attackEnd()
    {
        isAttacking = false;
        agent.enabled = true;
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

    public bool IsAggrod()
    {
        return aggrod;
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
        PlayerAggroRange.status = Node.STATUS.FAILURE;
        if (Vector3.Distance(player.transform.position, transform.position) < aggroRange)
        {
            PlayerAggroRange.status = Node.STATUS.SUCCESS;
            aggrod = true;

            if (bossWall.activeSelf == false)
                bossWall.SetActive(true);
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
        return PlayerAttackRange.status;
    }

   

    public void GoToNextPhase()
    {
        if(!phaseDelay && currentPhase < 3)
        {
            Debug.Log("Phased!!!!!");

            currentPhase++;
            phaseDelay = true;
            //TODO: ANIMATIONS FOR PHASING



            if(currentPhase == 2)
            {
                BenedictMeshMaterial.material = BenedictP2Material;
                BenedictMesh.mesh = BenedictP2Mesh;

                GetComponent<EnemyDamageTakenModifierController>().removeMod(DamageTakenModifier.ModifierName.BenedictImmunity);
            }
            else if(currentPhase == 3)
            {

                BenedictMeshMaterial.material = BenedictP3Material;
                BenedictMesh.mesh = BenedictP3Mesh;

                GetComponent<EnemyDamageTakenModifierController>().AddMod(DamageTakenModifier.ModifierName.BenedictDouble, 1, IronChefUtils.InfiniteDuration);

                SpeedEffector fast = new SpeedEffector();
                fast.effectName = SpeedEffector.EffectorName.BenedictSpeed;
                fast.percentAmount = 1;
                fast.duration = IronChefUtils.InfiniteDuration;
                GetComponent<EnemySpeedController>().Modifiers.Add(fast);
                
                rollBehavior.rollSpeed *= 1.25f;
                jumpTime *= 0.75f;
            }
            
        }
    }
}
