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
        setupEncounter();

        agent = GetComponent<NavMeshAgent>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        enemyCanvas = GetComponentInChildren<EnemyCanvas>(true);
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes
        CheckAngleRange = new Leaf("Player in Attack Range?", checkAngleRange);
        MoveReset = new Leaf("Reset Move", moveReset);
        MoveTowards = new Leaf("Waypoint Move", moveTowards);
        AttackProjectile = new Leaf("Attack", attackProjectile);

        //Setup sequence nodes and root
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackProjectile);
        CheckPlayer = new Sequence("Move Sequence", MoveReset, MoveTowards, CheckAttack);
        isobeanBehaviorTree = new BehaviorTree(CheckPlayer);
    }

    private void Update()
    {
        isobeanBehaviorTree.behavior();
    }

    public override Node.STATUS moveTowards()
    {
        //Check if nearby enemies are aggrod
        foreach (EnemyBehaviorTree enemyBehaviorTree in enemyBehaviorTrees)
        {
            if (enemyBehaviorTree.isAggrod())
                becomeAggro();
            if (enemyBehaviorTree.isSpawnRange())
                simpleFlag = false;
        }
        if (simpleFlag == true)
            becomeDeAggro();
        simpleFlag = true;

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

    public override Node.STATUS checkAngleRange()
    {
        if (aggrod)
            return base.checkAngleRange();
        return CheckAngleRange.status = Node.STATUS.FAILURE;
    }
}
