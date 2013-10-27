using UnityEngine;

public interface IRenderableAtmosphereElement
{
    float Radius { get; }
    
    float Height { get; }

    Vector3 Direction { get; }

    int[] CentralVertexIndices { get; }

    Boundary[] Boundaries { get; }
}