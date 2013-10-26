using System;
using System.Linq;
using UnityEngine;

public class SurfaceGenerator<TNode> : ISurfaceGenerator<TNode, TNode>
    where TNode : class, IUsableGridElement, IGenerableSurfaceElement
{
    private readonly float _radius;

    public SurfaceGenerator(float radius)
    {
        _radius = radius;
    }

    public TNode[] SurfaceElements(TNode[] nodes)
    {
        foreach (var gridElement in nodes)
        {
            gridElement.Radius = _radius;
        }

        return nodes;
    }

    public Vector3[] BoundaryVertices(Vector3[] boundaryDirections)
    {
        var boundaryVertices = boundaryDirections.Select(direction => _radius*direction.normalized);

        return boundaryVertices.ToArray();
    }
}
