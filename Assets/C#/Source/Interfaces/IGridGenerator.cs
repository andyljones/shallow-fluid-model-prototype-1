using UnityEngine;

public interface IGridGenerator
{
    INode[] Nodes { get; }

    Vector3[] MeshVertices { get; }

    int[] MeshTriangles { get; }

}

