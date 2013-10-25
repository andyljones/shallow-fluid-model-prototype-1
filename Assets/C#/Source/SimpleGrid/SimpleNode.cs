using UnityEngine;

public struct SimpleNode : INode
{
    public int Index { get; private set; }

    public Vector3 Position { get; set; }

    public SimpleNode(int index) : this()
    {
        this.Index = index;
    }
}

