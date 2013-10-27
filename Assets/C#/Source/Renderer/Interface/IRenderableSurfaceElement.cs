using UnityEngine;

public interface IRenderableSurfaceElement
{
    int VertexIndex { get; }
    
    float Radius { get; }

    Vector3 Direction { get; }

    Boundary[] Boundaries { get; }
}