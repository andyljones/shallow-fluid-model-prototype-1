public interface IPlanetRenderer<TSurfaceElement, TAtmosphereElement>
    where TSurfaceElement : IRenderableSurfaceElement
    where TAtmosphereElement : IRenderableAtmosphereElement
{
    void InitializeScene(Surface<TSurfaceElement> surface, Atmosphere<TAtmosphereElement> atmosphere);
}