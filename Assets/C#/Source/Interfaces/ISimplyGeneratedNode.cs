using UnityEngine;

public interface ISimplyGeneratedNode
{
    int Index { get; set; }

    float Radius { get; set; }

    Vector3 Direction { get; set; }

    int MeshIndex { get; set; }

    Boundary[] Boundaries { get; set; }
}