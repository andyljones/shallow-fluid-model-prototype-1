using UnityEngine;

public interface IBaseNode
{
    int Index { get; }

    float Radius { get; }

    Vector3 Direction { get; }

    Boundary[] Boundaries { get; }
}