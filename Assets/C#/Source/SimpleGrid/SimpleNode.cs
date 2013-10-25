using System.Runtime.InteropServices;
using UnityEngine;

public struct SimpleNode : INode
{
    public int Index { get; private set; }

    public float Radius { get; private set; }

    public float Height { get; private set; }

    public Vector3 Direction { get; set; }

    public SimpleNode(int index) : this()
    {
        Index = index;
    }
}

