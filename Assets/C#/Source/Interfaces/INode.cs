using UnityEngine;

public interface INode
{
    int Index { get; }

    float Radius { get; }

    Vector3 Direction { get; }

    int MeshIndex { get; }

    Boundary[] Boundaries { get; }
}