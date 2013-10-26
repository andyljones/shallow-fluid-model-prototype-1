using UnityEngine;

public struct SimpleNode : IGenerableNode
{
    public int Index { get; set; }

    public int VertexIndex { get; set; }

    public float Radius { get; set; }

    public Vector3 Direction { get; set; }

    public Boundary[] Boundaries { get; set; }
}

