using UnityEngine;

public class FakeGridElement : IGenerableGridElement, IUsableGridElement
{
    public int Index { get; set; }

    public int VertexIndex { get; set; }

    public Vector3 Direction { get; set; }

    public Boundary[] Boundaries { get; set; }
}