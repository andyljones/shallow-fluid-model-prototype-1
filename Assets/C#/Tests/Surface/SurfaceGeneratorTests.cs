using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SurfaceGeneratorTests
{
    private FakeNode[] _nodes;
    private SurfaceGenerator<FakeNode> _generator;
    
    [TestInitialize]
    public void Create_Grid_And_6000km_Surface()
    {
        _nodes = new FakeNode[] { new FakeNode() };
        _generator = new SurfaceGenerator<FakeNode>(6000f);
    }

    [TestMethod]
    public void Correct_Number_of_Elements()
    {
        var expectedNumberOfElements = 1;
        var surfaceElements = _generator.SurfaceElements(_nodes);
        Assert.AreEqual(expectedNumberOfElements, surfaceElements.Length);
    }

    [TestMethod]
    public void Radius_Is_Set_Correctly()
    {
        var expectedRadius = 6000f;
        var surfaceElements = _generator.SurfaceElements(_nodes);
        Assert.AreEqual(expectedRadius, surfaceElements[0].Radius);
    }


}