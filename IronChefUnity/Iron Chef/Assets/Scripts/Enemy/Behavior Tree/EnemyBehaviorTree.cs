using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class EnemyBehaviorTree : MonoBehaviour
{
    [Tooltip("List holding transform values for all the waypoints")]
    [SerializeField] protected List<Transform> waypointsTransforms = new List<Transform>();
    [Tooltip("Float for the maximum distance the enemy will begin to follow the player from.")]
    [SerializeField] protected float aggroRange;
    [Tooltip("Float for the maximum disatnce the enemy will move from the spawn.")]
    [SerializeField] protected float spawnRange;
    [Tooltip("Float for the maximum distance the enemy will begin to attack from.")]
    [SerializeField] protected float attackRange;
    [Tooltip("Float for the time between the enemy's attack.")]
    [SerializeField] protected float attackCD;
    [Tooltip("Float for the maximum angle the enemy can attack at.")]
    [SerializeField] protected float attackAngle;
    [Tooltip("Enum representing the SoundEffect the enemy makes when idle.")]
    [SerializeField] protected List<SoundEffectSpawner.SoundEffect> idleSoundEffect = new List<SoundEffectSpawner.SoundEffect>();
    [Tooltip("Enum representing the SoundEffect the enemy makes when attacking (also include an animation event and script on actual model).")]
    [SerializeField] protected List<SoundEffectSpawner.SoundEffect> attackSoundEffect = new List<SoundEffectSpawner.SoundEffect>();
    protected Transform player;
    protected GameObject boss;
    //The spawn location of the enemy is automatically set based on scene placement.
    protected Vector3 startPosition;
    protected Vector3 currentWaypoint;
    protected List<Vector3> waypointsVectors;
    protected Animator animator;
    protected MusicManager musicManager;
    protected AudioSource idleSound;
    protected SoundEffectSpawner soundEffectSpawner;
    protected NavMeshAgent agent;
    protected EnemyHitpoints enemyHitpoints;
    protected EnemyProjectile enemyProjectile;
    protected EnemyStunHandler enemyStunHandler;
    protected EnemyBasicAttackbox enemyBasicAttackbox;
    protected Node MoveTowards, MoveReset, AttackBasic, AttackSecondary, AttackProjectile, JumpBack, CheckEnemyHurt, CheckAggroRange, CheckSpawnRange, CheckDoubleRange, CheckAngleRange, CheckBehind, RunOnce;
    //Ensure the enemy doesn't start a new attack in the middle of an old one, and that we don't queue up a ton of music.
    protected bool aggrod, isAttackCD = false;
    [HideInInspector] public bool invincible = false, simpleFlag = true;

    protected void randomizeWayponts(int range)
    {
        //Will run setupWaypoints and then add 2 new randomized waypoints (This is a test function, will not work because wayponts can spawn in unobtainable walls)
        //If there already are assigned waypoints, don't do this
        setupWaypoints();
        if (waypointsVectors.Count <= 1)
        {
            waypointsVectors.Add(new Vector3(Random.Range(startPosition.x - range, startPosition.x + range), startPosition.y, Random.Range(startPosition.z - range, startPosition.z + range)));
            waypointsVectors.Add(new Vector3(Random.Range(startPosition.x - range, startPosition.x + range), startPosition.y, Random.Range(startPosition.z - range, startPosition.z + range)));
        }
    }

    protected void setupWaypoints()
    {
        //Waypoints and positioning
        waypointsVectors = new List<Vector3>();
        startPosition = transform.position;
        waypointsVectors.Add(startPosition);
        for (int i = 0; i < waypointsTransforms.Count; i++)
            waypointsVectors.Add(waypointsTransforms[i].position);
        currentWaypoint = waypointsVectors[0];
    }

    public virtual Node.STATUS moveTowards()
    {
        //Music and sound effects
        if (!aggrod)
            PlayerHitpoints.CombatCount++;
        aggrod = true;

        //Movement calculations
        Vector3 midpoint = player.transform.position - transform.position;
        if (midpoint.magnitude < attackRange && Vector3.Angle(transform.forward, player.position - transform.position) < attackAngle)
            midpoint = Vector3.zero;
        if (agent.enabled == true)
            agent.destination = (animator.GetCurrentAnimatorStateInfo(0).loop) ? (transform.position + midpoint) : transform.position;

        //Animation
        animator.SetBool("isMoving", (agent.velocity.magnitude == 0) ? false : true);

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }

    public virtual Node.STATUS moveReset()
    {
        //Before anything else, check if stunned
        if (enemyStunHandler.IsStunned())
        {
            agent.destination = transform.position;
            //Maybe there will be another animation state for stunning later
            animator.SetBool("isMoving", false);
            animator.Play("Base Layer.Idle", 0, 0);
            return MoveReset.status = Node.STATUS.RUNNING;
        }

        //Music and sound effects
        if (aggrod)
            PlayerHitpoints.CombatCount--;
        aggrod = false;
        if (agent.velocity.magnitude == 0 && animator.GetCurrentAnimatorStateInfo(0).loop && idleSound == null && Vector3.Distance(transform.position, player.position) <= 200)
            idleSound = soundEffectSpawner.MakeFollowingSoundEffect(transform, idleSoundEffect[0]);

        //Movement
        Vector3 distance = transform.position - currentWaypoint;
        distance.y = 0;
        if (distance.magnitude < 1)
            currentWaypoint = (waypointsVectors.IndexOf(currentWaypoint) + 1 >= waypointsVectors.Count) ? waypointsVectors[0] : waypointsVectors[waypointsVectors.IndexOf(currentWaypoint) + 1];
        if (agent.enabled == true)
            agent.destination = (animator.GetCurrentAnimatorStateInfo(0).loop) ? currentWaypoint : transform.position;

        //Animation
        animator.SetBool("isMoving", (agent.velocity == Vector3.zero) ? false : true);

        return MoveReset.status = Node.STATUS.SUCCESS;
    }

    public virtual Node.STATUS attackBasic()
    {
        //If we aren't already attack and the cd is done, then attack.
        if (!isAttackCD && AttackBasic.status != Node.STATUS.RUNNING)
        {
            animator.SetTrigger("Attack");
            AttackBasic.status = Node.STATUS.RUNNING;
        }
        //When the attack animation has finished this will play.
        //This is a nifty little hack, the idle and moving animations will loop but attacks don't (for most things). So it won't work on some enemies (try tags instead?).
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!isAttackCD)
                StartCoroutine("atttackCDEnd");
            AttackBasic.status = Node.STATUS.SUCCESS;
            //Don't forget to include hitOn and hitOff animator events. Otherwise put them here (and declare attackbox).
        }
        return AttackBasic.status;
    }

    //In case you need two seperate attack nodes that do different things.
    public virtual Node.STATUS attackSecondary()
    {
        if (!isAttackCD && AttackSecondary.status != Node.STATUS.RUNNING)
        {
            animator.SetTrigger("Attack");
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

    public virtual Node.STATUS attackProjectile()
    {
        if (!isAttackCD && AttackProjectile.status != Node.STATUS.RUNNING)
        {
            animator.SetTrigger("Projectile");
            AttackProjectile.status = Node.STATUS.RUNNING;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).loop)
        {
            if (!isAttackCD)
                StartCoroutine("atttackCDEnd");
            AttackProjectile.status = Node.STATUS.SUCCESS;
        }
        return AttackProjectile.status;
    }

    public virtual Node.STATUS jumpBack()
    {
        if (!isAttackCD && JumpBack.status != Node.STATUS.RUNNING && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Jump"))
        {
            animator.SetTrigger("Back");
            GetComponent<EnemyJump>().BeginJumping(.3f, (transform.forward * -attackRange) + transform.position);
        }
        else if (JumpBack.status == Node.STATUS.RUNNING)
            return JumpBack.status = Node.STATUS.SUCCESS;
        return JumpBack.status = Node.STATUS.RUNNING;
    }

    //This is the part where enemies chase forever after being hurt. Another possible solution is invincibility and full heal when they deaggro (most games do this)
    //Implementing that would be easy, simply remove CheckHurt sequence and all children, then add in a flag in enemyHitpoints that's enabled on MoveReset success and everything else false (Try running)
    public Node.STATUS checkEnemyHurt()
    {
        return CheckEnemyHurt.status = (enemyHitpoints.damaged ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE);
    }

    public Node.STATUS checkAggroRange()
    {
        //The distance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        return CheckAggroRange.status = (playerDistance < aggroRange) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    public Node.STATUS checkSpawnRange()
    {
        //The distance from the enemy to the enemy's start location (in the waypoint model, this is the enemy's last waypoint)
        float spawnDistance = Vector3.Distance(player.transform.position, currentWaypoint);
        return CheckSpawnRange.status = (spawnDistance < spawnRange) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    public Node.STATUS checkDoubleRange()
    {
        //The distance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        return CheckDoubleRange.status = (playerDistance < attackRange * 2) && (playerDistance > attackRange * 1.5) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    public Node.STATUS checkAngleRange()
    {
        //The distance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (Vector3.Angle(transform.forward, new Vector3 (player.position.x - transform.position.x, 0, player.position.z - transform.position.z)) > attackAngle)
            return CheckAngleRange.status = Node.STATUS.FAILURE;
        return CheckAngleRange.status = (playerDistance < attackRange) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    public Node.STATUS checkBehind()
    {
        //The distance from the enemy to the player
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (Vector3.Angle(transform.forward, new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z)) < attackAngle)
            return CheckAngleRange.status = Node.STATUS.FAILURE;
        return CheckAngleRange.status = (playerDistance < attackRange) ? Node.STATUS.SUCCESS : Node.STATUS.FAILURE;
    }

    //Meant to go with a Selector node
    public Node.STATUS runOnce()
    {
        if (simpleFlag)
        {
            simpleFlag = false;
            return RunOnce.status = Node.STATUS.FAILURE;
        }
        return RunOnce.status = Node.STATUS.SUCCESS;
    }

    protected IEnumerator atttackCDEnd()
    {
        isAttackCD = true;
        yield return new WaitForSeconds(attackCD);
        isAttackCD = false;
    }

    public void playSound(int value)
    {
        soundEffectSpawner.MakeSoundEffect(transform.position, attackSoundEffect[value]);
    }

    public virtual void counter()
    {
        invincible = false;
        animator.SetTrigger("Counter");
    }

    public bool isAggrod()
    {
        return aggrod;
    }
}