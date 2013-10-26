using UnityEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PolarAzimuthalGridGeneratorTests
{
    private PolarAzimuthalGridGenerator _gridGenerator;

    [TestInitialize]
    public void Create_Polar_Azimuthal_Generator_With_Five_Latitudes_And_Eight_Longitudes()
    {
        _gridGenerator = new PolarAzimuthalGridGenerator(5, 8);
    }

    [TestMethod]
    public void Generates_North_Pole()
    {
        var northPole = new Vector3(0.0f, 0.0f, 1.0f);
        Assert.IsTrue(TestTools.ApproxEquals(northPole, _gridGenerator.Directions[0], 0.01f));
    }

    [TestMethod]
    public void Has_The_Right_Number_Of_Points()
    {
        var expectedNumberOfGridPoints = 26;
        Assert.AreEqual(expectedNumberOfGridPoints, _gridGenerator.Directions.Length);
    }

    [TestMethod]
    public void Generates_South_Pole()
    {
        var southPole = new Vector3(0.0f, 0.0f, -1.0f);
        Assert.IsTrue(TestTools.ApproxEquals(southPole, _gridGenerator.Directions[25], 0.01f));
    }

    [TestMethod]
    public void Generates_45N45E_Correctly()
    {
        var x = Mathf.Sin(Mathf.PI / 4) * Mathf.Cos(Mathf.PI / 4);
        var y = Mathf.Sin(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4);
        var z = Mathf.Cos(Mathf.PI / 4);

        var expected45N45E = new Vector3(x, y, z);
        Assert.IsTrue(TestTools.ApproxEquals(expected45N45E, _gridGenerator.Directions[2], 0.001f));
    }

    [TestMethod]
    public void Fills_Points_Array()
    {
        CollectionAssert.AllItemsAreNotNull(_gridGenerator.Directions);
    }
}