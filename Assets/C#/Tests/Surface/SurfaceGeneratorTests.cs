using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

//TODO: Add tests to make sure that the properties of the SurfaceElement are the same as those of the GridElement
[TestClass]
public class SurfaceGeneratorTests
{
    private FakeGridElement[] _gridElements;
    private Vector3[] _directions;
    private SurfaceGenerator<FakeGridElement, FakeSurfaceElement> _generator;
    
    [TestInitialize]
    public void Create_Grid_And_6000km_Surface()
    {
        var boundary = new Boundary() { NeighboursIndex = 4, VertexIndices = new int[] { 20, 30 } };
        var fakeGridElement = new FakeGridElement() { Boundaries = new[] {boundary}, 
                                                      Direction = new Vector3(1,0,0).normalized,
                                                      Index = 0,
                                                      VertexIndex = 10};

        _gridElements = new[] { fakeGridElement };
        _directions = new[] { new Vector3(1, 1, 1)};
        _generator = new SurfaceGenerator<FakeGridElement, FakeSurfaceElement>(6000f);
    }

    [TestMethod]
    public void Correct_Number_of_Elements()
    {
        var expectedNumberOfElements = 1;
        var surfaceElements = _generator.SurfaceElements(_gridElements);
        Assert.AreEqual(expectedNumberOfElements, surfaceElements.Length);
    }

    [TestMethod]
    public void Elements_Radius_Is_Set_Correctly()
    {
        var expectedRadius = 6000f;
        var surfaceElements = _generator.SurfaceElements(_gridElements);
        Assert.AreEqual(expectedRadius, surfaceElements[0].Radius);
    }
}