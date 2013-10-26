using UnityEngine;

public class PolarAzimuthalHelper
{
    //TODO: Refactor this class into a polar-azimuthal grid generation class, and a polar-azimuthal grid helper class.
    public int NumberOfLatitudes { get; private set; }
    public int NumberOfLongitudes { get; private set; }
    public int NumberOfGridPoints { get; private set; }

    public Vector3[] NormalizedGridPoints { get; private set; }

    #region Constructor methods & their dependents
    public PolarAzimuthalHelper(float desiredAngularResolution)
    {
        CalculateNumberOfLatitudes(desiredAngularResolution);
        CalculateNumberOfLongitudes(desiredAngularResolution);
        CalculateNumberOfGridPoints();

        GenerateGridPoints();
    }

    public PolarAzimuthalHelper(int numberOfLatitudes, int numberOfLongitudes)
    {
        NumberOfLatitudes = numberOfLatitudes;
        NumberOfLongitudes = numberOfLongitudes;

        CalculateNumberOfGridPoints();

        GenerateGridPoints();
    }

    private void GenerateGridPoints()
    {
        NormalizedGridPoints = new Vector3[NumberOfGridPoints];

        var northPole = new Vector3(0.0f, 0.0f, 1.0f);
        NormalizedGridPoints[0] = northPole;

        for (int i = 1; i < NumberOfLatitudes - 1; i++)
        {
            for (int j = 0; j < NumberOfLongitudes; j++)
            {
                int index = 1 + (i - 1) * NumberOfLongitudes + j;
                NormalizedGridPoints[index] = GenerateNormalizedPointAt(i, j);
            }
        }

        var southPole = new Vector3(0.0f, 0.0f, -1.0f);
        NormalizedGridPoints[NumberOfGridPoints - 1] = southPole;
    }

    private Vector3 GenerateNormalizedPointAt(int polarIndex, int azimuthIndex)
    {
        float polarAngle = (Mathf.PI * polarIndex) / (NumberOfLatitudes - 1);
        float azimuthAngle = (2 * Mathf.PI * azimuthIndex) / NumberOfLongitudes;

        float x = Mathf.Sin(polarAngle) * Mathf.Cos(azimuthAngle);
        float y = Mathf.Sin(polarAngle) * Mathf.Sin(azimuthAngle);
        float z = Mathf.Cos(polarAngle);

        var point = new Vector3(x, y, z);
        
        return point;
    }

    private void CalculateNumberOfLatitudes(float desiredAngularResolution)
    {
        NumberOfLatitudes = Mathf.CeilToInt(Mathf.PI / desiredAngularResolution) + 1;
    }

    private void CalculateNumberOfLongitudes(float desiredAngluarResolution)
    {
        NumberOfLongitudes = Mathf.CeilToInt(2 * Mathf.PI / desiredAngluarResolution);
    }

    private void CalculateNumberOfGridPoints()
    {
        NumberOfGridPoints = NumberOfLongitudes * (NumberOfLatitudes - 2) + 2;
    }
    #endregion


    #region Index conversion methods
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
            offsetIndex = 1 + (offsetPolarIndex - 1)*NumberOfLongitudes + offsetAzimuthalIndex;
        }

        return offsetIndex;
    }

    public int PolarIndexOf(int index)
    {
        int polarIndex;

        if (index == 0)
        {
            polarIndex = 0;
        }
        else if (index == NumberOfGridPoints - 1)
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

        if (index == 0 || index == NumberOfGridPoints - 1)
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
    #endregion

    // This is an actual modulo operator, as opposed to the remainder operator % represents.
    private int MathMod(int n, int m)
    {
        return ((n%m) + m)%m;
    }
}

