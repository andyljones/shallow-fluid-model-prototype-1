using UnityEngine;

public class Atmosphere<TAtmosphereElement>
{
    public Atmosphere(TAtmosphereElement[] elements, Vector3[] vectors)
    {
        Elements = elements;
        Vectors = vectors;
    }

    public TAtmosphereElement[] Elements { get; private set; }

    public Vector3[] Vectors { get; private set; }
}