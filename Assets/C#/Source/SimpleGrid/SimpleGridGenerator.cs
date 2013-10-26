using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

public class SimpleGridGenerator<TNode> : IGridGenerator<TNode>
    where TNode : ISimplyGeneratedNode, new()
{
    public TNode[] Nodes { get; private set; }

    private Vector3[] nodeGrid;
    private Vector3[] vertexGrid;
    private PolarAzimuthalHelper nodeHelper;
    private PolarAzimuthalHelper vertexHelper;

    public SimpleGridGenerator(int numberOfLatitudes, int numberOfLongitudes)
    {
        var nodeGenerator = new PolarAzimuthalGenerator(numberOfLatitudes, numberOfLongitudes);
        nodeGrid = nodeGenerator.NormalizedGridPoints;
        nodeHelper = nodeGenerator.NavigationHelper;

        var vertexGenerator = new PolarAzimuthalGenerator(2*(nodeHelper.NumberOfLatitudes - 1) + 1, 2*nodeHelper.NumberOfLongitudes);
        vertexGrid = vertexGenerator.NormalizedGridPoints;
        vertexHelper = vertexGenerator.NavigationHelper;

        GenerateNodes();
    }

    private void GenerateNodes()
    {
        Nodes = new TNode[nodeGrid.Length];

        Vector3[] nodeDirections = nodeGrid;
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
}