using UnityEngine;

public interface IRenderableAtmosphereElement
{
    int Index { get; }

    float Radius { get; }
    
    float Height { get; }

    Vector3 Direction { get; }

    int[] CentralVertexIndices { get; }

    Boundary[] Boundaries { get; } 

    ISimulableConditions Conditions { get; }
}