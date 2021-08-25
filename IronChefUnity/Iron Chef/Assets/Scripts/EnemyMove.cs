using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform moveTowards;
    [SerializeField] private int aggroDistance;
    [SerializeField] private int moveRange;
    [SerializeField] private float speed = 5f;
    private Vector3 startPosition;
    private NavMeshAgent agent;
    private bool isReturning;

    private void OnEnable()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(moveTowards.position, gameObject.transform.position) < aggroDistance &&
            Vector3.Distance(startPosition, gameObject.transform.position) < moveRange)
        {
            agent.destination = moveTowards.position;
            agent.speed = speed;
        }
        else
        {
            agent.destination = startPosition;
            agent.speed = speed / 2;
        }
    }
}
