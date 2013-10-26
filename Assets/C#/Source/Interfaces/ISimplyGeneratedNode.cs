using UnityEngine;

public interface ISimplyGeneratedNode
{
    int Index { get; set; }

    int VertexIndex { get; set; }

    Vector3 Direction { get; set; }

    Boundary[] Boundaries { get; set; }
}