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
    }

    private void GenerateNodes(float radius)
    {
        Nodes = new INode[nodeHelper.NormalizedGridPoints.Length];

        Vector3[] nodeDirections = nodeHelper.NormalizedGridPoints;

        for (int nodeIndex = 0; nodeIndex < nodeDirections.Length; nodeIndex++)
        {
            int meshIndex = MeshIndexCorrespondingToNode(nodeIndex);
            Boundary[] boundaries = GenerateBoundariesForNode(nodeIndex, meshIndex);

            Nodes[nodeIndex] = new SimpleNode() { Index = nodeIndex, 
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



    private Boundary[] GenerateBoundariesForNode(int nodeIndex, int meshIndex)
    {
        return new Boundary[0];
    }

    private void VertexIndexFromNodeIndex(int index)
    {
        
    }

    private void GenerateVertices(float radius)
    {
        MeshVertices = vertexHelper.NormalizedGridPoints.Select(vertex => radius * vertex).ToArray();
    }
}