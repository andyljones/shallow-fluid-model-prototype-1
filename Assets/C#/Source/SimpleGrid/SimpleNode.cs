using System.Runtime.InteropServices;
using UnityEngine;

public struct SimpleNode : ISimpleGeneratedNode
{
    public int Index { get; private set; }

    public Vector3 Position { get; set; }

    public SimpleNode(int index) : this()
    {
        Index = index;
    }

    #region Equality & hashcode methods
    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (!(obj is SimpleNode))
        {
            return false;
        }

        return Equals((SimpleNode) obj);
    }

    // Two nodes are equal if they have the same index.
    public bool Equals(SimpleNode other)
    {
        if (GetHashCode() == other.GetHashCode())
        {
            return true;
        }

        if (Index == other.Index &&
            Position == other.Position)
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Index;
    }

    public static bool operator ==(SimpleNode lhsNode, SimpleNode rhsNode)
    {
        return lhsNode.Equals(rhsNode);
    }

    public static bool operator !=(SimpleNode lhsNode, SimpleNode rhsNode)
    {
        return !lhsNode.Equals(rhsNode);
    }
    #endregion;
}

