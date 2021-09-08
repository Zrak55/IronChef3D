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
    public override STATUS proccess()
    {
        //TODO: this
        return base.proccess();
    }
    #endregion
}
