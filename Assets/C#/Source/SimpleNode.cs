using System.Runtime.InteropServices;
using UnityEngine;

public struct SimpleNode : ISimplyGeneratedNode
{
    public int Index { get; set; }

    public int VertexIndex { get; set; }

    public Vector3 Direction { get; set; }

    public Boundary[] Boundaries { get; set; }
}

