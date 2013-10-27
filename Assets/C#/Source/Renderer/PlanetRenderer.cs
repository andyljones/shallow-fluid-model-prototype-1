/*****************NOT UNDER TEST**************/

public class PlanetRenderer<TSurfaceElement, TAtmosphereElement> : IPlanetRenderer<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IRenderableSurfaceElement
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    public void InitializeScene(Surface<TSurfaceElement> surface, Atmosphere<TAtmosphereElement> atmosphere)
    {
        InitializeSurface(surface);
        //InitializeAtmosphere(atmosphere);
    }

    public void InitializeSurface(Surface<TSurfaceElement> surface)
    {
        var helper = new MeshHelper(surface.Vectors);
    }
}