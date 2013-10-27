using System;
using System.Linq;

public struct Boundary
{
    public int NeighboursIndex;
    public int[] VertexIndices;

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
            return Equals((Boundary) obj); //TODO: This seems pointless. Can't "as" to a value type, so if it's not already a boundary then it can't be equal?
        }

    }

    #region Equals, GetHashCode & ToString overrides.
    public bool Equals(Boundary other)
    {
        if (GetHashCode() != other.GetHashCode())
        {
            return false;
        }
        else
        {
            return NeighboursIndex.Equals(other.NeighboursIndex) &&
                   VertexIndices.SequenceEqual(other.VertexIndices);
        }
    }

    public override int GetHashCode()
    {
        return NeighboursIndex.GetHashCode();
    }

    public override string ToString()
    {
        return String.Format("Neighbour Index: {0}\n Boundary Vertex Indicies: {1}", NeighboursIndex, PrintIntArray(VertexIndices));
    }

    private string PrintIntArray(int[] array)
    {
        return String.Join(", ", array.Select(i => i.ToString()).ToArray());
    }
    #endregion


}
