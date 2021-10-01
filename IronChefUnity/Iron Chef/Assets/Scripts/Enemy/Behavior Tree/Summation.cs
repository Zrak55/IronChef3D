using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summation : Node
{
    //This is a carbon copy of Sequence node. The only difference is it won't stop if one of the children fails.
    //Currently, the only intended use for this function is the root.
    #region Constructors
    public Summation() { }

    public Summation(string name)
    {
        this.name = name;
    }

    public Summation(params Node[] childParam)
    {
        foreach (Node child in childParam)
        {
            this.children.Add(child);
        }
    }

    public Summation(string name, params Node[] childParam)
    {
        this.name = name;
        foreach (Node child in childParam)
            this.children.Add(child);
    }
    #endregion
    #region Function
    public override STATUS proccess()
    {
        Node.STATUS childStatus;

        //If any children are running skip to those.
        if (this.status == STATUS.RUNNING)
        {
            foreach (Node child in children)
            {
                if (child.status == STATUS.RUNNING)
                {
                    status = child.proccess();
                    return status;
                }
            }
        }

        foreach (Node child in children)
            childStatus = child.proccess();
        status = STATUS.SUCCESS;
        return status;
    }
    #endregion
}
