using System.Linq;
using UnityEngine;

public class AtmosphereGenerator<TSurfaceElement, TAtmosphereElement> : IAtmosphereGenerator<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IUsableSurfaceElement
    where TAtmosphereElement : IGenerableAtmosphericElement, new()
{
    private readonly float _height;

    private int _numberOfSurfaceVectors;
    
    public AtmosphereGenerator(float height)
    {
        _height = height;
    }

    public Atmosphere<TAtmosphereElement> Atmosphere(Surface<TSurfaceElement> surface)
    {
        _numberOfSurfaceVectors = surface.Vectors.Length;

        var atmosphereElements = GenerateAtmosphereElements(surface.Elements);
        var atmosphereVectors = GenerateAtmosphereVectors(surface.Vectors);

        var atmosphere = new Atmosphere<TAtmosphereElement>(atmosphereElements, atmosphereVectors);

        return atmosphere;
    }

    #region Atmosphere element generation
    private TAtmosphereElement[] GenerateAtmosphereElements(TSurfaceElement[] surfaceElements)
    {
        var boundaryGenerator = new AtmosphericBoundaryGenerator(_numberOfSurfaceVectors);
        return surfaceElements.Select(element => GenerateAtmosphereElement(element, boundaryGenerator)).ToArray();
    }

    private TAtmosphereElement GenerateAtmosphereElement(TSurfaceElement surfaceElement, AtmosphericBoundaryGenerator boundaryGenerator)
    {
        var index = surfaceElement.Index;
        var centralVertexIndices = CalculateCentralVertexIndicies(surfaceElement.VertexIndex);
        var radius = surfaceElement.Radius;
        var height = _height;
        var direction = surfaceElement.Direction;
        var boundaries = boundaryGenerator.BoundariesOf(surfaceElement.Boundaries);

        var atmosphereElement = new TAtmosphereElement()
        {
            Index = index,
            CentralVertexIndices = centralVertexIndices,
            Radius = radius,
            Height = height,
            Direction = direction,
            Boundaries = boundaries
        };

        return atmosphereElement;
    }

    private int[] CalculateCentralVertexIndicies(int surfaceElementVertexIndex)
    {
        int bottomLayerCentralVertexIndex = surfaceElementVertexIndex;
        int middleLayerCentralVertexIndex = surfaceElementVertexIndex + _numberOfSurfaceVectors;
        int topLayerCentralVertexIndex = surfaceElementVertexIndex + 2*_numberOfSurfaceVectors;

        return new[] {bottomLayerCentralVertexIndex, middleLayerCentralVertexIndex, topLayerCentralVertexIndex};
    }
    #endregion

    #region Atmosphere vector generation
    private Vector3[] GenerateAtmosphereVectors(Vector3[] surfaceVectors)
    {
        var bottomLayer = surfaceVectors;
        var middleLayer = surfaceVectors;
        var topLayer = surfaceVectors;

        return bottomLayer.Concat(middleLayer).Concat(topLayer).ToArray();
    }
    #endregion

}
