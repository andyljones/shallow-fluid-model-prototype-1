using System;
using UnityEngine;

public class GridElement : IGenerableGridElement, IUsableGridElement
{
    public int Index { get; set; }

    public int VertexIndex { get; set; }

    public Vector3 Direction { get; set; }

    public Boundary[] Boundaries { get; set; }
}

