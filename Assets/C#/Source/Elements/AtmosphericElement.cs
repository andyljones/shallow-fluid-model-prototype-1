using UnityEngine;

public class AtmosphericElement : IGenerableAtmosphericElement
{
    public int Index { get; set; }

    public int VertexIndex { get; set; }

    public float Radius { get; set; }

    public float Height { get; set; }

    public Vector3 Direction { get; set; }

    public Boundary[] Boundaries { get; set; }
}
