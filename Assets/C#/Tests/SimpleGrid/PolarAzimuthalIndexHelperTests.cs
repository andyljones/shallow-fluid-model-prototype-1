using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PolarAzimuthalIndexHelperTests
{
    private PolarAzimuthalIndexHelper _indexHelper;
    private PolarAzimuthalGridGenerator _gridGenerator;

    [TestInitialize]
    public void Create_Polar_Azimuthal_Helper_With_Five_Latitudes_And_Eight_Longitudes()
    {
        _gridGenerator = new PolarAzimuthalGridGenerator(5, 8);
        _indexHelper = _gridGenerator.IndexHelper;
    }

    [TestMethod]
    public void Northern_Offset_Of_Highest_Latitiude_Returns_North_Pole()
    {
        int indexOf45N45E = 2;
        Assert.AreEqual(0, _indexHelper.Offset(indexOf45N45E, -1, 0));
        Assert.AreEqual(0, _indexHelper.Offset(indexOf45N45E, -1, 17));
    }

    [TestMethod]
    public void Southern_Offset_Of_Lowest_Latitude_Returns_South_Pole()
    {
        int indexOf45S45W = 23;
        Assert.AreEqual(25, _indexHelper.Offset(indexOf45S45W, 1, 0));
        Assert.AreEqual(25, _indexHelper.Offset(indexOf45S45W, 1, -14));
    }

    [TestMethod]
    public void North_Eastern_Offset_Of_Midlatitudes_Is_Correct()
    {
        int indexOf0N45E = 10;
        int indexOf45N90E = 3;
        Assert.AreEqual(indexOf45N90E, _indexHelper.Offset(indexOf0N45E, -1, 1));
    }

    [TestMethod]
    public void South_Western_Offset_Across_Prime_Meridian_Is_Correct()
    {
        int indexOf0N0E = 9;
        int indexOf45S45W = 24;
        Assert.AreEqual(indexOf45S45W, _indexHelper.Offset(indexOf0N0E, 1, -1));
    }

    [TestMethod]
    public void Polar_Index_Of_Works_Correctly_At_North_Pole()
    {
        int expectedPolarIndex = 0;
        Assert.AreEqual(expectedPolarIndex, _indexHelper.PolarIndexOf(0));
    }

    [TestMethod]
    public void Polar_Index_Of_Works_Correctly_At_South_Pole()
    {
        int expectedPolarIndex = 4;
        Assert.AreEqual(expectedPolarIndex, _indexHelper.PolarIndexOf(25));
    }

    [TestMethod]
    public void Polar_Index_Of_Works_Correctly_At_45N45E()
    {
        int expectedPolarIndex = 1;
        Assert.AreEqual(expectedPolarIndex, _indexHelper.PolarIndexOf(2));
    }

    [TestMethod]
    public void Azimuthal_Index_Of_Works_Correctly_At_North_Pole()
    {
        int expectedAzimuthalIndex = 0;
        Assert.AreEqual(expectedAzimuthalIndex, _indexHelper.AzimuthalIndexOf(0));
    }

    [TestMethod]
    public void Azimuthal_Index_Of_Works_Correctly_At_South_Pole()
    {
        int expectedAzimuthalIndex = 0;
        Assert.AreEqual(expectedAzimuthalIndex, _indexHelper.AzimuthalIndexOf(25));
    }

    [TestMethod]
    public void Azimuthal_Index_Of_Works_Correctly_At_45N45E()
    {
        int expectedAzimuthalIndex = 1;
        Assert.AreEqual(expectedAzimuthalIndex, _indexHelper.AzimuthalIndexOf(2));
    }

    [TestMethod]
    public void Azimuthal_Index_Of_Works_Correctly_At_0N90E()
    {
        int expectedAzimuthalIndex = 2;
        Assert.AreEqual(expectedAzimuthalIndex, _indexHelper.AzimuthalIndexOf(11));
    }

    [TestMethod]
    public void Index_Of_Works_Correctly_At_North_Pole()
    {
        int expectedIndex = 0;
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(0, -12));
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(0, 0));
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(0, 7));
    }

    [TestMethod]
    public void Index_Of_Works_Correctly_At_South_Pole()
    {
        int expectedIndex = 25;
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(4, -12));
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(4, 0));
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(4, 7));
    }

    [TestMethod]
    public void Index_Of_Works_Correctly_At_45N45E()
    {
        int expectedIndex = 2;
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(1, -7));
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(1, 1));
        Assert.AreEqual(expectedIndex, _indexHelper.IndexOf(1, 9));
    }
    
}