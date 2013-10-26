using UnityEngine;

public interface IGridGenerator<out TNode>
    where TNode : ISimplyGeneratedNode, new()
{
    TNode[] Nodes { get; }
    
    Vector3[] MeshVertices { get; }

    int[] MeshTriangles { get; }

}

