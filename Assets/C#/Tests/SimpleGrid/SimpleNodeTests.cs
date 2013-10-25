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
        node = new SimpleNode(4) {Direction = new Vector3(0, 0, 1)};
    }

}

