using System.CodeDom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AtmosphericBoundaryGeneratorTests
{
    private AtmosphericBoundaryGenerator _generator;
    private Boundary[] northPolarSurfaceBoundaries;
    private Boundary[] southPolarSurfaceBoundaries;

    [TestInitialize]
    public void Create_Generator_With_Indices_Vertices_Per_Layer()
    {
        //var indicesPerLayer = 114;
        _generator = new AtmosphericBoundaryGenerator(114);

        northPolarSurfaceBoundaries = new[]
        {
            new Boundary {NeighboursIndex = 1, VertexIndices = new[] {16, 1, 2}},
            new Boundary {NeighboursIndex = 2, VertexIndices = new[] {2, 3, 4}},
            new Boundary {NeighboursIndex = 3, VertexIndices = new[] {4, 5, 6}},
            new Boundary {NeighboursIndex = 4, VertexIndices = new[] {6, 7, 8}},
            new Boundary {NeighboursIndex = 5, VertexIndices = new[] {8, 9, 10}},
            new Boundary {NeighboursIndex = 6, VertexIndices = new[] {10, 11, 12}},
            new Boundary {NeighboursIndex = 7, VertexIndices = new[] {12, 13, 14}},
            new Boundary {NeighboursIndex = 8, VertexIndices = new[] {14, 15, 16}}
        };

        southPolarSurfaceBoundaries = new[]
        {
            new Boundary() {NeighboursIndex = 17, VertexIndices = new int[] {98, 97, 112}},
            new Boundary() {NeighboursIndex = 24, VertexIndices = new int[] {112, 111, 110}},
            new Boundary() {NeighboursIndex = 23, VertexIndices = new int[] {110, 109, 108}},
            new Boundary() {NeighboursIndex = 22, VertexIndices = new int[] {108, 107, 106}},
            new Boundary() {NeighboursIndex = 21, VertexIndices = new int[] {106, 105, 104}},
            new Boundary() {NeighboursIndex = 20, VertexIndices = new int[] {104, 103, 102}},
            new Boundary() {NeighboursIndex = 19, VertexIndices = new int[] {102, 101, 100}},
            new Boundary() {NeighboursIndex = 18, VertexIndices = new int[] {100, 99, 98}}
        };
    }

    [TestMethod]
    public void North_Polar_Bottom_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = -1,
            VertexIndices = new[] { 16, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }
        };

        var actualBoundary = _generator.BoundariesOf(northPolarSurfaceBoundaries)[0];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void North_Polar_Top_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = -1,
            VertexIndices = new[] { 244, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243}
        };

        var actualBoundary = _generator.BoundariesOf(northPolarSurfaceBoundaries)[1];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void North_Polar_0E_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = 1,
            VertexIndices = new[] {16, 1, 2, 244, 229, 230}
        };

        var actualBoundary = _generator.BoundariesOf(northPolarSurfaceBoundaries)[2];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void North_Polar_45E_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = 2,
            VertexIndices = new[] { 2, 3, 4, 230, 231, 232 }
        };

        var actualBoundary = _generator.BoundariesOf(northPolarSurfaceBoundaries)[3];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void North_Polar_90E_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = 3,
            VertexIndices = new[] { 4, 5, 6, 232, 233, 234 }
        };

        var actualBoundary = _generator.BoundariesOf(northPolarSurfaceBoundaries)[4];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void South_Polar_Bottom_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = -1,
            VertexIndices = new[] { 98, 97, 112, 111, 110, 109, 108, 107, 106, 105, 104, 103, 102, 101, 100, 99 }
        };

        var actualBoundary = _generator.BoundariesOf(southPolarSurfaceBoundaries)[0];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void South_Polar_Top_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = -1,
            VertexIndices = new[] { 326, 325, 340, 339, 338, 337, 336, 335, 334, 333, 332, 331, 330, 329, 328, 327 }
        };

        var actualBoundary = _generator.BoundariesOf(southPolarSurfaceBoundaries)[1];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void South_Polar_0W_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = 17,
            VertexIndices = new[] { 98, 97, 112, 326, 325, 340 }
        };

        var actualBoundary = _generator.BoundariesOf(southPolarSurfaceBoundaries)[2];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void South_Polar_45W_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = 24,
            VertexIndices = new[] { 112, 111, 110, 340, 339, 338 }
        };

        var actualBoundary = _generator.BoundariesOf(southPolarSurfaceBoundaries)[3];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void South_Polar_90W_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary
        {
            NeighboursIndex = 23,
            VertexIndices = new[] { 110, 109, 108, 338, 337, 336 }
        };

        var actualBoundary = _generator.BoundariesOf(southPolarSurfaceBoundaries)[4];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }
}