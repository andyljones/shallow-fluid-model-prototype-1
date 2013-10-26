public interface ISurfaceGenerator<in TGridElement, out TSurfaceElement>
    where TGridElement: class, IUsableGridElement
    where TSurfaceElement: IGenerableSurfaceElement
{
    TSurfaceElement[] SurfaceElements(TGridElement[] nodes);
}