using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemyBehavior : EnemyBehaviorTree
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;
    [Tooltip("List holding transform values for all the waypoints")]
    [SerializeField] private List<Transform> waypointsTransforms = new List<Transform>();
    [Tooltip("Float for the maximum disatnce the enemy will begin to follow the player from.")]
    [SerializeField] private float aggroRange;
    [Tooltip("Float for the maximum disatnce the enemy will move from the spawn.")]
    [SerializeField] private float spawnRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] private float attackRange;
    [Tooltip("Float for the time between the enemy's attack.")]
    [SerializeField] private float attackCD;
    [Tooltip("Float for the maximum angle the enemy can attack at.")]
    [SerializeField] private float attackAngle;
    [Tooltip("Enum representing the SoundEffect the enemy makes when idle.")]
    [SerializeField] private SoundEffectSpawner.SoundEffect idleSoundEffect;
    [Tooltip("Enum representing the SoundEffect the enemy makes when attacking (also include an animation event and script on actual model).")]
    [SerializeField] private SoundEffectSpawner.SoundEffect attackSoundEffect;
    private Transform player;
    //The spawn location of the enemy is automatically set based on scene placement.
    private Vector3 startPosition;
    private Vector3 currentWaypoint;
    private List<Vector3> waypoints;
    private Animator animator;
    private MusicManager musicManager;
    private AudioSource idleSound;
    private SoundEffectSpawner soundEffectSpawner;
    private NavMeshAgent agent;
    private EnemyHitpoints enemyHitpoints;
    private EnemyBasicAttackbox enemyBasicAttackbox;
    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckPlayer, CheckHurt, CheckAttack, MoveReset, MoveTowardsPlayer, PlayerSpawnRange, PlayerAggroRange, EnemyHurt, PlayerAttackRange, Attack;
    //Ensure the enemy doesn't start a new attack in the middle of an old one, and that we don't queue up a ton of music.
    private bool isAttackCD = false;

    private void Start()
    {
        //Waypoints and positioning
        waypoints = new List<Vector3>();
        startPosition = transform.position;
        waypoints.Add(startPosition);
        for (int i = 0; i < waypointsTransforms.Count; i++)
            waypoints.Add(waypointsTransforms[i].position);
        currentWaypoint = waypoints[0];

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        //GetComponentInChildren assumes we set up future enemies based on this script similar to those we've already done
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = FindObjectOfType<SoundEffectSpawner>();

        //Setup leaf nodes
        EnemyHurt = new Leaf("Enemy Hurt?", checkEnemyHurt);
        PlayerSpawnRange = new Leaf("Player in Spawn Range?", checkSpawnRange);
        PlayerAggroRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        PlayerAttackRange = new Leaf("Player in Attack Range?", checkPlayerAttackRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        MoveReset = new Leaf("Reset Move", moveReset);
        Attack = new Leaf("Attack", attack);

        //Setup sequence nodes and root
        CheckPlayer = new Sequence("Player Location Sequence", PlayerSpawnRange, PlayerAggroRange, MoveTowardsPlayer);
        CheckHurt = new Sequence("Check Hurt Sequence", EnemyHurt, MoveTowardsPlayer);
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, Attack);
        genericBehaviorTree = new BehaviorTree(MoveReset, CheckPlayer, CheckHurt, CheckAttack);
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        genericBehaviorTree.behavior();
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS moveTowards()
    {
        //Music and sound effects
        if (!aggrod)
            musicManager.combatCount++;
        aggrod = true;
        if (idleSound != null)
            idleSound.enabled = false;

        //Movement calculations
        Vector3 midpoint = (player.position - transform.position);
        if (midpoint.magnitude < (attackRange / 3))
            midpoint *= (0.05f / midpoint.magnitude);
        Vector3 target = transform.position + midpoint;
        agent.destination = (animator.GetCurrentAnimatorStateInfo(0).loop) ? target : transform.position;

        //Animation
        animator.SetBool("isMoving", (agent.velocity == Vector3.zero) ? false : true);

        MoveTowardsPlayer.status = Node.STATUS.SUCCESS;
        return MoveTowardsPlayer.status;
    }

    public Node.STATUS moveReset()
    {
        //Music and sound effects
        if (aggrod)
            musicManager.combatCount--;
        aggrod = false;
        if (idleSound == null)
            idleSound = soundEffectSpawner.MakeFollowingSoundEffect(transform, idleSoundEffect);

        //Movement
        if (Vector3.Distance(transform.position, currentWaypoint) < .2)
            currentWaypoint = (waypoints.IndexOf(currentWaypoint) + 1 >= waypoints.Count) ? waypoints[0] : waypoints[waypoints.IndexOf(currentWaypoint) + 1];
        agent.destination = (animator.GetCurrentAnimatorStateInfo(0).loop) ? currentWaypoint : transform.position;

        //Animation
        animator.SetBool("isMoving", (agent.velocity == Vector3.zero) ? false : true);

        MoveReset.status = Node.STATUS.SUCCESS;
        return MoveReset.status;
    }

    public Node.STATUS attack()
    {
        //If we aren't already attack and the cd is done, then attack.
        if (!isAttackCD && Attack.status != Node.STATUS.RUNNING)
        {
            animator.SetTrigger("Attack");
            Attack.status = Node.STATUS.RUNNING;
        }
        //When the attack animation has finished this will play.
        //This is a nifty little hack, the idle and moving animations will loop but attacks don't (for most things). So it won't work on some enemies (try tags instead?).
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            isAttackCD = true;
            Invoke("attackCDEnd", attackCD);
            Attack.status = Node.STATUS.SUCCESS;
            //Don't forget to include hitOn and hitOff animator events. Otherwise put them here (and declare attackbox).
        }
        return Attack.status;
    }

    //This is the part where enemies chase forever after being hurt. Another possible solution is invincibility and full heal when they deaggro (most games do this)
    //Implementing that would be easy, simply remove CheckHurt sequence and all children, then add in a flag in enemyHitpoints that's enabled when moveReset happens
    public Node.STATUS checkEnemyHurt()
    {
        return EnemyHurt.status = (enemyHitpoints.damaged ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE);
    }

    public Node.STATUS checkAggroRange()
    {
        //The distance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        return PlayerAggroRange.status = (playerDistance < aggroRange) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    public Node.STATUS checkSpawnRange()
    {
        //The distance from the enemy to the enemy's start location (in the waypoint model, this is the enemy's last waypoint)
        float spawnDistance = Vector3.Distance(player.transform.position, currentWaypoint);
        return PlayerSpawnRange.status = (spawnDistance < spawnRange) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    public Node.STATUS checkPlayerAttackRange()
    {
        //The dstance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (Vector3.Angle(transform.forward, player.position - transform.position) > attackAngle)
            return PlayerAggroRange.status = Node.STATUS.FAILURE;
        return PlayerAggroRange.status = (playerDistance < attackRange) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    public void playAttackSound()
    {
        soundEffectSpawner.MakeSoundEffect(transform.position, attackSoundEffect);
    }

    private void attackCDEnd()
    {
        isAttackCD = false;
    }
}
