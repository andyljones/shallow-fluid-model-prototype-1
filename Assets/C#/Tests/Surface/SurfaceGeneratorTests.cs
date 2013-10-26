using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class SurfaceGeneratorTests
{
    private FakeNode[] _nodes;
    private Vector3[] _directions;
    private SurfaceGenerator<FakeNode> _generator;
    
    [TestInitialize]
    public void Create_Grid_And_6000km_Surface()
    {
        _nodes = new FakeNode[] { new FakeNode() };
        _directions = new Vector3[] { new Vector3(1, 1, 1)};
        _generator = new SurfaceGenerator<FakeNode>(6000f);
    }

    [TestMethod]
    public void Correct_Number_of_Elements()
    {
        var expectedNumberOfElements = 1;
        var surfaceElements = _generator.SurfaceElements(_nodes);
        Assert.AreEqual(expectedNumberOfElements, surfaceElements.Length);
    }

    [TestMethod]
    public void Elements_Radius_Is_Set_Correctly()
    {
        var expectedRadius = 6000f;
        var surfaceElements = _generator.SurfaceElements(_nodes);
        Assert.AreEqual(expectedRadius, surfaceElements[0].Radius);
    }

    [TestMethod]
    public void Vertex_Radius_Is_Set_Correctly()
    {
        var expectedVector = new Vector3(3464.1016f, 3464.1016f, 3464.1016f);
        var vertices = _generator.BoundaryVertices(_directions);
        Assert.IsTrue(TestTools.ApproxEquals(expectedVector, vertices[0], 0.001f));
    }


}