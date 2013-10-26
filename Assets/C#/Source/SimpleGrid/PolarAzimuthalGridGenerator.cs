using UnityEngine;

public class PolarAzimuthalGridGenerator
{
    public Vector3[] Directions { get; private set; }
    public PolarAzimuthalIndexHelper IndexHelper;

    private readonly int _numberOfLatitudes;
    private readonly int _numberOfLongitudes;

    public PolarAzimuthalGridGenerator(int numberOfLatitudes, int numberOfLongitudes)
    {
        _numberOfLatitudes = numberOfLatitudes;
        _numberOfLongitudes = numberOfLongitudes;

        GenerateGridPoints();

        IndexHelper = new PolarAzimuthalIndexHelper(numberOfLatitudes, numberOfLongitudes);
    }

    private void GenerateGridPoints()
    {
        int numberOfGridPoints = _numberOfLongitudes * (_numberOfLatitudes - 2) + 2;
        Directions = new Vector3[numberOfGridPoints];

        var northPole = new Vector3(0.0f, 0.0f, 1.0f);
        Directions[0] = northPole;

        for (int i = 1; i < _numberOfLatitudes - 1; i++)
        {
            for (int j = 0; j < _numberOfLongitudes; j++)
            {
                int index = 1 + (i - 1) * _numberOfLongitudes + j;
                Directions[index] = DirectionAtPoint(i, j);
            }
        }

        var southPole = new Vector3(0.0f, 0.0f, -1.0f);
        Directions[numberOfGridPoints - 1] = southPole;
    }

    private Vector3 DirectionAtPoint(int polarIndex, int azimuthIndex)
    {
        float polarAngle = (Mathf.PI * polarIndex) / (_numberOfLatitudes - 1);
        float azimuthAngle = (2 * Mathf.PI * azimuthIndex) / _numberOfLongitudes;

        float x = Mathf.Sin(polarAngle) * Mathf.Cos(azimuthAngle);
        float y = Mathf.Sin(polarAngle) * Mathf.Sin(azimuthAngle);
        float z = Mathf.Cos(polarAngle);

        var point = new Vector3(x, y, z);

        return point;
    }
}

