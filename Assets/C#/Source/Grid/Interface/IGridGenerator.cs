using UnityEngine;

public interface IGridGenerator<TElement>
    where TElement : IGenerableGridElement, new()
{
    Grid<TElement> Grid();
}