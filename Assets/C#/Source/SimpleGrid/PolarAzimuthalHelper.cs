using UnityEngine;

public class PolarAzimuthalHelper
{
    public int NumberOfLatitudes { get; private set; }
    public int NumberOfLongitudes { get; private set; }

    public Vector3[] NormalizedGridPoints { get; private set; }

    public PolarAzimuthalHelper(float desiredAngularResolution)
    {
        this.NumberOfLatitudes = CalculateNumberOfLatitudes(desiredAngularResolution);
        this.NumberOfLongitudes = CalculateNumberOfLongitudes(desiredAngularResolution);
        this.NormalizedGridPoints = GenerateGridPoints(NumberOfLatitudes, NumberOfLongitudes);
    }

    private Vector3[] GenerateGridPoints(int numberOfLatitudes, int numberOfLongitudes)
    {
        int numberOfGridPoints = numberOfLongitudes * (numberOfLatitudes - 2) + 2;
        var normalizedGridPoints = new Vector3[numberOfGridPoints];

        var northPole = new Vector3(0.0f, 0.0f, 1.0f);
        normalizedGridPoints[0] = northPole;

        for (int i = 1; i < numberOfLatitudes - 1; i++)
        {
            for (int j = 0; j < numberOfLongitudes; j++)
            {
                int index = 1 + (i - 1) * numberOfLongitudes + j;
                
                float polarAngle = Mathf.PI * ((float)i / (numberOfLatitudes - 1));
                float azimuthAngle = 2 * Mathf.PI * ((float)j / numberOfLongitudes);

                normalizedGridPoints[index] = GenerateNormalizedPointAt(polarAngle, azimuthAngle);
            }
        }

        var southPole = new Vector3(0.0f, 0.0f, -1.0f);
        normalizedGridPoints[numberOfGridPoints - 1] = southPole;

        return normalizedGridPoints;
    }

    private Vector3 GenerateNormalizedPointAt(float polarAngle, float azimuthAngle)
    {
        float x = Mathf.Sin(polarAngle) * Mathf.Cos(azimuthAngle);
        float y = Mathf.Sin(polarAngle) * Mathf.Sin(azimuthAngle);
        float z = Mathf.Cos(polarAngle);

        var point = new Vector3(x, y, z);
        
        return point;
    }

    private int CalculateNumberOfLatitudes(float desiredAngularResolution)
    {
        return Mathf.CeilToInt(Mathf.PI / desiredAngularResolution) + 1;
    }

    private int CalculateNumberOfLongitudes(float desiredAngluarResolution)
    {
        return Mathf.CeilToInt(2 * Mathf.PI / desiredAngluarResolution);
    }
}

