using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class BoundaryGeneratorTests
{
    private BoundaryGenerator generator;

    [TestInitialize]
    public void Create_Boundary_Generator_For_5_Latitude_8_Longitude_Globe()
    {
        var nodeHelper = new PolarAzimuthalHelper(5, 8);
        var vertexHelper = new PolarAzimuthalHelper(9, 16);

        generator = new BoundaryGenerator(nodeHelper, vertexHelper);
    }

    [TestMethod]
    public void Generates_Correct_Number_of_Boundaries_At_North_Pole()
    {
        int expectedNumberOfBoundaries = 8;
        Assert.AreEqual(expectedNumberOfBoundaries, generator.BoundariesForNode(0, 0).Length);
    }

    [TestMethod]
    public void Boundary_Array_Of_North_Pole_Is_Full()
    {
        CollectionAssert.AllItemsAreNotNull(generator.BoundariesForNode(0, 0));
    }

    [TestMethod]
    public void Boundary_At_North_Pole_Between_22E_and_67E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 2, VertexIndices =  new int[] {2, 3, 4}};
        var actualBoundary = generator.BoundariesForNode(0, 0)[1];
        Assert.AreEqual(expectedBoundary, actualBoundary);    
    }

    [TestMethod]
    public void Generates_Correct_Number_of_Boundaries_At_South_Pole()
    {
        int expectedNumberOfBoundaries = 8;
        Assert.AreEqual(expectedNumberOfBoundaries, generator.BoundariesForNode(25, 113).Length);
    }

    [TestMethod]
    public void Boundary_Array_Of_South_Pole_Is_Full()
    {
        CollectionAssert.AllItemsAreNotNull(generator.BoundariesForNode(25, 113));
    }

    [TestMethod]
    public void Boundary_At_South_Pole_Between_22W_and_67W_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 24, VertexIndices = new int[] { 112, 111, 110 } };
        var actualBoundary = generator.BoundariesForNode(25, 113)[7];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void Correct_Number_Of_Boundaries_About_45N45E()
    {
        var expectedNumberOfBoundaries = 4;
        var actualNumberOfBoundaries = generator.BoundariesForNode(2, 11).Length;
        Assert.AreEqual(expectedNumberOfBoundaries, actualNumberOfBoundaries);
    }

    [TestMethod]
    public void Northern_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() {NeighboursIndex = 0, VertexIndices = new int[] {4, 3, 2}};
        var actualBoundary = generator.BoundariesForNode(2, 19)[0];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void Western_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 1, VertexIndices = new int[] { 2, 18, 34 } };
        var actualBoundary = generator.BoundariesForNode(2, 19)[1];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void Southern_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 10, VertexIndices = new int[] { 34, 35, 36 } };
        var actualBoundary = generator.BoundariesForNode(2, 19)[2];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }

    [TestMethod]
    public void Eastern_Boundary_Of_45N45E_Is_Correct()
    {
        var expectedBoundary = new Boundary() { NeighboursIndex = 3, VertexIndices = new int[] { 36, 20, 4 } };
        var actualBoundary = generator.BoundariesForNode(2, 19)[3];
        Assert.AreEqual(expectedBoundary, actualBoundary);
    }
}