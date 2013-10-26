using UnityEngine;

public interface IGenerableNode
{
    int Index { set; }

    int VertexIndex { set; }

    float Radius { set; }

    Vector3 Direction { set; }

    Boundary[] Boundaries { set; }
}