using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// An array type that supports access via longitude and latitude. 
/// </summary>
/// <typeparam name="T"></typeparam>
class PolarArray<T> : IEnumerable<T>
{
    private readonly int noOfLats;
    private readonly int noOfLongs;
    public readonly int noOfItems;

    /// <summary>
    /// The collection underlying the class. The format is
    /// { north pole, items of northermost latitude, ..., items of southernmost latitude, south pole } 
    /// where the vertices of each latitude are listed from the prime meridian, going east.
    /// </summary>
    private List<T> Items { get; set; }

    /// <summary>
    /// Indexing into the first or last latitude will return the respective polar element, regardless of the longitude index.
    /// 
    /// Indexing into intermediate latitudes will wrap the longitude index when noOfLongs is exceeded.
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
    public T this[PAIndex paIndex]
    {
        get
        {
            int i = IndexOf(paIndex);
            return Items[i];
        }
        set
        {
            int i = IndexOf(paIndex);
            Items[i] = value;
        }
    }

    /// <summary>
    /// Indexing into the first or last latitude will return the respective polar element, regardless of the longitude index.
    /// 
    /// Indexing into intermediate latitudes will wrap the longitude index when noOfLongs is exceeded.
    /// </summary>
    /// <param name="polIndex"></param>
    /// <param name="aziIndex"></param>
    /// <returns></returns>
    public T this[int polIndex, int aziIndex]
    {
        get
        {
            return this[new PAIndex(polIndex, aziIndex)];
        }
        set
        {
            this[new PAIndex(polIndex, aziIndex)] = value;
        }
    }

    /// <param name="noOfLats">Must be at least 3</param>
    /// <param name="noOfLongs"></param>
    public PolarArray(int noOfLats, int noOfLongs)
    {
        this.noOfLats = noOfLats;
        this.noOfLongs = noOfLongs;
        this.noOfItems = (noOfLats - 2) * noOfLongs + 2;

        Items = new List<T>(new T[noOfItems]);
    }

    #region IEnumerable<T> members
    public IEnumerator<T> GetEnumerator()
    {
        return (IEnumerator<T>) Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion

    public HashSet<PAIndex> NeighboursOf(PAIndex paIndex)
    {
        var neighbours = new HashSet<PAIndex>();

        if (paIndex.pol == 0)
        {
            for (int aziIndex = 0; aziIndex < noOfLongs; aziIndex++)
            {
                neighbours.Add(new PAIndex(1, aziIndex));
            }
        }
        else if (paIndex.pol == noOfLats - 1)
        {
            for (int aziIndex = 0; aziIndex < noOfLongs; aziIndex++)
            {
                neighbours.Add(new PAIndex(noOfLats - 2, aziIndex));
            }
        }
        else
        {
            neighbours.Add(new PAIndex(paIndex.pol - 1, Mod(paIndex.azi + 0, noOfLongs)));
            neighbours.Add(new PAIndex(paIndex.pol + 0, Mod(paIndex.azi + 1, noOfLongs)));
            neighbours.Add(new PAIndex(paIndex.pol + 1, Mod(paIndex.azi + 0, noOfLongs)));
            neighbours.Add(new PAIndex(paIndex.pol + 0, Mod(paIndex.azi - 1, noOfLongs)));
        }
        return neighbours;
    }

    public PAIndex NearestPAIndexTo(PACoord coord)
    {        
        float polarSpacing = Mathf.PI / (noOfLats - 1);
        float normalizedPolar = coord.pol / polarSpacing;
        int polarIndex = Mathf.RoundToInt(normalizedPolar);

        int azimuthIndex;

        if (polarIndex == 0 || polarIndex == noOfLats - 1)
        {
            azimuthIndex = 0;
        }
        else
        {
            float azimuthSpacing = 2 * Mathf.PI / noOfLongs;
            float normalizedAzimuth = coord.azi / azimuthSpacing ;
            azimuthIndex = Mathf.RoundToInt(normalizedAzimuth) % noOfLongs;
        }

        return new PAIndex(polarIndex, azimuthIndex);
    }

    /// <summary>
    /// Enumerates over the polar-azimuthal indices in the order they're found in the underlying array.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<PAIndex> PAIndexEnumerator()
    {
        for (int index = 0; index < noOfItems; index++)
        {
            yield return PAIndexOf(index);
        }
    }

    /// <summary>
    /// Converts a index into the underlying array into a polar-azimuthal index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public PAIndex PAIndexOf(int index)
    {
        int pol;
        int azi;

        if (index == 0)
        {
            pol = 0;
            azi = 0;
        }
        else if (index == noOfItems - 1)
        {
            pol = noOfLats - 1;
            azi = 0;
        }
        else
        {
            pol = (index - 1) / noOfLongs + 1;
            azi = Mod(index - 1, noOfLongs);
        }

        return new PAIndex(pol, azi);
    }

    /// <summary>
    /// Converts a polar-azimuthal index to the sequential index into the underlying array it corresponds to.
    /// </summary>
    /// <param name="paIndex"></param>
    /// <returns></returns>
    public int IndexOf(PAIndex paIndex)
    {
        int index;

        if (paIndex.pol == 0)
        {
            index = 0;
        }
        else if (paIndex.pol == noOfLats - 1)
        {
            index = noOfItems - 1;
        }
        else
        {
            int firstLonOfLat = (paIndex.pol - 1) * noOfLongs + 1;
            index = firstLonOfLat + Mod(paIndex.azi, noOfLongs);
        }

        return index;
    }

    /// <summary>
    /// Converts a polar-azimuthal index to the sequential index into the underlying array it corresponds to.
    /// </summary>
    /// <param name="polIndex"></param>
    /// <param name="aziIndex"></param>
    /// <returns></returns>
    public int IndexOf(int polIndex, int aziIndex)
    {
        return IndexOf(new PAIndex(polIndex, aziIndex));
    }

    /// <summary>
    /// Calculates the polar-azimuthal angles corresponding to a polar-azimuthal index. 
    /// </summary>
    /// <param name="paIndex"></param>
    /// <returns></returns>
    public PACoord CoordsOf(PAIndex paIndex)
    {
        float polar;
        float azimuthal;

        if (paIndex.pol == 0) // North pole
        {
            polar = 0.0F;
            azimuthal = 0.0F; // TODO Not sure if this is the best way to deal with the singularity
        }
        else if (paIndex.pol == noOfLats - 1) // South pole
        {
            polar = Mathf.PI;
            azimuthal = 0.0F; // See previous singularity comment.
        }
        else
        {
            float spacing = Mathf.PI / (noOfLats - 1);
            polar = paIndex.pol * spacing;
            azimuthal = AzimuthOf(paIndex.azi);
        }

        return new PACoord(polar, azimuthal);
    }

    public bool IsPolarIndex(PAIndex paIndex)
    {
        return paIndex.pol == 0 || paIndex.pol == (noOfLats - 1);
    }

    // Assumes it's not at the poles.
    private float AzimuthOf(int azimuthIndex)
    {
        azimuthIndex = Mod(azimuthIndex, noOfLongs); // Normalize the index.

        float spacing = 2 * Mathf.PI / noOfLongs;

        return azimuthIndex * spacing;
    }

    // Because C#'s default modulo operator is not in fact a modulo operator.
    private static int Mod(int n, int m)
    {
        return ((n % m) + m) % m;
    }
}

