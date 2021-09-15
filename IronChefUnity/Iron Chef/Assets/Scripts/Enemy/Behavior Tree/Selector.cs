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
    //This function is OUTDATED. Needs to be updated, not currently in use.
    //The node will run its first child. If it fails, keep going through children. Otherwise, continue with that node.
    //Selector needs more testing to confirm it works as is.
    public override STATUS proccess()
    {
        //Run the process for the child we are currently on. If it fails, then stop the sequence. If it is running, then return running so we can return next update.
        STATUS childStatus = children[currentChild].proccess();
        if (childStatus != STATUS.FAILURE)
            return childStatus;

        //If it succeeds, then check to make sure we aren't out of bounds. If we are, then the sequence was succesful. Otherwise, we will run the next child next update.
        currentChild++;
        if (currentChild < children.Count)
            return STATUS.RUNNING;
        currentChild = 0;
        return STATUS.FAILURE;

    }
    #endregion
}
