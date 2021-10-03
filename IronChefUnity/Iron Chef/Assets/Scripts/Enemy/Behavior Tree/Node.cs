using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    #region Variables
    /*Each status indicates what the tree should do upon arriving at that node.
     * If the node is a method node, "this node's function" is an in game effect, otherwise it just is more logical operations
     *  Failure: Return failure to parent if parent is sequential, move to sibling if parent is selector
     *  Success: Move to sibling if the parent is sequential, return failure if parent is a selector
     *  Running: Do this node's function again, until it returns failure or success
     *  Start: The tree hasn't gotten to this node yet, so do its function
    */
    public enum STATUS {FAILURE, SUCCESS, RUNNING, START};
    //Nodes representing the children of the currrent node.
    public List<Node> children = new List<Node>();
    //Name of the node, with method nodes being named after the method they invoke.
    public string name { get; set; }
    //See above for enum explanation. Each node starts with status Start to indicate they haven't taken any action yet.
    public STATUS status { get; set; } = STATUS.START;
    //Int that keeps track of the child number we are currently on.
    public int currentChild = 0;

    #endregion
    #region Constructors
    public Node() {}

    public Node(string name)
    {
        this.name = name;
    }

    //Use a param here so children can be added at declaration.
    //You can go back later and add each child individually from within the enemy behavior class, because the param gets complicated with parentheses.
    public Node(params Node[] childParam)
    {
        foreach (Node child in childParam)
        {
            this.children.Add(child);
        }
    }

    //Same as above, but includes name.
    //The constuctors without name are likely unnecessary, but will keep for now.
    public Node(string name, params Node[] childParam)
    {
        this.name = name;
        foreach (Node child in childParam)
            this.children.Add(child);
    }
    #endregion
    #region Functions
    //Base function for all nodes. Override in other types of nodes and add functionality
    public virtual STATUS proccess()
    {
        status = STATUS.SUCCESS;
        return status;
    }
    
    //Recursively prints out the tree.
    public string printName(int tab)
    {
        string output = name + "\n";

        foreach (Node child in children)
        {
            //Adds in tabs to show inheritance in the debug log
            for (int i = 0; i < tab; i++)
                output += "\t";
            output += child.printName(tab + 1);
        }
        return output;
    }

    //TODO: untested function
    //Recursively resets the tree.
    public void reset()
    {
        status = STATUS.START;
        foreach (Node child in children)
            child.reset();
    }
#endregion
}
