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
        var northPoleNode = new SimpleNode(0) {Position = new Vector3(0, 0, 6000)};
        Assert.AreEqual(northPoleNode, sgg.Nodes[0]);
    }

    [TestMethod]
    public void South_Pole_Is_Correct()
    {
        var southPoleNode = new SimpleNode(25) { Position = new Vector3(0, 0, -6000) };
        Assert.AreEqual(southPoleNode, sgg.Nodes[25]);
    }

    [TestMethod]
    // Need to override SimpleNode's equals to get this to work.
    public void Node_45N45E_Is_Correct()
    {
        var x = 6000 * Mathf.Sin(Mathf.PI / 4) * Mathf.Cos(Mathf.PI / 4);
        var y = 6000 * Mathf.Sin(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4);
        var z = 6000 * Mathf.Cos(Mathf.PI / 4);

        var expected45N45E = new SimpleNode(2) { Position = new Vector3(x, y, z) };
        Assert.AreEqual(expected45N45E, sgg.Nodes[2]);
    }

    [TestMethod]
    public void Number_Of_Mesh_Vertices_Is_Correct()
    {
        int expectedNumberOfMeshVertices = 98;
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
        Assert.AreEqual(northPoleVertex, sgg.MeshVertices[0]);
    }

    [TestMethod]
    public void Mesh_Vertex_South_Pole_Is_Correct()
    {
        var southPoleVertex = new Vector3(0, 0, -6000);
        Assert.AreEqual(southPoleVertex, sgg.MeshVertices[97]);
    }

    [TestMethod]
    public void Mesh_Vertex_67N22E_Is_Correct()
    {
        var x = 6000 * Mathf.Sin(Mathf.PI / 8) * Mathf.Cos(Mathf.PI / 8);
        var y = 6000 * Mathf.Sin(Mathf.PI / 8) * Mathf.Sin(Mathf.PI / 8);
        var z = 6000 * Mathf.Cos(Mathf.PI / 8);

        var expected67N22E = new SimpleNode(2) { Position = new Vector3(x, y, z) };
        Assert.AreEqual(expected67N22E, sgg.Nodes[2]);
    }
}

