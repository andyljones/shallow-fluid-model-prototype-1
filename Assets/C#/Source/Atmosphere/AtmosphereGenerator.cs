using System.Linq;
using UnityEngine;

//TODO: Holy shit refactor this, it's horrible. Extract a boundary generator?
public class AtmosphereGenerator<TSurfaceElement, TAtmosphereElement> : IAtmosphereGenerator<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IUsableSurfaceElement
    where TAtmosphereElement : IGenerableAtmosphericElement, new()
{
    private readonly float _height;

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

        _atmosphereElements = _surfaceElements.Select(GenerateAtmosphereElement).ToArray();
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
        var topAndBottomBoundaries = GenerateTopAndBottom(surfaceBoundaries);
        var bottomBoundary = new[] {topAndBottomBoundaries[0]};
        var topBoundary = new[] {topAndBottomBoundaries[1]};
        var sideBoundaries = GenerateSides(surfaceBoundaries);

        var atmosphericBoundaries = bottomBoundary.Concat(sideBoundaries).Concat(topBoundary);

        return atmosphericBoundaries.ToArray();
    }

    private Boundary[] GenerateTopAndBottom(Boundary[] surfaceBoundaries)
    {
        int[] boundaryPoints = surfaceBoundaries.SelectMany(boundary => boundary.VertexIndices).Distinct().ToArray();

        var bottomBoundary = new Boundary() {NeighboursIndex = -1, VertexIndices = boundaryPoints};

        int offset = _surfaceVertices.Length*2;
        var topBoundary = new Boundary() {NeighboursIndex = -1, VertexIndices = boundaryPoints.Select(i => i + offset).ToArray()};

        return new[] { bottomBoundary, topBoundary };
    }

    private Boundary[] GenerateSides(Boundary[] surfaceBoundaries)
    {
        var atmosphericBoundaries = surfaceBoundaries.Select(GenerateSide);

        return atmosphericBoundaries.ToArray();
    }

    private Boundary GenerateSide(Boundary surfaceBoundary)
    {
        int offset = _surfaceVertices.Length * 2;

        int neighbourIndex = surfaceBoundary.NeighboursIndex;

        var lowerVertexIndicies = surfaceBoundary.VertexIndices;
        var upperVertexIndicies = surfaceBoundary.VertexIndices.Select(index => index + offset);
        int[] vertexIndicies = lowerVertexIndicies.Concat(upperVertexIndicies).ToArray();

        var atmosphericBoundary = new Boundary() {NeighboursIndex = neighbourIndex, VertexIndices = vertexIndicies};

        return atmosphericBoundary;
    }

    public TAtmosphereElement[] AtmosphereElements()
    {
        return _atmosphereElements;
    }

    public Vector3[] AtmosphereVertices()
    {
        var bottomLayer = _surfaceVertices;
        var middleLayer = _surfaceVertices.Select(vertex => (_height / 2 + vertex.magnitude)*vertex.normalized);
        var topLayer = _surfaceVertices.Select(vertex => (_height + vertex.magnitude) * vertex.normalized);

        return bottomLayer.Concat(middleLayer).Concat(topLayer).ToArray();
    }
}
