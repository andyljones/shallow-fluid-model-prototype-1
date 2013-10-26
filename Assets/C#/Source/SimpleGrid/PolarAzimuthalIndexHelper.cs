using UnityEngine;

public class PolarAzimuthalIndexHelper
{
    public readonly int NumberOfLatitudes;
    public readonly int NumberOfLongitudes;
    public readonly int NumberOfGridPoints;

    public PolarAzimuthalIndexHelper(int numberOfLatitudes, int numberOfLongitudes)
    {
        NumberOfLatitudes = numberOfLatitudes;
        NumberOfLongitudes = numberOfLongitudes;
        NumberOfGridPoints = NumberOfLongitudes * (NumberOfLatitudes - 2) + 2;
    }

    // Offset an index by a given number of gridpoints running north-south and a given number of gridpoints running east-west. 
    public int Offset(int index, int polarOffset, int azimuthalOffset)
    {
        int polarIndex = PolarIndexOf(index);
        int azimuthalIndex = AzimuthalIndexOf(index);

        int offsetIndex;

        if (polarIndex + polarOffset <= 0) // Handle offsets into the north pole
        {
            offsetIndex = 0;
        }
        else if (polarIndex + polarOffset >= NumberOfLatitudes - 1) // Handle offsets into the south pole
        {
            offsetIndex = NumberOfGridPoints - 1;
        }
        else // Handle midlatitude offsets
        {
            int offsetPolarIndex = polarIndex + polarOffset;
            int offsetAzimuthalIndex = MathMod(azimuthalIndex + azimuthalOffset, NumberOfLongitudes);
            offsetIndex = IndexOf(offsetPolarIndex, offsetAzimuthalIndex);
        }

        return offsetIndex;
    }

    public int PolarIndexOf(int index)
    {
        int polarIndex;

        if (NorthPoleIs(index))
        {
            polarIndex = 0;
        }
        else if (SouthPoleIs(index))
        {
            polarIndex = NumberOfLatitudes - 1;
        }
        else
        {
            polarIndex = (index - 1) / NumberOfLongitudes + 1;
        }

        return polarIndex;
    }

    public int AzimuthalIndexOf(int index)
    {
        int azimuthalIndex;

        if (NorthPoleIs(index) || SouthPoleIs(index))
        {
            azimuthalIndex = 0;
        }
        else
        {
            azimuthalIndex = MathMod(index - 1, NumberOfLongitudes);    
        }

        return azimuthalIndex;
    }

    public int IndexOf(int polarIndex, int azimuthalIndex)
    {
        int index;

        if (polarIndex <= 0)
        {
            index = 0;
        }
        else if (polarIndex >= NumberOfLatitudes - 1)
        {
            index = NumberOfGridPoints - 1;
        }
        else
        {
            int indexAtStartOfLatitude = 1 + (polarIndex - 1)*NumberOfLongitudes;
            int normalizedAzimuthalCoordinate = MathMod(azimuthalIndex, NumberOfLongitudes);

            index = indexAtStartOfLatitude + normalizedAzimuthalCoordinate;
        }

        return index;
    }

    public bool NorthPoleIs(int index)
    {
        return index == 0;
    }

    public bool SouthPoleIs(int index)
    {
        return index == NumberOfGridPoints - 1;
    }

    // This is an actual modulo operator, as opposed to the remainder operator % represents.
    private static int MathMod(int n, int m)
    {
        return ((n%m) + m)%m;
    }
}

