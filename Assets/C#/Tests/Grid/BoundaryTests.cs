using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class BoundaryTests
{
    private Boundary boundary;

    [TestInitialize]
    public void Create_Boundary()
    {
        boundary = new Boundary() {NeighboursIndex = 7, VertexIndices = new int[] {5, 2, 7, 3}};
    }

    [TestMethod]
    public void Boundaries_With_Same_Contents_Are_Equal()
    {
        var sameBoundary = new Boundary() { NeighboursIndex = 7, VertexIndices = new int[] { 5, 2, 7, 3 } };
        Assert.AreEqual(boundary, sameBoundary);
    }

    [TestMethod]
    public void Boundaries_With_Different_Indices_Are_Not_Equal()
    {
        var differentBoundary = new Boundary() { NeighboursIndex = 4, VertexIndices = new int[] { 5, 2, 7, 3 } };
        Assert.AreNotEqual(boundary, differentBoundary);
    }

    [TestMethod]
    public void Boundaries_With_Different_Vertex_Index_Arrays_Are_Not_Equal()
    {
        var differentBoundary = new Boundary() { NeighboursIndex = 7, VertexIndices = new int[] { 8, 2, 7, 3 } };
        Assert.AreNotEqual(boundary, differentBoundary);
    }

    [TestMethod]
    public void Boundaries_With_The_Same_Index_Have_The_Same_HashCode()
    {
        var sameBoundary = new Boundary() { NeighboursIndex = 7, VertexIndices = new int[] { 5, 2, 7, 3 } };
        Assert.AreEqual(boundary.GetHashCode(), sameBoundary.GetHashCode());
    }

    [TestMethod]
    public void Boundaries_With_A_Different_Index_Have_The_Same_HashCode()
    {
        var sameBoundary = new Boundary() { NeighboursIndex = 4, VertexIndices = new int[] { 5, 2, 7, 3 } };
        Assert.AreNotEqual(boundary.GetHashCode(), sameBoundary.GetHashCode());
    }
}