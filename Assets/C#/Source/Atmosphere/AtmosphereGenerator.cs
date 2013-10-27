using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

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

        GenerateAtmosphereElements();
        GenerateAtmosphereVertices();
    }

    //TODO: Refactor this into a property rather than a method, and apply the same pattern across the other generators
    public TAtmosphereElement[] AtmosphereElements()
    {
        return _atmosphereElements;
    }

    public Vector3[] AtmosphereVertices()
    {
        return _atmosphericVertices;
    }

    private void GenerateAtmosphereElements()
    {
        var boundaryGenerator = new AtmosphericBoundaryGenerator(_surfaceVertices.Length);
        _atmosphereElements = _surfaceElements.Select(element => GenerateAtmosphereElement(element, boundaryGenerator)).ToArray();
    }

    private TAtmosphereElement GenerateAtmosphereElement(TSurfaceElement surfaceElement, AtmosphericBoundaryGenerator boundaryGenerator)
    {
        var index = surfaceElement.Index;
        var vertexIndex = surfaceElement.VertexIndex + _surfaceVertices.Length;
        var radius = surfaceElement.Radius;
        var height = _height;
        var direction = surfaceElement.Direction;
        var boundaries = boundaryGenerator.BoundariesOf(surfaceElement.Boundaries);

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

    private void GenerateAtmosphereVertices()
    {
        var bottomLayer = _surfaceVertices;
        var middleLayer = _surfaceVertices;
        var topLayer = _surfaceVertices;

        _atmosphericVertices = bottomLayer.Concat(middleLayer).Concat(topLayer).ToArray();
    }

}
