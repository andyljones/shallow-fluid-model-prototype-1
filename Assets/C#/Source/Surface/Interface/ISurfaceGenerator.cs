public interface ISurfaceGenerator<TGridElement, TSurfaceElement>
    where TGridElement: IUsableGridElement
    where TSurfaceElement: IGenerableSurfaceElement, new()
{
    Surface<TSurfaceElement> Surface(Grid<TGridElement> grid);
}