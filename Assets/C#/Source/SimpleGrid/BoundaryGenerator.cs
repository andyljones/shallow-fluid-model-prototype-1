using UnityEngine;

public class BoundaryGenerator
{
    private PolarAzimuthalHelper nodeHelper;
    private PolarAzimuthalHelper vertexHelper;

    public BoundaryGenerator(PolarAzimuthalHelper nodeHelper, PolarAzimuthalHelper vertexHelper)
    {
        this.vertexHelper = vertexHelper;
        this.nodeHelper = nodeHelper;
    }

    public Boundary[] BoundariesForNode(int nodeIndex, int vertexIndex)
    {
        Boundary[] boundaries;

        if (nodeIndex == 0)
        {
            boundaries = GenerateNorthPolarBoundary(nodeIndex, vertexIndex);
        }
        else if (nodeIndex == nodeHelper.NumberOfGridPoints - 1)
        {
            boundaries = GenerateSouthPolarBoundary(nodeIndex, vertexIndex);
        }
        else
        {
            boundaries = GenerateMidlatitudinalBoundaries(nodeIndex, vertexIndex);
        }

        return boundaries;
    }

    private Boundary[] GenerateNorthPolarBoundary(int nodeIndex, int vertexIndex)
    {
        var boundaries = new Boundary[nodeHelper.NumberOfLongitudes];

        for (int nodeOffset = 0; nodeOffset < nodeHelper.NumberOfLongitudes; nodeOffset++)
        {
            int neighbouringIndex = nodeHelper.Offset(nodeIndex, 1, nodeOffset);

            int vertexOffset = nodeOffset*2;
            var vertexIndices = new int[] { vertexHelper.Offset(vertexIndex, 1, vertexOffset - 1), 
                                              vertexHelper.Offset(vertexIndex, 1, vertexOffset + 0), 
                                              vertexHelper.Offset(vertexIndex, 1, vertexOffset + 1)};

            boundaries[nodeOffset] = new Boundary() {NeighboursIndex = neighbouringIndex, VertexIndices = vertexIndices};
        }

        return boundaries;
    }

    private Boundary[] GenerateSouthPolarBoundary(int nodeIndex, int vertexIndex)
    {
        var boundaries = new Boundary[nodeHelper.NumberOfLongitudes];

        for (int nodeOffset = 0; nodeOffset < nodeHelper.NumberOfLongitudes; nodeOffset++)
        {
            int neighbouringIndex = nodeHelper.Offset(nodeIndex, -1, nodeOffset);
            
            int vertexOffset = nodeOffset * 2;
            var vertexIndices = new int[] { vertexHelper.Offset(vertexIndex, -1, vertexOffset + 1), 
                                              vertexHelper.Offset(vertexIndex, -1, vertexOffset + 0), 
                                              vertexHelper.Offset(vertexIndex, -1, vertexOffset - 1)};

            boundaries[nodeOffset] = new Boundary() { NeighboursIndex = neighbouringIndex, VertexIndices = vertexIndices };
        }

        return boundaries;
    }

    private Boundary[] GenerateMidlatitudinalBoundaries(int nodeIndex, int vertexIndex)
    {
        int nNeighbour = nodeHelper.Offset(nodeIndex, -1, 0);
        int eNeighbour = nodeHelper.Offset(nodeIndex, 0, 1);
        int sNeighbour = nodeHelper.Offset(nodeIndex, 1, 0);
        int wNeighbour = nodeHelper.Offset(nodeIndex, 0, -1);

        int nwVertex = vertexHelper.Offset(vertexIndex, -1, -1);
        int nVertex  = vertexHelper.Offset(vertexIndex, -1,  0);
        int neVertex = vertexHelper.Offset(vertexIndex, -1,  1);
        int eVertex  = vertexHelper.Offset(vertexIndex,  0,  1);
        int seVertex = vertexHelper.Offset(vertexIndex,  1,  1);
        int sVertex  = vertexHelper.Offset(vertexIndex,  1,  0);
        int swVertex = vertexHelper.Offset(vertexIndex,  1, -1);
        int wVertex  = vertexHelper.Offset(vertexIndex,  0, -1);

        Boundary nBoundary = new Boundary() { NeighboursIndex = nNeighbour, VertexIndices = new int[] { neVertex, nVertex, nwVertex } };
        Boundary eBoundary = new Boundary() { NeighboursIndex = eNeighbour, VertexIndices = new int[] { seVertex, eVertex, neVertex } };
        Boundary sBoundary = new Boundary() { NeighboursIndex = sNeighbour, VertexIndices = new int[] { swVertex, sVertex, seVertex } };
        Boundary wBoundary = new Boundary() { NeighboursIndex = wNeighbour, VertexIndices = new int[] { nwVertex, wVertex, swVertex } };

        Boundary[] boundaries = new Boundary[4] { nBoundary, wBoundary, sBoundary, eBoundary };

        return boundaries;
    }
}