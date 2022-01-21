using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsobeanBehavior : EnemyBehaviorTree
{
    BehaviorTree isobeanBehaviorTree;
    private Node CheckPlayer, CheckHurt, CheckAttack;
    //Isobean is mostly done. Next step is adding attack animation to animator, and a projectileAttack to that. That should also fix the music.
    private void Start()
    {
        setupWaypoints();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        CheckAngleRange = new Leaf("Player in Attack Range?", checkAngleRange);
        MoveTowards = new Leaf("Reset Move", moveTowards);
        AttackProjectile = new Leaf("Attack", attackProjectile);

        //Setup sequence nodes and root
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackProjectile);
        CheckPlayer = new Sequence("Move Sequence", MoveTowards, CheckAttack);
        isobeanBehaviorTree = new BehaviorTree(CheckPlayer);
    }

    private void Update()
    {
        isobeanBehaviorTree.behavior();
    }

    public override Node.STATUS moveTowards()
    {
        //Before anything else, check if stunned
        if (enemyStunHandler.IsStunned())
        {
            agent.destination = transform.position;
            //Maybe there will be another animation state for stunning later
            animator.SetBool("isMoving", false);
            animator.Play("Base Layer.Idle", 0, 0);
            return MoveTowards.status = Node.STATUS.RUNNING;
        }

        //Music and sound effects
        if (aggrod)
            musicManager.combatCount--;
        aggrod = false;

        //Movement
        Vector3 distance = transform.position - currentWaypoint;
        distance.y = 0;
        if (distance.magnitude < 1)
            currentWaypoint = (waypointsVectors.IndexOf(currentWaypoint) + 1 >= waypointsVectors.Count) ? waypointsVectors[0] : waypointsVectors[waypointsVectors.IndexOf(currentWaypoint) + 1];
        agent.destination = currentWaypoint;

        //Animation
        animator.SetBool("isMoving", (agent.velocity == Vector3.zero) ? false : true);

        return MoveTowards.status = Node.STATUS.SUCCESS;
    }

    public override Node.STATUS attackProjectile()
    {
        //Music
        if (!aggrod)
            musicManager.combatCount++;
        aggrod = true;
        return base.attackProjectile();
    }
}
