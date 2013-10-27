using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class GridGeneratorTests
{
    private IGridGenerator<FakeGridElement> _generator;
    private FakeGridElement[] _elements;
    private Vector3[] _vectors;

    [TestInitialize]
    public void Create_Simple_Grid_With_Five_Latitudes_And_Eight_Longitudes()
    {
        _generator = new GridGenerator<FakeGridElement>(5, 8);
        _elements = _generator.Grid().Elements;
    }

    [TestMethod]
    public void Right_Number_Of_Nodes()
    {
        Assert.AreEqual(26, _elements.Length);
    }

    [TestMethod]
    public void Every_Node_Has_Been_Created()
    {
        CollectionAssert.AllItemsAreNotNull(_elements);
    }

    [TestMethod]
    public void North_Pole_Is_Correct()
    {
        var expectedIndex = 0;
        var expectedDirection = new Vector3(0, 0, 1);
        var northPole = _elements[0];
        Assert.AreEqual(expectedIndex, northPole.Index);
        Assert.IsTrue(TestTools.ApproxEquals(expectedDirection, northPole.Direction, 0.001f));
    }

    [TestMethod]
    public void South_Pole_Is_Correct()
    {
        var expectedIndex = 25;
        var expectedDirection = new Vector3(0, 0, -1);
        var southPole = _elements[25];
        Assert.AreEqual(expectedIndex, southPole.Index);
        Assert.IsTrue(TestTools.ApproxEquals(expectedDirection, southPole.Direction, 0.001f));
    }

    [TestMethod]
    public void Node_45N45E_Is_Correct()
    {
        var x = Mathf.Sin(Mathf.PI / 4) * Mathf.Cos(Mathf.PI / 4);
        var y = Mathf.Sin(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4);
        var z = Mathf.Cos(Mathf.PI / 4);

        var expectedIndex = 2;
        var expectedDirection = new Vector3(x, y, z);
        var node45N45E = _elements[2];
        Assert.AreEqual(expectedIndex, node45N45E.Index);
        Assert.IsTrue(TestTools.ApproxEquals(expectedDirection, node45N45E.Direction, 0.001f));
    }

    [TestMethod]
    public void North_Pole_Node_Has_Correct_Mesh_Vertex_Index()
    {
        var expectedMeshIndex = 0;
        Assert.AreEqual(expectedMeshIndex, _elements[0].VertexIndex);
    }

    [TestMethod]
    public void South_Pole_Node_Has_Correct_Mesh_Vertex_Index()
    {
        var expectedMeshIndex = 113;
        Assert.AreEqual(expectedMeshIndex, _elements[25].VertexIndex);
    }

    [TestMethod]
    public void Node_At_45N45E_Has_Correct_Mesh_Vertex_Index()
    {
        var expectedMeshIndex = 19;
        Assert.AreEqual(expectedMeshIndex, _elements[2].VertexIndex);
    }

    [TestMethod]
    public void All_Boundaries_About_North_Pole_Are_Correct()
    {
        var expectedBoundaries = new[] {new Boundary { NeighboursIndex = 1, VertexIndices =  new[] {16,  1,  2}},
                                        new Boundary { NeighboursIndex = 2, VertexIndices =  new[] { 2,  3,  4}},
                                        new Boundary { NeighboursIndex = 3, VertexIndices =  new[] { 4,  5,  6}},
                                        new Boundary { NeighboursIndex = 4, VertexIndices =  new[] { 6,  7,  8}},
                                        new Boundary { NeighboursIndex = 5, VertexIndices =  new[] { 8,  9, 10}},
                                        new Boundary { NeighboursIndex = 6, VertexIndices =  new[] {10, 11, 12}},
                                        new Boundary { NeighboursIndex = 7, VertexIndices =  new[] {12, 13, 14}},
                                        new Boundary { NeighboursIndex = 8, VertexIndices =  new[] {14, 15, 16}}};

        var actualBoundaries = _elements[0].Boundaries;
        CollectionAssert.AreEqual(expectedBoundaries, actualBoundaries);
    }

    [TestMethod]
    public void All_Boundaries_About_South_Pole_Are_Correct()
    {
        var expectedBoundaries = new[]  {new Boundary() { NeighboursIndex = 17, VertexIndices =  new int[] { 98,  97, 112}},
                                         new Boundary() { NeighboursIndex = 24, VertexIndices =  new int[] {112, 111, 110}},
                                         new Boundary() { NeighboursIndex = 23, VertexIndices =  new int[] {110, 109, 108}},
                                         new Boundary() { NeighboursIndex = 22, VertexIndices =  new int[] {108, 107, 106}},
                                         new Boundary() { NeighboursIndex = 21, VertexIndices =  new int[] {106, 105, 104}},
                                         new Boundary() { NeighboursIndex = 20, VertexIndices =  new int[] {104, 103, 102}},
                                         new Boundary() { NeighboursIndex = 19, VertexIndices =  new int[] {102, 101, 100}},
                                         new Boundary() { NeighboursIndex = 18, VertexIndices =  new int[] {100,  99,  98}}};

        var actualBoundaries = _elements[25].Boundaries;
        CollectionAssert.AreEqual(expectedBoundaries, actualBoundaries);
    }

    [TestMethod]
    public void Northern_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 0, VertexIndices = new int[] { 4, 3, 2 } };
        var actualBoundary = _elements[2].Boundaries[0];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void Western_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 1, VertexIndices = new int[] { 2, 18, 34 } };
        var actualBoundary = _elements[2].Boundaries[1];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void Southern_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 10, VertexIndices = new int[] { 34, 35, 36 } };
        var actualBoundary = _elements[2].Boundaries[2];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void Eastern_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 3, VertexIndices = new int[] { 36, 20, 4 } };
        var actualBoundary = _elements[2].Boundaries[3];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }
}

