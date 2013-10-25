using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class SimpleGridGeneratorTests
{
    IGridGenerator sgg;

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
    public void Generates_45N45E_Correctly()
    {
        var x = 6000 * Mathf.Sin(Mathf.PI / 4) * Mathf.Cos(Mathf.PI / 4);
        var y = 6000 * Mathf.Sin(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4);
        var z = 6000 * Mathf.Cos(Mathf.PI / 4);

        var expected45N45E = new SimpleNode(2) { Position = new Vector3(x, y, z) };
        Assert.AreEqual(expected45N45E, sgg.Nodes[2]);
    }
}

