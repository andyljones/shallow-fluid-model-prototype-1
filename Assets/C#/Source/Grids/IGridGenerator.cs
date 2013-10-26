using UnityEngine;

public interface IGridGenerator<out TNode>
    where TNode : IGenerableNode, new()
{
    TNode[] Nodes { get; }
}