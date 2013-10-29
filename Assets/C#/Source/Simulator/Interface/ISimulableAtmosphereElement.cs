using UnityEngine;

public interface ISimulableAtmosphereElement
{
    int Index { get; }

    float Radius { get; }

    Vector3 Direction { get; }

    Boundary[] Boundaries { get; }

    ISimulableConditions Conditions { get; set; }
}

