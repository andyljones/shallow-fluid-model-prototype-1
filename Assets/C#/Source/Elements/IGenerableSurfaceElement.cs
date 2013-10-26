using UnityEngine;

public interface IGenerableSurfaceElement
{
    int Index { set; }

    int VertexIndex { set; }

    float Radius { set; }
    
    Vector3 Direction { set; }

    Boundary[] Boundaries { set; }
}