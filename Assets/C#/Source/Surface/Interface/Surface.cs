using UnityEngine;

public class Surface<TSurfaceElement>
{
    public Surface(TSurfaceElement[] elements, Vector3[] vectors)
    {
        Elements = elements;
        Vectors = vectors;
    }

    public TSurfaceElement[] Elements { get; private set; }

    public Vector3[] Vectors { get; private set; }
}