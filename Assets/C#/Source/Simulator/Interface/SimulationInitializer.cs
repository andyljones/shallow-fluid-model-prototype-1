using System.Linq;
using UnityEngine;

public class SimulatorInitializer<TAtmosphereElement, TConditions>
    where TAtmosphereElement : class, ISimulableAtmosphereElement
    where TConditions : struct, ISimulableConditions
{
    private TAtmosphereElement[] _atmosphereElements;
    private readonly TConditions _initialConditions;

    public PersistantInformation[] Information { get; private set; }

    public SimulatorInitializer(TAtmosphereElement[] atmosphereElements, TConditions initialConditions)
    {
        _atmosphereElements = atmosphereElements;
        _initialConditions = initialConditions;
        Information = new PersistantInformation[_atmosphereElements.Length];
    }

    public void InitializeAtmosphericElements()
    {
        foreach (var element in _atmosphereElements)
        {
            InitializeElement(element);
        }
    }

    private void InitializeElement(TAtmosphereElement element)
    {
        element.Conditions = _initialConditions;

        var localCoordSystem = GetLocalCoordinateSystem(element);
        var localX = localCoordSystem[0];
        var localY = localCoordSystem[1];
        var localZ = localCoordSystem[2];

        var neighbourVectors = GetNeighbourVectors(element, localX, localY);

        Information[element.Index] = new PersistantInformation(localX, localY, localZ) { NeighbourVectors = neighbourVectors };
    }

    private Vector3[] GetLocalCoordinateSystem(TAtmosphereElement element)
    {
        var globalZ = new Vector3(0, 0, 1);

        Vector3 localZ = element.Direction.normalized;
        Vector3 localX;
        Vector3 localY;

        if (localZ == globalZ) // Deal with singularity at north pole.
        {
            // Continuation of the prime meridian's local coordinate systems.
            localX = new Vector3(0, -1, 0);
            localY = new Vector3(-1, 0, 0);
        }
        else if (localZ == -globalZ) // Deal with singularity at south pole.
        {
            // Continuation of the prime meridian's local coordinate systems.
            localX = new Vector3(0, -1, 0);
            localY = new Vector3(1, 0, 0);
        }
        else
        {
            localX = Vector3.Cross(localZ, globalZ).normalized; // Points east. I think.
            localY = Vector3.Cross(localX, localZ).normalized; // Points north. I think.
        }

        return new[] { localX, localY, localZ };
    }

    private Vector3[] GetNeighbourVectors(TAtmosphereElement element, Vector3 localX, Vector3 localY)
    {
        return element.Boundaries.Select(boundary => GetNeighbourVector(element, boundary, localX, localY)).ToArray();
    }

    private Vector3 GetNeighbourVector(TAtmosphereElement element, Boundary boundary, Vector3 localX, Vector3 localY)
    {
        var neighbour = _atmosphereElements[boundary.NeighboursIndex];
        var relativeVector = (neighbour.Radius * neighbour.Direction) - (element.Radius * element.Direction);

        float x = Vector3.Dot(relativeVector, localX);
        float y = Vector3.Dot(relativeVector, localY);

        return new Vector3(x, y, 0);
    }
}