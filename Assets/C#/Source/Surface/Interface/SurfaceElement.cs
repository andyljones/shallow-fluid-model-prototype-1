using System;
using UnityEngine;

public class SurfaceElement : IGenerableSurfaceElement, IUsableSurfaceElement, IRenderableSurfaceElement
{
    public int Index { get; set; }

    public int VertexIndex { get; set; }

    public float Radius { get; set; }

    public Vector3 Direction { get; set; }

    public Boundary[] Boundaries { get; set; }
}

