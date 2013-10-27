using System.Collections.Generic;
using System.Linq;

public class AtmosphericBoundaryGenerator
{
    private readonly int _numberOfIndiciesPerLayer;

    public AtmosphericBoundaryGenerator(int numberOfIndiciesPerLayer)
    {
        _numberOfIndiciesPerLayer = numberOfIndiciesPerLayer;
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
        int[] surfaceBoundaryIndicies = surfaceBoundaries.SelectMany(boundary => boundary.VertexIndices).Distinct().ToArray();

        var bottomBoundary = new Boundary() { NeighboursIndex = -1, 
                                              VertexIndices = surfaceBoundaryIndicies };

        var topBoundary    = new Boundary() { NeighboursIndex = -1, 
                                              VertexIndices = OffsetIndiciesToTopLayer(surfaceBoundaryIndicies) };

        return new[] { bottomBoundary, topBoundary };
    }

    private int[] OffsetIndiciesToTopLayer(int[] surfaceVertexIndices)
    {
        return surfaceVertexIndices.Select(i => i + 2*_numberOfIndiciesPerLayer).ToArray();
    }

    private IEnumerable<Boundary> GenerateSides(IEnumerable<Boundary> surfaceBoundaries)
    {
        var atmosphericBoundaries = surfaceBoundaries.Select(GenerateSide);

        return atmosphericBoundaries;
    }

    private Boundary GenerateSide(Boundary surfaceBoundary)
    {
        int neighbourIndex = surfaceBoundary.NeighboursIndex;

        var lowerVertexIndicies = surfaceBoundary.VertexIndices;
        var upperVertexIndicies = OffsetIndiciesToTopLayer(surfaceBoundary.VertexIndices);
        int[] vertexIndicies = lowerVertexIndicies.Concat(upperVertexIndicies).ToArray();

        var atmosphericBoundary = new Boundary { NeighboursIndex = neighbourIndex, VertexIndices = vertexIndicies };

        return atmosphericBoundary;
    }
}

