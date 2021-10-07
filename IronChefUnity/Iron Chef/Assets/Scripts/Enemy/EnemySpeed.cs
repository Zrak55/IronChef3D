using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpeed : MonoBehaviour
{
    private float startSpeed;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(agent != null)
            startSpeed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetStartSpeed()
    {
        return startSpeed;
    }

    public void SetCurrentSpeed(float s)
    {
        if(agent != null)
            agent.speed = s;
    }
}
