using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class AtmosphereGeneratorTests
{
    private AtmosphereGenerator<FakeSurfaceElement, FakeAtmosphericElement> _generator;
    private FakeAtmosphericElement[] _atmosphericElements;
    private Vector3[] _atmosphericVertices;

    [TestInitialize]
    public void Create_Atmosphere_With_One_8km_Layer()
    {
        var boundary0 = new Boundary() {NeighboursIndex = 4, VertexIndices = new int[] {20, 30}};
        var boundary1 = new Boundary() {NeighboursIndex = 5, VertexIndices = new int[] {30, 40}};
        var boundary2 = new Boundary() {NeighboursIndex = 6, VertexIndices = new int[] {40, 20}};

        var fakeNode0 = new FakeSurfaceElement()
        {
            Boundaries = new[] {boundary0, boundary1, boundary2},
            Direction = new Vector3(1, 0, 0).normalized,
            Index = 0,
            Radius = 6000f,
            VertexIndex = 10
        };
        var fakeNode1 = new FakeSurfaceElement()
        {
            Boundaries = new[] {boundary0, boundary1, boundary2 },
            Direction = new Vector3(0, 1, 0).normalized,
            Index = 1,
            Radius = 6000f,
            VertexIndex = 11
        };
        var fakeNode2 = new FakeSurfaceElement()
        {
            Boundaries = new[] {boundary0, boundary1, boundary2 },
            Direction = new Vector3(0, 0, 1).normalized,
            Index = 2,
            Radius = 6000f,
            VertexIndex = 12
        };

        var elements = new[] {fakeNode0, fakeNode1, fakeNode2};
        var vertices = new[] { new Vector3(3, 4, 0), new Vector3(4, 0, 3), new Vector3(0, 3, 4)};

        _generator = new AtmosphereGenerator<FakeSurfaceElement, FakeAtmosphericElement>(10f);
        _generator.GenerateAtmosphere(elements, vertices);
        _atmosphericElements = _generator.AtmosphereElements();
        _atmosphericVertices = _generator.AtmosphereVertices();
    }

    [TestMethod]
    public void Generates_Correct_Number_Of_Atmospheric_Elements()
    {
        var expectedNumberOfElements = 3;
        Assert.AreEqual(expectedNumberOfElements, _atmosphericElements.Length);
    }

    [TestMethod]
    public void All_Atmospheric_Elements_Are_Non_Null()
    {
        CollectionAssert.AllItemsAreNotNull(_atmosphericElements);
    }

    [TestMethod]
    public void Atmospheric_Elements_Have_The_Right_Index()
    {
        var expectedIndex = 1;
        Assert.AreEqual(expectedIndex, _atmosphericElements[1].Index);
    }

    [TestMethod]
    public void Atmospheric_Elements_Have_The_Right_Vertex_Index()
    {
        var expectedVertexIndex = 14;
        Assert.AreEqual(expectedVertexIndex, _atmosphericElements[1].VertexIndex);
    }

    [TestMethod]
    public void Atmospheric_Elements_Have_The_Right_Radius()
    {
        var expectedRadius = 6000f;
        Assert.AreEqual(expectedRadius, _atmosphericElements[1].Radius);
    }

    [TestMethod]
    public void Atmospheric_Elements_Have_The_Right_Height()
    {
        var expectedHeight = 10f;
        Assert.AreEqual(expectedHeight, _atmosphericElements[1].Height);
    }

    [TestMethod]
    public void Atmospheric_Elements_Have_The_Right_Direction()
    {
        var expectedDirection = new Vector3(0, 1, 0);
        Assert.IsTrue(TestTools.ApproxEquals(expectedDirection, _atmosphericElements[1].Direction, 0.001f));
    }

    [TestMethod]
    public void Atmospheric_Boundaries_Have_The_Correct_Number_Of_Elements()
    {
        var expectedNumberOfBoundaries = 5;
        Assert.AreEqual(expectedNumberOfBoundaries, _atmosphericElements[1].Boundaries.Length);
    }

    [TestMethod]
    public void Bottom_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary() {NeighboursIndex = -1, VertexIndices = new[] {20, 30, 40}};
        Assert.AreEqual(expectedBoundary, _atmosphericElements[1].Boundaries[0]);
    }

    [TestMethod]
    public void Top_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = -1, VertexIndices = new[] { 26, 36, 46 } };
        Assert.AreEqual(expectedBoundary, _atmosphericElements[1].Boundaries[4]);
    }

    [TestMethod]
    public void First_Side_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 4, VertexIndices = new[] { 20, 30, 26, 36 } };
        Assert.AreEqual(expectedBoundary, _atmosphericElements[1].Boundaries[1]);
    }

    [TestMethod]
    public void Second_Side_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 5, VertexIndices = new[] { 30, 40, 36, 46 } };
        Assert.AreEqual(expectedBoundary, _atmosphericElements[1].Boundaries[2]);
    }

    [TestMethod]
    public void Third_Side_Boundary_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 6, VertexIndices = new[] { 40, 20, 46, 26 } };
        Assert.AreEqual(expectedBoundary, _atmosphericElements[1].Boundaries[3]);
    }

    [TestMethod]
    public void Number_Of_Atmospheric_Vertices_Is_Correct()
    {
        var expectedNumberOfVertices = 9;
        Assert.AreEqual(expectedNumberOfVertices, _atmosphericVertices.Length);
    }

    [TestMethod]
    public void Atmospheric_Vertices_Are_All_Non_Null()
    {
        CollectionAssert.AllItemsAreNotNull(_atmosphericVertices);
    }

    [TestMethod]
    public void Atmospheric_Bottom_Layer_Has_Right_Magnitude()
    {
        var expectedMagnitude = 5f;
        Assert.AreEqual(expectedMagnitude, _atmosphericVertices[0].magnitude);
    }

    [TestMethod]
    public void Atmospheric_Middle_Layer_Has_Right_Magnitude()
    {
        var expectedMagnitude = 10f;
        Assert.AreEqual(expectedMagnitude, _atmosphericVertices[4].magnitude);
    }

    [TestMethod]
    public void Atmospheric_Top_Layer_Has_Right_Magnitude()
    {
        var expectedMagnitude = 15f;
        Assert.AreEqual(expectedMagnitude, _atmosphericVertices[7].magnitude);
    }
}