using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

[TestClass]
public class NodeTests
{
    private Node _node;

    [TestInitialize]
    public void Create_Simple_Node_At_North_Pole()
    {
        _node = new Node() {Index = 0, Direction = new Vector3(0, 0, 1)};
    }

}

