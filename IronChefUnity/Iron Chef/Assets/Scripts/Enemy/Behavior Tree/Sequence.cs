using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    #region Constructors
    public Sequence() { }

    public Sequence(string name)
    {
        this.name = name;
    }

    public Sequence(params Node[] childParam)
    {
        foreach (Node child in childParam)
        {
            this.children.Add(child);
        }
    }

    public Sequence(string name, params Node[] childParam)
    {
        this.name = name;
        foreach (Node child in childParam)
            this.children.Add(child);
    }
    //A sequence node has the intention of running all of its children nodes, in sequence
    #endregion
    #region Function
    //The idea of the sequence node is to run all the children, one at a time and in order. It will stop if any of them fail.
    public override STATUS proccess()
    {
        Node.STATUS childStatus;

        //If any children are running skip to those.
        foreach (Node child in children)
        {
            if (child.status == STATUS.RUNNING)
            {
                status = child.proccess();
                return status;
            }
        }

        //Updated version of the code that runs through every child every update frame. If a child ever fails, we exit the sequence.
        foreach (Node child in children)
        {
            childStatus = child.proccess();
            if (childStatus == STATUS.FAILURE || childStatus == STATUS.RUNNING)
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
