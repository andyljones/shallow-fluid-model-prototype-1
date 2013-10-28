using System.Collections.Generic;
using System.Linq;

public class AtmosphericBoundaryGenerator
{
    private readonly int _numberOfIndicesPerLayer;

    public AtmosphericBoundaryGenerator(int numberOfIndicesPerLayer)
    {
        _numberOfIndicesPerLayer = numberOfIndicesPerLayer;
    }

    public Boundary[] BoundariesOf(Boundary[] surfaceBoundaries)
    {
        var topAndBottomBoundaries = GenerateTopAndBottom(surfaceBoundaries);
        var sideBoundaries = GenerateSides(surfaceBoundaries);

        var atmosphericBoundaries = topAndBottomBoundaries.Concat(sideBoundaries);

        return atmosphericBoundaries.ToArray();
    }

    private IEnumerable<Boundary> GenerateTopAndBottom(IEnumerable<Boundary> surfaceBoundaries)
    {
        //TODO: Is the bug because the order of the boundaries themselves is off at the north/south pole, so Distinct() doesn't return them in order?
        int[] surfaceBoundaryIndices = surfaceBoundaries.SelectMany(boundary => boundary.VertexIndices).Distinct().ToArray();

        var bottomBoundary = new Boundary() { NeighboursIndex = -1, 
                                              VertexIndices = surfaceBoundaryIndices };

        var topBoundary    = new Boundary() { NeighboursIndex = -1, 
                                              VertexIndices = OffsetIndicesToTopLayer(surfaceBoundaryIndices) };

        return new[] { bottomBoundary, topBoundary };
    }

    private int[] OffsetIndicesToTopLayer(int[] surfaceVertexIndices)
    {
        return surfaceVertexIndices.Select(i => i + 2*_numberOfIndicesPerLayer).ToArray();
    }

    private IEnumerable<Boundary> GenerateSides(IEnumerable<Boundary> surfaceBoundaries)
    {
        var atmosphericBoundaries = surfaceBoundaries.Select<Boundary, Boundary>(GenerateSide);

        return atmosphericBoundaries;
    }

    private Boundary GenerateSide(Boundary surfaceBoundary)
    {
        int neighbourIndex = surfaceBoundary.NeighboursIndex;

        var lowerVertexIndices = surfaceBoundary.VertexIndices;
        var upperVertexIndices = OffsetIndicesToTopLayer(surfaceBoundary.VertexIndices);
        int[] vertexIndices = lowerVertexIndices.Concat(upperVertexIndices).ToArray();

        var atmosphericBoundary = new Boundary { NeighboursIndex = neighbourIndex, VertexIndices = vertexIndices };

        return atmosphericBoundary;
    }
}

