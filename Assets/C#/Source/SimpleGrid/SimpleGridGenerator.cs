using UnityEngine;

public class SimpleGridGenerator : IGridGenerator
{
    public INode[] Nodes { get; private set; }

    public UnityEngine.Vector3[] MeshVertices
    {
        get { throw new System.NotImplementedException(); }
    }

    public int[] MeshTriangles
    {
        get { throw new System.NotImplementedException(); }
    }

    private PolarAzimuthalHelper helper;

    public SimpleGridGenerator(float radius, float desiredResolution)
    {
        this.helper = new PolarAzimuthalHelper(desiredResolution / radius);
        this.Nodes = GenerateNodes(radius);
    }

    private INode[] GenerateNodes(float radius)
    {
        Nodes = new INode[helper.NormalizedGridPoints.Length];

        Vector3[] nodeDirections = helper.NormalizedGridPoints;

        for (int i = 0; i < nodeDirections.Length; i++)
        {
            Vector3 nodePosition = radius * nodeDirections[i];
            Nodes[i] = new SimpleNode(i) { Position = nodePosition };
        }

        return Nodes;
    }
}