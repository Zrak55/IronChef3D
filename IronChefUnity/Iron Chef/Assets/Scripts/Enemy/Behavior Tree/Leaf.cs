using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Node
{
    //This is a delegate which allows us to pass a method into our leaf function.
    public delegate STATUS method();
    method leafAction;

    #region Constructors
    Leaf() { }

    //These constructors are the same as the Node class, with the delegate added.
    public Leaf(string name, method leafAction)
    {
        this.name = name;
        this.leafAction = leafAction;
    }

    //Once again, unsure if this constructor will ever be used.
    public Leaf(method leafAction, params Node[] childParam)
    {
        this.leafAction = leafAction;
        foreach (Node child in childParam)
        {
            this.children.Add(child);
        }
    }

    public Leaf(string name, method leafAction, params Node[] childParam)
    {
        this.name = name;
        this.leafAction = leafAction;
        foreach (Node child in childParam)
            this.children.Add(child);
    }
    #endregion
    #region Functions
    //Leaf proccess override for leaf, where we run the delegate function.
    public override STATUS proccess()
    {
        if (leafAction == null)
            return STATUS.FAILURE;
        return leafAction();
    }
    #endregion
}
