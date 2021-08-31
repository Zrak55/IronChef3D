using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Tooltip("Gameobject that the enemy will move towards.")]
    [SerializeField] private Transform moveTowards;
    [Tooltip("Distance the enemy begins to target moveTowards.")]
    [SerializeField] private int aggroDistance;
    [Tooltip("Farthest distance the enemy will move before returning to start, ignored if the enemy is hit.")]
    [SerializeField] private int moveRange;
    [Tooltip("Distance the enemy will re-aggro at from the start point after beginning to return to start after de-aggro.")]
    [SerializeField] private int returnRange;
    [Tooltip("Speed the enemy moves at when going towards moveTowards.")]
    [SerializeField] private float startSpeed = 5f;

    [HideInInspector]
    [Tooltip("The target speed, modified by the EnemySpeedController when effects are applied to the enemy")]
    private float currentSpeed;


    private Vector3 startPosition;
    private Vector3 currentPosition;
    private NavMeshAgent agent;
    private EnemyHitpoints enemyHitpoints;
    [HideInInspector] public bool isAggro = false;
    [HideInInspector] public float playerDistance;
    private bool isReturning = false;
    private EnemyAttack enemyAttack;
    private Animator anim;

    private void OnEnable()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemyAttack = gameObject.GetComponent<EnemyAttack>();
        enemyHitpoints = gameObject.GetComponent<EnemyHitpoints>();
        startPosition = gameObject.transform.position;
        anim = gameObject.GetComponent<Animator>();
        currentSpeed = startSpeed;
    }

    void Update()
    {
        //Distance from the enemy's start location to its current location
        float spawnDistance = Vector3.Distance(startPosition, gameObject.transform.position);
        //Distance from moveTowards to the enemy's current location
        playerDistance = Vector3.Distance(moveTowards.position, gameObject.transform.position);

        //Reverts the enemy to the original state after returning to spawn
        if (spawnDistance < returnRange)
            isReturning = false;

        //Moves towards moveTowards if it's in range, the enemy isn't returning to spawn, and moveTowards is in the total range
        //When you damage the enemy, it will stay aggro'd forever
        if ((playerDistance < aggroDistance && spawnDistance < moveRange && !isReturning) || enemyHitpoints.damaged == true)
        {
            agent.destination = moveTowards.position;
            agent.speed = currentSpeed;
            isAggro = true;
        }
        else
        {
            //Sets the enemy's mode towards return to spawn, can't re-aggro during this time
            if (spawnDistance > moveRange)
                isReturning = true;
            agent.destination = startPosition;
            agent.speed = currentSpeed / 2;
            if (!enemyHitpoints.damaged)
                isAggro = false;
        }

        //If the enemy isn't at its destination, then it is moving
        if (agent.destination != gameObject.transform.position)
            anim.SetBool("Walking", true);
        else
            anim.SetBool("Walking", false);
    }

    public void Pause(bool isStopped)
    {
        agent.isStopped = isStopped;
    }


    public float GetStartSpeed()
    {
        return startSpeed;
    }
    public void SetCurrentSpeed(float s)
    {
        currentSpeed = s;
    }
}
