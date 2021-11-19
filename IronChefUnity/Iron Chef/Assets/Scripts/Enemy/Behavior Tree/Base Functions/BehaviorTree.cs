using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree
{
    //This class is just a container for the root node and some general use functions that apply to the whole tree.
    Summation root;

    #region Constructors
    public BehaviorTree()
    {
        root = new Summation("root");
    }

    public BehaviorTree(params Node[] childParam)
    {
        root = new Summation("root", childParam);
    }

    #endregion
    #region Functions
    //Include this in the update frame of the enemy behavior script. This is where the logic happens and it iterates through the tree.
    public void behavior()
    {
        root.proccess();
    }

    //Prints out the tree recursively. Note that in Unity you need to click on the debug message to see the whole thing.
    public void printTree()
    {
        Debug.Log(root.printName(1));
    }

    //Changes all the nodes back to start. Honestly not really needed/useful, maybe there will be a use someday.
    public void resetTree()
    {
        root.reset();
    }
    #endregion
}
