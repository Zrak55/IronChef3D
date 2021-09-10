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

    public Leaf(string name)
    {
        this.name = name;

    }

    public Leaf(string name, method leafAction)
    {
        this.name = name;
        this.leafAction = leafAction;
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

    //This will run if you forgot to declare a method in the constructor.
    public Node.STATUS defaultMethod()
    {
        Debug.Log("Forgot to declare a method for leaf node " + name + "!");
        return STATUS.SUCCESS;
    }

    #endregion
}
