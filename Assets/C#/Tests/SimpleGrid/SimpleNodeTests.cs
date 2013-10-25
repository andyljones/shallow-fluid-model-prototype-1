using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class SimpleNodeTests
{
    private SimpleNode node;

    [TestInitialize]
    public void Create_Simple_Node_At_North_Pole()
    {
        node = new SimpleNode(4) {Position = new Vector3(0, 0, 1)};
    }

    [TestMethod]
    public void Is_Not_Equal_To_Null()
    {
        Assert.IsFalse(node.Equals(null));
    }

    [TestMethod]
    public void Different_Types_Arent_Equal()
    {
        Assert.IsFalse(node.Equals("this string is not a SimpleNode"));
    }

    [TestMethod]
    public void Same_Nodes_Have_Same_Hashcodes()
    {
        var sameNode = new SimpleNode(4) {Position = new Vector3(0, 0, 1)};
        Assert.IsTrue(node.GetHashCode() == sameNode.GetHashCode());
    }

    [TestMethod]
    public void Equality_Operator_Works_Correctly()
    {
        var sameNode = new SimpleNode(4) { Position = new Vector3(0, 0, 1) };
        Assert.IsTrue(node == sameNode);

        var differentNode = new SimpleNode(5) {Position = new Vector3(0, 0, 1)};
        Assert.IsFalse(node == differentNode);
    }

    [TestMethod]
    public void Inequality_Operator_Works_Correctly()
    {
        var sameNode = new SimpleNode(4) { Position = new Vector3(0, 0, 1) };
        Assert.IsFalse(node != sameNode);

        var differentNode = new SimpleNode(5) { Position = new Vector3(0, 0, 1) };
        Assert.IsTrue(node != differentNode);
    }

}

