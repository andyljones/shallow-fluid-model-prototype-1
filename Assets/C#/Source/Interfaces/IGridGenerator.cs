using UnityEngine;

public interface IGridGenerator
{
    ISimpleGeneratedNode[] Nodes { get; }
    
    Vector3[] MeshVertices { get; }

    int[] MeshTriangles { get; }

}

