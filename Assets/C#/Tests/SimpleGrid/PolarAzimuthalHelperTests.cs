using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using System;

[TestClass]
public class PolarAzimuthalHelperTests
{
    PolarAzimuthalHelper helper;

    [TestInitialize]
    public void Create_Polar_Azimuthal_Helper_With_45_Degree_Resolution()
    {
        float desiredAngularResolution = Mathf.PI / 4;
        helper = new PolarAzimuthalHelper(desiredAngularResolution);
    }

    [TestMethod]
    public void Has_Five_Latitudes()
    {
        Assert.AreEqual(5, helper.NumberOfLatitudes);
    }

    [TestMethod]
    public void Has_Eight_Longitudes()
    {
        Assert.AreEqual(8, helper.NumberOfLongitudes);
    }

    [TestMethod]
    public void Generates_North_Pole()
    {
        var northPole = new Vector3(0.0f, 0.0f, 1.0f);
        Assert.AreEqual(northPole, helper.NormalizedGridPoints[0]);
    }

    [TestMethod]
    public void Has_The_Right_Number_Of_Points()
    {
        var expectedNumberOfGridPoints = 26;
        Assert.AreEqual(expectedNumberOfGridPoints, helper.NormalizedGridPoints.Length);
    }

    [TestMethod]
    public void Generates_South_Pole()
    {
        var southPole = new Vector3(0.0f, 0.0f, -1.0f);
        Assert.AreEqual(southPole, helper.NormalizedGridPoints[25]);
    }

    [TestMethod]
    public void Generates_45N45E_Correctly()
    {
        var x = Mathf.Sin(Mathf.PI / 4) * Mathf.Cos(Mathf.PI / 4);
        var y = Mathf.Sin(Mathf.PI / 4) * Mathf.Sin(Mathf.PI / 4);
        var z = Mathf.Cos(Mathf.PI / 4);

        var expected45N45E = new Vector3(x, y, z);
        Assert.IsTrue(expected45N45E == helper.NormalizedGridPoints[2]);
    }

    [TestMethod]
    public void Fills_Points_Array()
    {
        CollectionAssert.AllItemsAreNotNull(helper.NormalizedGridPoints);
    }
}