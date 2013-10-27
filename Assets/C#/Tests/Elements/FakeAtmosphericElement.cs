﻿using UnityEngine;

public class FakeAtmosphericElement : IGenerableAtmosphericElement
{
    public int Index { get; set; }

    public int[] CentralVertexIndicies { get; set; }

    public float Radius { get; set; }

    public float Height { get; set; }

    public Vector3 Direction { get; set; }

    public Boundary[] Boundaries { get; set; }
}
