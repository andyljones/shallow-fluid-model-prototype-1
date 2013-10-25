using UnityEditor;
using UnityEngine;

public class SimpleGridGenerator : IGridGenerator
{
    public INode[] Nodes { get; private set; }

    public int[] MeshTriangles { get; private set; }

    private PolarAzimuthalHelper helper;

    public SimpleGridGenerator(float radius, float desiredResolution)
    {
        helper = new PolarAzimuthalHelper(desiredResolution / radius);

        GenerateNodes(radius);
    }

    private void GenerateNodes(float radius)
    {
        Nodes = new INode[helper.NormalizedGridPoints.Length];

        Vector3[] nodeDirections = helper.NormalizedGridPoints;

        for (int i = 0; i < nodeDirections.Length; i++)
        {
            Vector3 nodePosition = radius * nodeDirections[i];
            Nodes[i] = new SimpleNode(i) { Position = nodePosition };
        }
    }
}