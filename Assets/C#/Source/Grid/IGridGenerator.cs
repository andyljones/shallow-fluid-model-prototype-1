using UnityEngine;

public interface IGridGenerator<out TNode>
    where TNode : IGenerableGridElement, new()
{
    TNode[] GridElements();

    Vector3[] BoundaryPoints();
}