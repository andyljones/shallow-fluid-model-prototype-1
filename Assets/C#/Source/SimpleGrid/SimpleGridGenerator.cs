using System.Linq;
using System.Reflection.Emit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEditor;
using UnityEngine;

public class SimpleGridGenerator : IGridGenerator
{
    public INode[] Nodes { get; private set; }
    public Vector3[] MeshVertices { get; private set; }
    public int[] MeshTriangles { get; private set; }

    private PolarAzimuthalHelper nodeHelper;
    private PolarAzimuthalHelper vertexHelper;

    public SimpleGridGenerator(float radius, float desiredResolution)
    {
        nodeHelper = new PolarAzimuthalHelper(desiredResolution / radius);
        vertexHelper = new PolarAzimuthalHelper(2*(nodeHelper.NumberOfLatitudes - 1) + 1, 2*nodeHelper.NumberOfLongitudes);

        GenerateNodes(radius);
        GenerateVertices(radius);
        //GenerateNodeBoundaries();
    }

    private void GenerateNodes(float radius)
    {
        Nodes = new INode[nodeHelper.NormalizedGridPoints.Length];

        Vector3[] nodeDirections = nodeHelper.NormalizedGridPoints;

        for (int i = 0; i < nodeDirections.Length; i++)
        {
            Nodes[i] = new SimpleNode(i) { Direction = nodeDirections[i] };
        }
    }

    public void GenerateVertices(float radius)
    {
        Vector3[] vertexDirections = vertexHelper.NormalizedGridPoints;
        
        MeshVertices = vertexDirections.Select(vector => radius*vector).ToArray();
    }

}