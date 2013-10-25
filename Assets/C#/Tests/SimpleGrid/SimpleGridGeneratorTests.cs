using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class SimpleGridGeneratorTests
{
    public IGridGenerator sgg;

    [TestInitialize]
    public void Create_6000k_Simple_Grid_With_Five_Latitudes_And_Eight_Longitudes()
    {
        sgg = new SimpleGridGenerator(6000, 6000 * Mathf.PI / 4);
    }

    [TestMethod]
    public void Right_Number_Of_Nodes()
    {
        Assert.AreEqual(26, sgg.Nodes.Length);
    }

    [TestMethod]
    public void Every_Node_Has_Been_Created()
    {
        CollectionAssert.AllItemsAreNotNull(sgg.Nodes);
    }

    [TestMethod]
    public void North_Pole_Is_Correct()
    {
        INode northPoleNode = new SimpleNode(0) {Direction = new Vector3(0, 0, 1)};
        Assert.IsTrue(TestTools.ApproxEquals(northPoleNode, sgg.Nodes[0], 0.001f));
    }

    [TestMethod]
    public void South_Pole_Is_Correct()
    {
        var southPoleNode = new SimpleNode(25) { Direction = new Vector3(0, 0, -1) };
        Assert.IsTrue(TestTools.ApproxEquals(southPoleNode, sgg.Nodes[25], 0.001f));
    }

    [TestMethod]
    public void Node_45N45E_Is_Correct()
    {
        var x = Mathf.Sin(Mathf.PI / 4) * Mathf.Cos(Mathf.PI / 4);
        var y = Mathf.Sin(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4);
        var z = Mathf.Cos(Mathf.PI / 4);

        var expected45N45E = new SimpleNode(2) { Direction = new Vector3(x, y, z) };
        Assert.IsTrue(TestTools.ApproxEquals(expected45N45E, sgg.Nodes[2], 0.001f));
    }

    [TestMethod]
    public void Number_Of_Mesh_Vertices_Is_Correct()
    {
        int expectedNumberOfMeshVertices = 114;
        Assert.AreEqual(expectedNumberOfMeshVertices, sgg.MeshVertices.Length);
    }

    [TestMethod]
    public void Mesh_Vertices_Are_All_Non_Null()
    {
        CollectionAssert.AllItemsAreNotNull(sgg.MeshVertices);
    }

    [TestMethod]
    public void Mesh_Vertex_North_Pole_Is_Correct()
    {
        var northPoleVertex = new Vector3(0, 0, 6000);
        Assert.IsTrue(TestTools.ApproxEquals(northPoleVertex, sgg.MeshVertices[0], 0.001f));
    }

    [TestMethod]
    public void Mesh_Vertex_South_Pole_Is_Correct()
    {
        var southPoleVertex = new Vector3(0, 0, -6000);
        Assert.IsTrue(TestTools.ApproxEquals(southPoleVertex, sgg.MeshVertices[113], 0.001f));
    }

    [TestMethod]
    public void Mesh_Vertex_67N22E_Is_Correct()
    {
        var x = 6000 * Mathf.Sin(Mathf.PI / 8) * Mathf.Cos(Mathf.PI / 8);
        var y = 6000 * Mathf.Sin(Mathf.PI / 8) * Mathf.Sin(Mathf.PI / 8);
        var z = 6000 * Mathf.Cos(Mathf.PI / 8);

        var expected67N22E = new Vector3(x, y, z);
        Assert.IsTrue(TestTools.ApproxEquals(expected67N22E, sgg.MeshVertices[2], 0.001f));
    }
}

