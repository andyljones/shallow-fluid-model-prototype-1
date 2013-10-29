using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Simulator<TAtmosphereElement, TConditions> : ISimulator<TAtmosphereElement, TConditions>
    where TAtmosphereElement : class, ISimulableAtmosphereElement
    where TConditions : struct, ISimulableConditions
{
    private float _timestep;
    private int _maxSteps;
    private Atmosphere<TAtmosphereElement> _atmosphere;
    protected TConditions[] _oldConditions; // Protected rather than private for test purposes.
    protected TConditions[] _currentConditions; // Protected rather than private for test purposes.
    protected PersistantInformation[] _information; // Protected rather than private for test purposes.

    public Simulator(float timestep, int maxStepsPerFrame)
    {
        _maxSteps = maxStepsPerFrame;
        _timestep = timestep;
    }


    public void InitializeSimulator(Atmosphere<TAtmosphereElement> atmosphere, TConditions initialConditions)
    {
        _atmosphere = atmosphere;

        int numberOfElements = atmosphere.Elements.Length;

        _oldConditions = Enumerable.Repeat(initialConditions, numberOfElements).ToArray();
        _currentConditions = Enumerable.Repeat(initialConditions, numberOfElements).ToArray();
        _information = new PersistantInformation[numberOfElements];

        foreach (var element in atmosphere.Elements)
        {
            InitializeElement(element, initialConditions);
        }
    }

    private void InitializeElement(TAtmosphereElement element, TConditions initialConditions)
    {
        element.Conditions = initialConditions;

        var localCoordSystem = GetLocalCoordinateSystem(element);
        var localX = localCoordSystem[0];
        var localY = localCoordSystem[1];
        var localZ = localCoordSystem[2];

        var neighbourVectors = GetNeighbourVectors(element, localX, localY);

        _information[element.Index] = new PersistantInformation(localX, localY, localZ) { NeighbourVectors = neighbourVectors };
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

        return new[] {localX, localY, localZ};
    }

    private Vector3[] GetNeighbourVectors(TAtmosphereElement element, Vector3 localX, Vector3 localY)
    {
        return element.Boundaries.Select(boundary => GetNeighbourVector(element, boundary, localX, localY)).ToArray();
    }

    private Vector3 GetNeighbourVector(TAtmosphereElement element, Boundary boundary, Vector3 localX, Vector3 localY)
    {
        var neighbour = _atmosphere.Elements[boundary.NeighboursIndex];
        var relativeVector = (neighbour.Radius*neighbour.Direction) - (element.Radius*element.Direction);

        float x = Vector3.Dot(relativeVector, localX);
        float y = Vector3.Dot(relativeVector, localY);

        return new Vector3(x, y, 0);
    }

    public void Update()
    {
        var oldConditions = _oldConditions; // Local copy to prevent torn reads.

        foreach (var element in _atmosphere.Elements)
        {
            element.Conditions = oldConditions[element.Index];
        }
    }
}
