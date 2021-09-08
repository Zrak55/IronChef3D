using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyBehavior : MonoBehaviour
{
    //This is a test class and not meant for actual use
    BehaviorTree genericBehaviorTree;

    public void Awake()
    {
        genericBehaviorTree = new BehaviorTree(new Node("Walk towards player",
                                                    new Node("Attack player")), 
                                               new Node("Death"),
                                               new Node("Top",
                                                    new Node("Middle 1",
                                                        new Node("Bottom")),
                                                    new Node("Middle 2")));
        genericBehaviorTree.printTree();
    }
}
