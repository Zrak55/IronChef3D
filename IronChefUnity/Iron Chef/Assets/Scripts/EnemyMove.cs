using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [Tooltip("Script for the enemy's health.")]
    [SerializeField] private EnemyHitpoints enemyHitpoints;
    [Tooltip("Gameobject that the enemy will move towards.")]
    [SerializeField] private Transform moveTowards;
    [Tooltip("Distance the enemy begins to target moveTowards.")]
    [SerializeField] private int aggroDistance;
    [Tooltip("Farthest distance the enemy will move before returning to start, ignored if the enemy is hit.")]
    [SerializeField] private int moveRange;
    [Tooltip("Distance the enemy will re-aggro at from the start point after beginning to return to start after de-aggro.")]
    [SerializeField] private int returnRange;
    [Tooltip("Speed the enemy moves at when going towards moveTowards.")]
    [SerializeField] private float speed = 5f;
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private NavMeshAgent agent;
    private bool isReturning = false;

    private void OnEnable()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        startPosition = gameObject.transform.position;
    }

    void Update()
    {
        //Distance from the enemy's start location to its current location
        float spawnDistance = Vector3.Distance(startPosition, gameObject.transform.position);
        //Distance from moveTowards to the enemy's current location
        float playerDistance = Vector3.Distance(moveTowards.position, gameObject.transform.position);

        //Reverts the enemy to the original state after returning to spawn
        if (spawnDistance < returnRange)
            isReturning = false;

        //Moves towards moveTowards if it's in range, the enemy isn't returning to spawn, and moveTowards is in the total range
        //When you damage the enemy, it will stay aggro'd forever
        if ((playerDistance < aggroDistance && spawnDistance < moveRange && !isReturning) || enemyHitpoints.damaged == true)
        {
            agent.destination = moveTowards.position;
            agent.speed = speed;
        }
        else
        {
            //Sets the enemy's mode towards return to spawn, can't re-aggro during this time
            if (spawnDistance > moveRange)
                isReturning = true;
            agent.destination = startPosition;
            agent.speed = speed / 2;
        }
    }
}
