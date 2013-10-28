public class BoundaryGenerator
{
    private readonly PolarAzimuthalIndexHelper _nodeIndexHelper;
    private readonly PolarAzimuthalIndexHelper _vertexIndexHelper;

    public BoundaryGenerator(PolarAzimuthalIndexHelper nodeIndexHelper, PolarAzimuthalIndexHelper vertexIndexHelper)
    {
        _nodeIndexHelper = nodeIndexHelper;
        _vertexIndexHelper = vertexIndexHelper;
    }

    public Boundary[] BoundariesForNode(int nodeIndex, int vertexIndex)
    {
        Boundary[] boundaries;

        if (_nodeIndexHelper.NorthPoleIs(nodeIndex)) //TODO: Rewrite as "nodeIndex == _nodeIndexHelper.NorthPole"
        {
            boundaries = GenerateNorthPolarBoundary(nodeIndex, vertexIndex);
        }
        else if (_nodeIndexHelper.SouthPoleIs(nodeIndex))
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
        var numberOfLongitudes = _nodeIndexHelper.NumberOfLongitudes;
        var boundaries = new Boundary[numberOfLongitudes];

        for (int nodeOffset = 0; nodeOffset < _nodeIndexHelper.NumberOfLongitudes; nodeOffset++)
        {
            int neighbouringIndex = _nodeIndexHelper.Offset(nodeIndex, 1, nodeOffset);

            int vertexOffset = nodeOffset*2;
            var vertexIndices = new int[] { _vertexIndexHelper.Offset(vertexIndex, 1, vertexOffset - 1), 
                                              _vertexIndexHelper.Offset(vertexIndex, 1, vertexOffset + 0), 
                                              _vertexIndexHelper.Offset(vertexIndex, 1, vertexOffset + 1)};

            boundaries[nodeOffset] = new Boundary() {NeighboursIndex = neighbouringIndex, VertexIndices = vertexIndices};
        }

        return boundaries;
    }

    private Boundary[] GenerateSouthPolarBoundary(int nodeIndex, int vertexIndex)
    {
        var numberOfLongitudes = _nodeIndexHelper.NumberOfLongitudes;
        var boundaries = new Boundary[numberOfLongitudes];

        for (int nodeOffset = 0; nodeOffset < _nodeIndexHelper.NumberOfLongitudes; nodeOffset++)
        {
            // Offsetting to the west preserve ordering. Ensures the order of the indices' appearance is compatible with the renderer's clockwise rule.
            int neighbouringIndex = _nodeIndexHelper.Offset(nodeIndex, -1, -nodeOffset);
            
            int vertexOffset = -nodeOffset * 2; 
            var vertexIndices = new int[] { _vertexIndexHelper.Offset(vertexIndex, -1, vertexOffset + 1), 
                                            _vertexIndexHelper.Offset(vertexIndex, -1, vertexOffset + 0), 
                                            _vertexIndexHelper.Offset(vertexIndex, -1, vertexOffset - 1)};

            boundaries[nodeOffset] = new Boundary() { NeighboursIndex = neighbouringIndex, VertexIndices = vertexIndices };
        }

        return boundaries;
    }

    private Boundary[] GenerateMidlatitudinalBoundaries(int nodeIndex, int vertexIndex)
    {
        int nNeighbour = _nodeIndexHelper.Offset(nodeIndex, -1, 0);
        int eNeighbour = _nodeIndexHelper.Offset(nodeIndex, 0, 1);
        int sNeighbour = _nodeIndexHelper.Offset(nodeIndex, 1, 0);
        int wNeighbour = _nodeIndexHelper.Offset(nodeIndex, 0, -1);

        int nwVertex = _vertexIndexHelper.Offset(vertexIndex, -1, -1);
        int nVertex  = _vertexIndexHelper.Offset(vertexIndex, -1,  0);
        int neVertex = _vertexIndexHelper.Offset(vertexIndex, -1,  1);
        int eVertex  = _vertexIndexHelper.Offset(vertexIndex,  0,  1);
        int seVertex = _vertexIndexHelper.Offset(vertexIndex,  1,  1);
        int sVertex  = _vertexIndexHelper.Offset(vertexIndex,  1,  0);
        int swVertex = _vertexIndexHelper.Offset(vertexIndex,  1, -1);
        int wVertex  = _vertexIndexHelper.Offset(vertexIndex,  0, -1);

        var nBoundary = new Boundary() { NeighboursIndex = nNeighbour, VertexIndices = new int[] { neVertex, nVertex, nwVertex } };
        var eBoundary = new Boundary() { NeighboursIndex = eNeighbour, VertexIndices = new int[] { seVertex, eVertex, neVertex } };
        var sBoundary = new Boundary() { NeighboursIndex = sNeighbour, VertexIndices = new int[] { swVertex, sVertex, seVertex } };
        var wBoundary = new Boundary() { NeighboursIndex = wNeighbour, VertexIndices = new int[] { nwVertex, wVertex, swVertex } };

        var boundaries = new Boundary[] { nBoundary, wBoundary, sBoundary, eBoundary };

        return boundaries;
    }
}