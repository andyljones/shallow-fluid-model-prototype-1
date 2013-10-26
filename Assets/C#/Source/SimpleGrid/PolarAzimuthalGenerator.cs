using UnityEngine;

public class PolarAzimuthalGenerator
{
    public Vector3[] NormalizedGridPoints { get; private set; }
    public PolarAzimuthalHelper NavigationHelper;

    private int NumberOfLatitudes;
    private int NumberOfLongitudes;

    public PolarAzimuthalGenerator(int numberOfLatitudes, int numberOfLongitudes)
    {
        NumberOfLatitudes = numberOfLatitudes;
        NumberOfLongitudes = numberOfLongitudes;

        NavigationHelper = new PolarAzimuthalHelper(numberOfLatitudes, numberOfLongitudes);

        GenerateGridPoints();
    }

    private void GenerateGridPoints()
    {
        int NumberOfGridPoints = NumberOfLongitudes * (NumberOfLatitudes - 2) + 2;
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
}

