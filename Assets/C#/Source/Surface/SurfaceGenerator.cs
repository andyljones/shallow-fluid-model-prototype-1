using System;
using System.Linq;
using UnityEngine;

public class SurfaceGenerator<TGridElement, TSurfaceElement> : ISurfaceGenerator<TGridElement, TSurfaceElement>
    where TGridElement : IUsableGridElement
    where TSurfaceElement : IGenerableSurfaceElement, new()
{
    private readonly float _radius;

    public SurfaceGenerator(float radius)
    {
        _radius = radius;
    }

    public TSurfaceElement[] SurfaceElements(TGridElement[] gridElements)
    {
        var surfaceElements = new TSurfaceElement[gridElements.Length];

        foreach (var gridElement in gridElements)
        {
            var surfaceElement = new TSurfaceElement() {Boundaries  = gridElement.Boundaries, 
                                                        Direction   = gridElement.Direction,
                                                        Index       = gridElement.Index,
                                                        VertexIndex = gridElement.VertexIndex, 
                                                        Radius      = _radius};

            surfaceElements[gridElement.Index] = surfaceElement;
        }

        return surfaceElements;
    }

    public Vector3[] BoundaryVertices(Vector3[] boundaryDirections)
    {
        var boundaryVertices = boundaryDirections.Select(direction => _radius*direction.normalized);

        return boundaryVertices.ToArray();
    }
}
