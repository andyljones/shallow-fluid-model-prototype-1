using UnityEngine;

public interface IUsableSurfaceElement
{
    int Index { get; }

    int VertexIndex { get; }

    float Radius { get; }

    Vector3 Direction { get; }

    Boundary[] Boundaries { get; }
}