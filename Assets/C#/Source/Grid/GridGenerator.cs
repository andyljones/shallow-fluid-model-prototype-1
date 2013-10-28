using UnityEngine;

public class GridGenerator<TElement> : IGridGenerator<TElement>
    where TElement : IGenerableGridElement, new()
{
    private readonly Vector3[] _nodeDirections;
    private readonly Vector3[] _vertexDirections;
    private readonly PolarAzimuthalIndexHelper _nodeIndexHelper;
    private readonly PolarAzimuthalIndexHelper _vertexIndexHelper;

    public GridGenerator(int numberOfLatitudes, int numberOfLongitudes)
    {
        var nodeGenerator = new PolarAzimuthalGridGenerator(numberOfLatitudes, numberOfLongitudes);
        _nodeDirections = nodeGenerator.Directions;
        _nodeIndexHelper = nodeGenerator.IndexHelper;

        var vertexGenerator = new PolarAzimuthalGridGenerator(2*(_nodeIndexHelper.NumberOfLatitudes - 1) + 1, 2*_nodeIndexHelper.NumberOfLongitudes);
        _vertexDirections = vertexGenerator.Directions;
        _vertexIndexHelper = vertexGenerator.IndexHelper;
    }

    public Grid<TElement> Grid()
    {
        var elements = GridElements();
        var vectors = GridVectors();

        return new Grid<TElement>(elements, vectors);
    }

    public TElement[] GridElements()
    {
        var gridElements = new TElement[_nodeDirections.Length];

        var nodeDirections = _nodeDirections;
        var boundaryGenerator = new BoundaryGenerator(_nodeIndexHelper, _vertexIndexHelper);

        for (int nodeIndex = 0; nodeIndex < nodeDirections.Length; nodeIndex++)
        {
            var vertexIndex = VertexIndexCorrespondingToNode(nodeIndex);
            var boundaries = boundaryGenerator.BoundariesForNode(nodeIndex, vertexIndex);

            gridElements[nodeIndex] = new TElement() { Index = nodeIndex,
                                             VertexIndex = vertexIndex, 
                                             Direction = nodeDirections[nodeIndex], 
                                             Boundaries = boundaries};
        }

        return gridElements;
    }

    private int VertexIndexCorrespondingToNode(int index)
    {
        int meshIndex;

        if (index == 0)
        {
            meshIndex = 0;
        }
        else if (index == _nodeIndexHelper.NumberOfGridPoints - 1)
        {
            meshIndex = _vertexIndexHelper.NumberOfGridPoints - 1;
        }
        else
        {
            var polarNodeIndex = _nodeIndexHelper.PolarIndexOf(index);
            var azimuthalNodeIndex = _nodeIndexHelper.AzimuthalIndexOf(index);

            meshIndex = _vertexIndexHelper.IndexOf(2*polarNodeIndex, 2*azimuthalNodeIndex);
        }

        return meshIndex;
    }

    private Vector3[] GridVectors()
    {
        return _vertexDirections;
    }
}