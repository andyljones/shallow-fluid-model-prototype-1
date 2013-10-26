using System.Linq;
using UnityEngine;

public class SimpleGridGenerator<TNode> : IGridGenerator<TNode>
    where TNode : ISimplyGeneratedNode, new()
{
    public TNode[] Nodes { get; private set; }
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
    }

    private void GenerateNodes(float radius)
    {
        Nodes = new TNode[nodeHelper.NormalizedGridPoints.Length];

        Vector3[] nodeDirections = nodeHelper.NormalizedGridPoints;
        BoundaryGenerator boundaryGenerator = new BoundaryGenerator(nodeHelper, vertexHelper);

        for (int nodeIndex = 0; nodeIndex < nodeDirections.Length; nodeIndex++)
        {
            int meshIndex = MeshIndexCorrespondingToNode(nodeIndex);
            Boundary[] boundaries = boundaryGenerator.BoundariesForNode(nodeIndex, meshIndex);

            Nodes[nodeIndex] = new TNode() { Index = nodeIndex, 
                                             Direction = nodeDirections[nodeIndex], 
                                             MeshIndex = meshIndex, 
                                             Boundaries = boundaries};
        }
    }

    private int MeshIndexCorrespondingToNode(int index)
    {
        int meshIndex;

        if (index == 0)
        {
            meshIndex = 0;
        }
        else if (index == nodeHelper.NumberOfGridPoints - 1)
        {
            meshIndex = vertexHelper.NumberOfGridPoints - 1;
        }
        else
        {
            int polarNodeIndex = nodeHelper.PolarIndexOf(index);
            int azimuthalNodeIndex = nodeHelper.AzimuthalIndexOf(index);

            meshIndex = vertexHelper.IndexOf(2*polarNodeIndex, 2*azimuthalNodeIndex);
        }

        return meshIndex;
    }

    private void GenerateVertices(float radius)
    {
        MeshVertices = vertexHelper.NormalizedGridPoints.Select(vertex => radius * vertex).ToArray();
    }
}