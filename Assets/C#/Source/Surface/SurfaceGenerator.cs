public class SurfaceGenerator<TGridElement, TSurfaceElement> : ISurfaceGenerator<TGridElement, TSurfaceElement>
    where TGridElement : IUsableGridElement
    where TSurfaceElement : IGenerableSurfaceElement, new()
{
    private readonly float _radius;

    public SurfaceGenerator(float radius)
    {
        _radius = radius;
    }

    public Surface<TSurfaceElement> Surface(Grid<TGridElement> grid)
    {
        var gridElements = grid.Elements;
        var surfaceElements = new TSurfaceElement[gridElements.Length];

        foreach (var gridElement in gridElements)
        {
            var surfaceElement = new TSurfaceElement
            {
                Boundaries = gridElement.Boundaries,
                Direction = gridElement.Direction,
                Index = gridElement.Index,
                VertexIndex = gridElement.VertexIndex,
                Radius = _radius
            };

            surfaceElements[gridElement.Index] = surfaceElement;
        }

        var surfaceVectors = grid.Vectors;

        return new Surface<TSurfaceElement>(surfaceElements, surfaceVectors);
    }
}
