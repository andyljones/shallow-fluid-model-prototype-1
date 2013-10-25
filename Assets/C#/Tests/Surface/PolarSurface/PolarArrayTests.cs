using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;
using System.Collections.Generic;

[TestClass]
public class PolarArrayTests
{
    private PolarArray<string> polarArray;
    private Pair<int>[] indices;

    [TestInitialize]
    public void ConstructPolarArray()
    {
        polarArray = new PolarArray<string>(3, 4);

        polarArray[0, 0] = "(90,0)";
        polarArray[1, 0] = "(0,0)";
        polarArray[1, 1] = "(0,90)";
        polarArray[1, 2] = "(0,180)";
        polarArray[1, 3] = "(0,270)";
        polarArray[2, 0] = "(-90,0)";

        indices = new PAIndex[6] { new PAIndex(0,0), 
                                     new PAIndex(1,0), 
                                     new PAIndex(1,1), 
                                     new PAIndex(1,2), 
                                     new PAIndex(1,3), 
                                     new PAIndex(2,0) 
                                    };
    }

    [TestMethod]
    public void IndexingTest()
    {
        // Check poles can be accessed with any longitude argument
        Assert.AreEqual("(90,0)", polarArray[0, 7]);
        Assert.AreEqual("(-90,0)", polarArray[2, 4]);

        // Check other latitudes are what they should be
        Assert.AreEqual("(0,90)", polarArray[1, 1]);
        Assert.AreEqual("(0,270)", polarArray[1, 7]);

        // Check latitudes can be accessed with negative arguments.
        Assert.AreEqual("(0,180)", polarArray[1, -2]);
        Assert.AreEqual("(0,180)", polarArray[1, -6]);
    }

    [TestMethod]
    public void PolAziIndexEnumeratorTest()
    {
        int index = 0;

        foreach (PAIndex paIndex in polarArray.PAIndexEnumerator())
        {
            Assert.AreEqual(indices[index], paIndex);
            index++;
        }
    }

    [TestMethod]
    public void PolAziConversionTest()
    {
        var northPole = polarArray.CoordsOf(new PAIndex(0, 0));
        Assert.AreEqual(0.0F, northPole.pol);
        Assert.AreEqual(0.0F, northPole.azi);
        
        var equator90E = polarArray.CoordsOf(new PAIndex(1, 1));
        Assert.AreEqual(Mathf.PI / 2, equator90E.pol);
        Assert.AreEqual(Mathf.PI / 2, equator90E.azi);

        var degenerate90E = polarArray.CoordsOf(new PAIndex(1, 5));
        Assert.AreEqual(Mathf.PI / 2, degenerate90E.pol);
        Assert.AreEqual(Mathf.PI / 2, degenerate90E.azi);

        var equator180E = polarArray.CoordsOf(new PAIndex(1, 2));
        Assert.AreEqual(Mathf.PI / 2, equator180E.pol);
        Assert.AreEqual(Mathf.PI, equator180E.azi);

        var equator90W = polarArray.CoordsOf(new PAIndex(1, 3));
        Assert.AreEqual(Mathf.PI / 2, equator90W.pol);
        Assert.AreEqual(3 * Mathf.PI / 2, equator90W.azi);

        var southPole = polarArray.CoordsOf(new PAIndex(2, 5));
        Assert.AreEqual(Mathf.PI, southPole.pol);
        Assert.AreEqual(0.0F, southPole.azi);
    }

    [TestMethod]
    public void NearestIndexTest()
    {
        var northPoleIndex = new PAIndex(0, 0);
        var northPole = polarArray.CoordsOf(northPoleIndex);
        Assert.AreEqual(northPoleIndex, polarArray.NearestPAIndexTo(northPole));

        var closeToPole = new PACoord(Mathf.PI / 8, Mathf.PI);
        Assert.AreEqual(northPoleIndex, polarArray.NearestPAIndexTo(closeToPole));

        var closeToEquator90E = new PACoord(5 * Mathf.PI / 8, 3 * Mathf.PI / 8);
        Assert.AreEqual(new PAIndex(1, 1), polarArray.NearestPAIndexTo(closeToEquator90E));
    }

    [TestMethod]
    public void IsPolarTest()
    {
        Assert.IsTrue(polarArray.IsPolarIndex(new PAIndex(0, 3)));
        Assert.IsTrue(polarArray.IsPolarIndex(new PAIndex(2, 5)));

        Assert.IsFalse(polarArray.IsPolarIndex(new PAIndex(1, 4)));
    }

    [TestMethod]
    public void NeighboursOfTest()
    {
        var expectedNorthPoleNeighbours = new HashSet<PAIndex>() {new PAIndex(1,0), new PAIndex(1,1), new PAIndex(1,2), new PAIndex(1,3) };
        var actualNorthPoleNeighbours = polarArray.NeighboursOf(new PAIndex(0, 0));
        Assert.IsTrue(expectedNorthPoleNeighbours.SetEquals(actualNorthPoleNeighbours));

        var expected0N0ENeighbours = new HashSet<PAIndex>() { new PAIndex(0, 0), new PAIndex(1, 1), new PAIndex(2, 0), new PAIndex(1, 3) };
        var actual0N0ENeighbours = polarArray.NeighboursOf(new PAIndex(1, 0));
        Assert.IsTrue(expected0N0ENeighbours.SetEquals(actual0N0ENeighbours));

        var expectedSouthPoleNeighbours = new HashSet<PAIndex>() { new PAIndex(1, 0), new PAIndex(1, 1), new PAIndex(1, 2), new PAIndex(1, 3) };
        var actualSouthPoleNeighbours = polarArray.NeighboursOf(new PAIndex(2, 0));
        Assert.IsTrue(expectedSouthPoleNeighbours.SetEquals(actualSouthPoleNeighbours));
    }
}
