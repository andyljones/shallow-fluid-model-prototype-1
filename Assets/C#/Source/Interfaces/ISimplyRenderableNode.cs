using UnityEngine;

public interface ISimplyRenderableNode
{
    int MeshIndex { get; }

    Boundary[] Boundaries { get; }
}