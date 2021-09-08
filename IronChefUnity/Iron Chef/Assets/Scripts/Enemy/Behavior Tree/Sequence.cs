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
        //Run the process for the child we are currently on. If it fails, then stop the sequence. If it is running, then return running so we can return next update.
        STATUS childStatus = children[currentChild].proccess();
        if (childStatus != STATUS.SUCCESS)
            return childStatus;

        //If it succeeds, then check to make sure we aren't out of bounds. If we are, then the sequence was succesful. Otherwise, we will run the next child next update.
        currentChild++;
        if (currentChild < children.Count)
            return STATUS.RUNNING;
        currentChild = 0;
        return STATUS.SUCCESS;

    }
    #endregion
}
