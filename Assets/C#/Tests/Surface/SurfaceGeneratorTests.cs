using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

//TODO: Add tests to make sure that the properties of the SurfaceElement are the same as those of the GridElement
//TODO: Clean these test cases up.
[TestClass]
public class SurfaceGeneratorTests
{
    private SurfaceGenerator<FakeGridElement, FakeSurfaceElement> _generator;
    private Surface<FakeSurfaceElement> _surface;

    [TestInitialize]
    public void Create_Grid_And_6000km_Surface()
    {
        var gridElement = new FakeGridElement
        {
            Boundaries = new[]
            {
                new Boundary {NeighboursIndex = 4, VertexIndices = new[] {20, 30}}
            },
            Direction = new Vector3(1, 0, 0).normalized,
            Index = 0,
            VertexIndex = 10
        };

        var gridElements = new[] { gridElement };
        var gridVectors = new[] { new Vector3(1, 1, 1) };
        var fakeGrid = new Grid<FakeGridElement>(gridElements, gridVectors);

        _generator = new SurfaceGenerator<FakeGridElement, FakeSurfaceElement>(6000f);
        _surface = _generator.Surface(fakeGrid);
    }

    [TestMethod]
    public void Correct_Number_of_Elements()
    {
        var expectedNumberOfElements = 1;
        Assert.AreEqual(expectedNumberOfElements, _surface.Elements.Length);
    }

    [TestMethod]
    public void Elements_Radius_Is_Set_Correctly()
    {
        var expectedRadius = 6000f;
        Assert.AreEqual(expectedRadius, _surface.Elements[0].Radius);
    }
}