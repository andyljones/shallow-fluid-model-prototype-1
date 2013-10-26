using UnityEngine;

public interface IGridGenerator<out TNode>
    where TNode : ISimplyGeneratedNode, new()
{
    TNode[] Nodes { get; }
}