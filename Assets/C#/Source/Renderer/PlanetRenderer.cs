/*****************NOT UNDER TEST**************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

public class PlanetRenderer<TSurfaceElement, TAtmosphereElement> : IPlanetRenderer<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IRenderableSurfaceElement
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    private SurfaceRenderer<TSurfaceElement> _surfaceRenderer;
    private AtmosphereRenderer<TAtmosphereElement> _atmosphereRenderer;

    private GameObject _surfaceObject;
    private GameObject _atmosphereObject;

    public void InitializeScene(Surface<TSurfaceElement> surface, Atmosphere<TAtmosphereElement> atmosphere)
    {
        _surfaceRenderer = new SurfaceRenderer<TSurfaceElement>(surface);
        _atmosphereRenderer = new AtmosphereRenderer<TAtmosphereElement>(atmosphere);

        _surfaceRenderer.SetSurfaceObject();
        _atmosphereRenderer.SetAtmosphereObject();
    }

    public void UpdateScene()
    {
        _surfaceRenderer.UpdateSurfaceObject();
        _atmosphereRenderer.UpdateAtmosphereObject();
    }
}