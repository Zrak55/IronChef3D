using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    #region Constructors
    //Constructors copied from Node class. See Node class for detailed comments.
    public Selector() { }

    public Selector(string name)
    {
        this.name = name;
    }

    public Selector(params Node[] childParam)
    {
        foreach (Node child in childParam)
        {
            this.children.Add(child);
        }
    }

    public Selector(string name, params Node[] childParam)
    {
        this.name = name;
        foreach (Node child in childParam)
            this.children.Add(child);
    }
    #endregion
    #region Functions
    //The node will run its first child. If it fails, keep going through children. Otherwise, continue with that node.
    //Selector needs more testing to confirm it works as is.
    public override STATUS proccess()
    {
        STATUS childStatus;

        //Updated version that runs through every child in one update, but will only keep going if the nodes fail. Returns once we hit success or running
        foreach (Node child in children)
        {
            childStatus = child.proccess();
            if (childStatus == STATUS.SUCCESS || childStatus == STATUS.RUNNING)
            {
                status = childStatus;
                return status;
            }
        }
        status = STATUS.SUCCESS;
        return status;
    }
    #endregion
}
