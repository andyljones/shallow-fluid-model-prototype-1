using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public struct PersistantInformation
{
    public readonly Vector3 X;
    public readonly Vector3 Y;
    public readonly Vector3 Z;

    public Vector3[] NeighbourVectors; 

    public PersistantInformation(Vector3 x, Vector3 y, Vector3 z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.NeighbourVectors = new Vector3[0];
    }

    #region Equality and hashcode overrides
    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        else if (this.GetType() != obj.GetType())
        {
            return false;
        }
        else
        {
            return Equals((PersistantInformation) obj);
        }
    }

    public bool Equals(PersistantInformation obj)
    {
        return (X == obj.X) &&
               (Y == obj.Y) &&
               (Z == obj.Z) &&
               NeighbourVectors.SequenceEqual(obj.NeighbourVectors);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
    }
    #endregion
}

