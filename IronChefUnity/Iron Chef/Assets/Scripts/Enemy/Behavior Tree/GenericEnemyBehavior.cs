using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemyBehavior : MonoBehaviour
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;
    [Tooltip("Transform for the object the enemy will follow, which is the player.")]
    [SerializeField] private Transform player;
    [Tooltip("Float for the maximum disatnce the enemy will begin to follow the player from.")]
    [SerializeField] private float aggroRange;
    //Nodes for the behavior tree. Will be adding more later.
    private Node MoveTowardsPlayer, PlayerSpawnRange;
    //The spawn location of the enemy is automatically set based on scene placement.
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;

        //Setup behavior tree
        PlayerSpawnRange = new Leaf("Player in Aggro Range?", checkAggroRange);
        MoveTowardsPlayer = new Leaf("Move towards player", moveTowards);
        genericBehaviorTree = new BehaviorTree(PlayerSpawnRange, MoveTowardsPlayer);
        genericBehaviorTree.printTree();
    }

    private void Update()
    {
        genericBehaviorTree.behavior();
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS moveTowards()
    {
        Debug.Log("Moving towards the player.");
        return Node.STATUS.SUCCESS;
    }

    public Node.STATUS checkAggroRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < aggroRange)
            return Node.STATUS.SUCCESS;
        return Node.STATUS.FAILURE;
    }
}
