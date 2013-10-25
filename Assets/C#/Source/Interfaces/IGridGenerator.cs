using UnityEngine;

public interface IGridGenerator
{
    INode[] Nodes { get; }

    int[] MeshTriangles { get; }

}

