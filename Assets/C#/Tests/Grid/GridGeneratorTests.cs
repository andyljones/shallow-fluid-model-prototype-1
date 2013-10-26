using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

//TODO: Add tests for boundary vertices that are generated
[TestClass]
public class GridGeneratorTests
{
    private IGridGenerator<Node> _generator;

    [TestInitialize]
    public void Create_Simple_Grid_With_Five_Latitudes_And_Eight_Longitudes()
    {
        _generator = new GridGenerator<Node>(5, 8);
    }

    [TestMethod]
    public void Right_Number_Of_Nodes()
    {
        Assert.AreEqual(26, _generator.GridElements().Length);
    }

    [TestMethod]
    public void Every_Node_Has_Been_Created()
    {
        CollectionAssert.AllItemsAreNotNull(_generator.GridElements());
    }

    [TestMethod]
    public void North_Pole_Is_Correct()
    {
        var northPoleNode = new Node() {Index = 0, Direction = new Vector3(0, 0, 1)};
        Assert.IsTrue(TestTools.ApproxEquals(northPoleNode, _generator.GridElements()[0], 0.001f));
    }

    [TestMethod]
    public void South_Pole_Is_Correct()
    {
        var southPoleNode = new Node() { Index = 25, Direction = new Vector3(0, 0, -1) };
        Assert.IsTrue(TestTools.ApproxEquals(southPoleNode, _generator.GridElements()[25], 0.001f));
    }

    [TestMethod]
    public void Node_45N45E_Is_Correct()
    {
        var x = Mathf.Sin(Mathf.PI / 4) * Mathf.Cos(Mathf.PI / 4);
        var y = Mathf.Sin(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4);
        var z = Mathf.Cos(Mathf.PI / 4);

        var expected45N45E = new Node() { Index = 2, Direction = new Vector3(x, y, z) };
        Assert.IsTrue(TestTools.ApproxEquals(expected45N45E, _generator.GridElements()[2], 0.001f));
    }

    [TestMethod]
    public void North_Pole_Node_Has_Correct_Mesh_Vertex_Index()
    {
        var expectedMeshIndex = 0;
        Assert.AreEqual(expectedMeshIndex, _generator.GridElements()[0].VertexIndex);
    }

    [TestMethod]
    public void South_Pole_Node_Has_Correct_Mesh_Vertex_Index()
    {
        var expectedMeshIndex = 113;
        Assert.AreEqual(expectedMeshIndex, _generator.GridElements()[25].VertexIndex);
    }

    [TestMethod]
    public void Node_At_45N45E_Has_Correct_Mesh_Vertex_Index()
    {
        var expectedMeshIndex = 19;
        Assert.AreEqual(expectedMeshIndex, _generator.GridElements()[2].VertexIndex);
    }
}

