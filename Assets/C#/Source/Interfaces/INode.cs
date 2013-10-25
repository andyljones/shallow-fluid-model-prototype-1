using UnityEngine;

public interface INode
{
    int Index { get; }

    float Radius { get; }

    float Height { get; }

    Vector3 Direction { get; }
}