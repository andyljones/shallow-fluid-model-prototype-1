using UnityEngine;

public interface IUsableGridElement
{
    int Index { get; }

    int VertexIndex { get; }

    Vector3 Direction { get; }

    Boundary[] Boundaries { get; }
}