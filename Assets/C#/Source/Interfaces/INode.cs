using UnityEngine;

public interface INode
{
    int Index { get; }

    Vector3 Position { get; set; }
}