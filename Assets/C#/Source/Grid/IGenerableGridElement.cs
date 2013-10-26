using UnityEngine;

public interface IGenerableGridElement
{
    int Index { set; }

    int VertexIndex { set; }

    float Radius { set; }

    Vector3 Direction { set; }

    Boundary[] Boundaries { set; }
}