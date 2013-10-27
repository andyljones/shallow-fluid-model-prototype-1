using UnityEngine;

public interface IGenerableAtmosphericElement
{
    int Index { set; }

    int VertexIndex { set; } //TODO: Replace with an array of CentralVertexIndicies

    float Radius { set; }

    float Height { set; }

    Vector3 Direction { set; }

    Boundary[] Boundaries { set; }
}