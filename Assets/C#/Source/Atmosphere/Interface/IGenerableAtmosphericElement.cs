﻿using UnityEngine;

public interface IGenerableAtmosphericElement
{
    int Index { set; }

    int[] CentralVertexIndices { set; }

    float Radius { set; }

    float Height { set; }

    Vector3 Direction { set; }

    Boundary[] Boundaries { set; }
}