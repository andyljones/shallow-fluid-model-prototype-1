using UnityEngine;

public class Grid<TGridElement>
{
    public Grid(TGridElement[] elements, Vector3[] vectors)
    {
        Elements = elements;
        Vectors = vectors;
    }

    public TGridElement[] Elements { get; private set; }

    public Vector3[] Vectors { get; private set; }
}