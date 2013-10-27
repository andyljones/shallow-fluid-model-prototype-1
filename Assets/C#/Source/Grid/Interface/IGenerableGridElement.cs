using UnityEngine;

public interface IGenerableGridElement
{
    int Index { set; }

    int VertexIndex { set; }

    Vector3 Direction { set; }

    Boundary[] Boundaries { set; }
}