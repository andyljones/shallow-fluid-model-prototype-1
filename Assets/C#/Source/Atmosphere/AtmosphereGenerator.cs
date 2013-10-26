using System.Linq;
using UnityEngine;

public class AtmosphereGenerator<TSurfaceElement, TAtmosphereElement> : IAtmosphereGenerator<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IUsableSurfaceElement
    where TAtmosphereElement : IGenerableAtmosphericElement, new()
{
    private float _height;

    private TAtmosphereElement[] _atmosphereElements;
    private Vector3[] _atmosphericVertices;

    private TSurfaceElement[] _surfaceElements;
    private Vector3[] _surfaceVertices;

    public AtmosphereGenerator(float height)
    {
        _height = height;
    }

    public void GenerateAtmosphere(TSurfaceElement[] surfaceElements, Vector3[] surfaceVertices)
    {
        _surfaceElements = surfaceElements;
        _surfaceVertices = surfaceVertices;

        _atmosphereElements = _surfaceElements.Select(surfaceElement => GenerateAtmosphereElement(surfaceElement)).ToArray();
    }


    private TAtmosphereElement GenerateAtmosphereElement(TSurfaceElement surfaceElement)
    {
        var index = surfaceElement.Index;
        var vertexIndex = CalculateVertexIndex(surfaceElement.VertexIndex);
        var radius = surfaceElement.Radius;
        var height = _height;
        var direction = surfaceElement.Direction;
        var boundaries = GenerateBoundaries(surfaceElement.Boundaries);

        var atmosphereElement = new TAtmosphereElement()
        {
            Index = index,
            VertexIndex = vertexIndex,
            Radius = radius,
            Height = height,
            Direction = direction,
            Boundaries = boundaries
        };

        return atmosphereElement;
    }

    private int CalculateVertexIndex(int vertexIndex)
    {
        return vertexIndex + _surfaceVertices.Length;
    }

    private Boundary[] GenerateBoundaries(Boundary[] surfaceBoundaries)
    {
        var numberOfBoundaries = surfaceBoundaries.Length + 2;
        var atmosphericBoundaries = new Boundary[numberOfBoundaries];

        //GenerateTopBoundary(surfaceBoundaries, atmosphericBoundaries);

        return atmosphericBoundaries;
    }


    public TAtmosphereElement[] AtmosphereElements()
    {
        return _atmosphereElements;
    }

    public Vector3[] BoundaryPoints()
    {
        throw new System.NotImplementedException();
    }
}
